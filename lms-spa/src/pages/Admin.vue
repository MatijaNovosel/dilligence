<template>
  <div class="row q-mt-lg">
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="open" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar
          :style="`border-bottom: 1px solid ${$q.dark.isActive ? 'rgba(255, 255, 255, 0.6)' : 'rgba(0, 0, 0, 0.12)'};`"
        >
          <span style="font-size: 18px;">Add course privileges</span>
          <q-space />
          <q-btn @click="reset" :ripple="false" dense size="sm" flat round icon="mdi-close-thick" />
        </q-toolbar>
        <q-card-section class="q-gutter-sm">
          <q-input
            @input="courseNameChanged"
            dense
            outlined
            v-model="searchTextCourse"
            label="Course name"
          />
        </q-card-section>
        <q-card-section class="q-pt-none">
          <q-scroll-area
            :thumb-style="thumbStyle"
            :bar-style="barStyle"
            :class="`border-box-${$q.dark.isActive ? 'dark' : 'light'}`"
            style="height: 400px;"
          >
            <q-option-group
              dense
              v-model="courses"
              :options="courseOptions"
              type="checkbox"
              @input="optionsChanged"
            />
          </q-scroll-area>
        </q-card-section>
      </q-card>
    </q-dialog>
    <div class="col-xs-12 col-md-4">
      <q-item>
        <q-item-section>
          <q-input dense label="Search" v-model="searchText" @input="searchTextChanged">
            <template v-slot:append>
              <q-icon name="search" v-if="!searchLoading" />
              <q-spinner size="1em" v-else />
            </template>
          </q-input>
        </q-item-section>
      </q-item>
      <q-scroll-area
        :thumb-style="thumbStyle"
        :bar-style="barStyle"
        :class="`border-box-${$q.dark.isActive ? 'dark' : 'light'}`"
        class="q-ma-sm"
        style="height: 600px;"
      >
        <q-item v-for="(user, i) in users" :key="i">
          <q-item-section>
            <q-item-label>{{ `${user.username} (${user.name} ${user.surname})` }}</q-item-label>
            <q-item-label caption lines="2">
              <q-icon style="margin-bottom: 1px" name="mdi-email" />
              Email: {{ user.email }}
            </q-item-label>
          </q-item-section>
          <q-item-section side>
            <q-item-label caption>
              <q-btn @click="getUserDetails(user.id)" size="sm" flat round icon="mdi-information" />
            </q-item-label>
          </q-item-section>
        </q-item>
      </q-scroll-area>
    </div>
    <div class="col-xs-12 col-md-8">
      <div class="row q-mx-sm">
        <div class="border-box-light full-width" v-if="activeUser">
          <div class="col-12 text-center">
            <q-img
              class="image-box q-mt-md"
              width="300px"
              height="300px"
              style="border-radius: 10px"
              :src="generatePictureSource(activeUser.picture)"
            />
          </div>
          <div class="col-12 text-center q-pt-md">
            <span
              style="font-size: 20px"
            >{{ `${activeUser.name} ${activeUser.surname} (${activeUser.username})` }}</span>
          </div>
          <div class="col-12 text-center q-pt-none">
            <span style="font-size: 12px">{{ activeUser.email }}</span>
          </div>
          <div class="col-12 q-ml-lg q-my-md">
            <q-separator class="q-my-sm" />
            <q-tree
              :nodes="generalPriveleges"
              :ticked.sync="activeUser.privileges.generalPrivileges"
              @update:ticked="generalPrivilegesChanged"
              node-key="key"
              tick-strategy="leaf-filtered"
              default-expand-all
            />
          </div>
          <div class="col-12 q-ml-lg q-my-md">
            <span>Course specific privileges</span>
            <q-btn
              class="q-ml-sm bg-green-6 text-white"
              size="xs"
              dense
              icon="mdi-plus-thick"
              flat
              @click="openPrivilegeDialog"
            />
            <q-separator class="q-my-sm" />
            <template
              v-if="activeUser.privileges.courses && activeUser.privileges.courses.length != 0"
            >
              <q-tree
                v-for="(course, i) in activeUser.privileges.courses"
                :nodes="courseSpecificPrivileges[i]"
                :key="i"
                :ticked.sync="courseSpecificPrivilegesTicked[i]"
                @update:ticked="courseSpecificPrivilegesChanged"
                node-key="key"
                tick-strategy="leaf-filtered"
              />
            </template>
            <template v-else>
              <span style="font-size: 12px">None found!</span>
            </template>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UserMixin from "../mixins/userMixin";
import UserService from "../services/api/user";
import CourseService from "../services/api/course";
import NotificationService from "../services/notification/notifications";
import { generatePictureSource } from "../helpers/helpers";
import { debounce } from "debounce";

export default {
  name: "Admin",
  mixins: [UserMixin],
  data() {
    return {
      open: false,
      users: null,
      courses: [],
      searchText: null,
      searchTextCourse: null,
      activeUser: null,
      searchLoading: false,
      courseOptions: [],
      thumbStyle: {
        right: "2px",
        borderRadius: "5px",
        backgroundColor: "#027be3",
        width: "5px",
        opacity: 0.75
      },
      barStyle: {
        right: "0px",
        borderRadius: "9px",
        backgroundColor: "#027be3",
        width: "9px",
        opacity: 0.2
      },
      courseSpecificPrivileges: [],
      courseSpecificPrivilegesTicked: [],
      dialogStyle: {
        width: "80%",
        "max-width": "90vw"
      },
      generalPriveleges: [
        {
          label: "General privileges",
          children: [
            { label: "Can grant privileges", key: 1 },
            { label: "Can view all users", key: 2 },
            { label: "Can create courses", key: 3 }
          ]
        }
      ],
      coursePrivileges: [
        { label: "Can manage course", key: 4 },
        { label: "Can change course landing page", key: 5 },
        { label: "Can manage notifications", key: 6 },
        { label: "Can send notifications", key: 7 },
        { label: "Can delete notifications", key: 8 },
        { label: "Can archive notifications", key: 9 },
        { label: "Can kick participants", key: 10 },
        { label: "Can mute participants", key: 11 },
        { label: "Can manage tasks", key: 12 },
        { label: "Can create tasks", key: 13 },
        { label: "Can delete tasks", key: 14 },
        { label: "Can grade tasks", key: 15 },
        { label: "Can manage exams", key: 16 },
        { label: "Can create exams", key: 17 },
        { label: "Can delete exams", key: 18 },
        { label: "Can grade exams", key: 19 },
        { label: "Can manage discussions", key: 20 },
        { label: "Can create new discussions", key: 21 },
        { label: "Can delete discussions", key: 22 },
        { label: "Can manage course files", key: 23 },
        { label: "Can upload course files", key: 24 },
        { label: "Can delete course files", key: 25 },
        { label: "Is involved with course", key: 26 }
      ]
    };
  },
  created() {
    this.getUsers();
  },
  methods: {
    generatePictureSource,
    courseSpecificPrivilegesChanged: debounce(function() {
      let payload = { userId: this.activeUser.id, courses: [] };
      this.courseSpecificPrivilegesTicked.forEach((x, i) => {
        payload.courses.push({
          courseId: this.courseSpecificPrivileges[i][0].key,
          privileges: x
        });
      });
      UserService.updateCoursePrivileges(payload).then(() => {
        this.getUserDetails(this.activeUser.id);
        NotificationService.showSuccess("Privileges successfully updated!");
      });
    }, 750),
    generalPrivilegesChanged: debounce(function() {
      UserService.updateGeneralPrivileges({
        privileges: this.activeUser.privileges.generalPrivileges,
        userId: this.activeUser.id
      }).then(() => {
        this.getUserDetails(this.activeUser.id);
        this.reinitPrivileges();
        NotificationService.showSuccess("Privileges successfully updated!");
      });
    }, 750),
    openPrivilegeDialog() {
      this.open = true;
      this.courseNameChanged();
    },
    optionsChanged: debounce(function() {
      UserService.updatePrivileges({
        courses: this.courses,
        userId: this.activeUser.id
      }).then(() => {
        this.getUserDetails(this.activeUser.id);
        this.reinitPrivileges();
        NotificationService.showSuccess("Privileges successfully updated!");
      });
    }, 750),
    reset() {
      this.searchTextCourse = null;
      this.open = false;
    },
    getUserDetails(id) {
      UserService.getUserDetails(id).then(({ data }) => {
        this.activeUser = data;
        this.courseSpecificPrivilegesTicked = [];
        this.courseSpecificPrivileges = [];
        this.activeUser.privileges.courses.forEach(x => {
          this.courseSpecificPrivilegesTicked.push(x.privileges);
          this.courseSpecificPrivileges.push([
            {
              label: x.name,
              key: x.id,
              children: this.coursePrivileges
            }
          ]);
        });
      });
    },
    getUsers() {
      this.searchLoading = true;
      UserService.getAll()
        .then(({ data }) => {
          this.users = data;
        })
        .finally(() => {
          this.searchLoading = false;
        });
    },
    courseNameChanged: debounce(function() {
      CourseService.get(
        this.activeUser.id,
        null,
        this.searchTextCourse,
        true,
        true
      ).then(({ data }) => {
        this.courseOptions = [];
        data.results.forEach(x => {
          this.courseOptions.push({ label: x.name, value: x.id });
          if (x.isInvolved) {
            this.courses.push(x.id);
          }
        });
      });
    }, 750),
    searchTextChanged: debounce(function() {
      if (this.searchText === "" || this.searchText == null) {
        this.getUsers();
      }
      UserService.searchUser(this.searchText).then(({ data }) => {
        this.users = data.results;
      });
    }, 750)
  }
};
</script>
