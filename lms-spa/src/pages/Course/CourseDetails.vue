<template>
  <q-page class="q-pa-md">
    <template v-if="course">
      <div class="absolute-top-left">
        <q-btn
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse)"
          flat
          size="sm"
          @click="openChangePasswordDialog"
        >Change password</q-btn>
        <q-btn
          @click="confirmDialog = true"
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse)"
          class="q-ml-sm"
          flat
          size="sm"
        >Delete course</q-btn>
        <q-btn
          v-if="!hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
          class="q-ml-sm"
          flat
          size="sm"
        >Unsubscribe</q-btn>
      </div>
      <div class="row">
        <div class="col-12 q-pb-md q-pt-lg text-center">
          <span class="text-weight-light text-h5">{{ course.name }}</span>
        </div>
        <div class="col-12 text-center q-mb-md">
          <q-btn-group outline>
            <q-btn
              v-for="(subRoute, i) in subRoutes"
              :key="i"
              :icon="subRoute.icon"
              size="sm"
              :label="!$q.screen.xs && !$q.screen.sm ? subRoute.label : null"
              :class="$route.name == subRoute.name ? $q.dark.isActive ? 'border-bottom-dark' : 'border-bottom-light' : ''"
              @click="$router.push({ name: subRoute.name })"
            />
          </q-btn-group>
        </div>
        <div class="col-12 q-mb-md">
          <q-separator />
        </div>
        <div class="col-12">
          <router-view />
        </div>
      </div>
    </template>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="changePasswordDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar
          :style="`border-bottom: 1px solid ${$q.dark.isActive ? 'rgba(255, 255, 255, 0.6)' : 'rgba(0, 0, 0, 0.12)'};`"
        >
          <span style="font-size: 18px;">Change password</span>
          <q-space />
          <q-btn
            @click="closeDialog"
            :ripple="false"
            dense
            size="sm"
            flat
            round
            icon="mdi-close-thick"
          />
        </q-toolbar>
        <template v-if="course">
          <q-card-section>
            <q-input
              dense
              readonly
              outlined
              label="Current password"
              :type="!currentPasswordShow ? 'password' : 'text'"
              v-model="course.password"
            >
              <template v-slot:append>
                <q-btn
                  flat
                  round
                  size="sm"
                  :icon="!currentPasswordShow ? 'mdi-eye' : 'mdi-eye-off'"
                  @click="currentPasswordShow = !currentPasswordShow"
                />
              </template>
            </q-input>
          </q-card-section>
          <q-card-section class="q-pt-none">
            <q-input
              dense
              outlined
              label="New password"
              @input="passwordUpdated"
              :type="!newPasswordShow ? 'password' : 'text'"
              v-model="newPassword"
            >
              <template v-slot:append>
                <q-btn
                  flat
                  round
                  size="sm"
                  :icon="!newPasswordShow ? 'mdi-eye' : 'mdi-eye-off'"
                  @click="newPasswordShow = !newPasswordShow"
                />
              </template>
            </q-input>
          </q-card-section>
        </template>
      </q-card>
    </q-dialog>
    <confirmation-dialog
      :open="confirmDialog"
      @close="confirmDialog = false"
      @confirm="deleteCourse"
    />
  </q-page>
</template>

<script>
import CourseService from "../../services/api/course";
import NotificationService from "../../services/notification/notifications";
import UserMixin from "../../mixins/userMixin";
import { debounce } from "debounce";
import ConfirmationDialog from "../../components/confirmation-dialog";

export default {
  name: "CourseDetails",
  mixins: [UserMixin],
  components: {
    "confirmation-dialog": ConfirmationDialog
  },
  created() {
    this.courseId = this.$route.params.id;
    this.getData();
  },
  methods: {
    deleteCourse() {
      // delete course
    },
    passwordUpdated: debounce(function() {
      CourseService.updatePassword(this.courseId, this.newPassword).then(() => {
        this.getData();
        NotificationService.showSuccess("Password successfully changed!");
      });
    }, 1500),
    closeDialog() {
      this.changePasswordDialog = false;
      this.newPassword = null;
    },
    openChangePasswordDialog() {
      this.changePasswordDialog = true;
    },
    getData() {
      CourseService.getCourse(this.courseId).then(({ data }) => {
        this.course = data;
      });
    }
  },
  data() {
    return {
      confirmDialog: false,
      currentPasswordShow: false,
      newPasswordShow: true,
      dialogStyle: {
        width: "60%",
        "max-width": "90vw"
      },
      changePasswordDialog: false,
      newPassword: null,
      subRoutes: [
        {
          name: "course-details-home",
          label: "Home",
          icon: "mdi-home"
        },
        {
          name: "course-details-notifications",
          label: "Notifications",
          icon: "mdi-message-alert"
        },
        {
          name: "course-details-participants",
          label: "Participants",
          icon: "mdi-account-group"
        },
        {
          name: "course-details-tasks",
          label: "Tasks",
          icon: "mdi-calendar-check"
        },
        {
          name: "course-details-exams",
          label: "Exams",
          icon: "mdi-test-tube"
        },
        {
          name: "course-details-discussion",
          label: "Discussion",
          icon: "mdi-chat"
        },
        {
          name: "course-details-files",
          label: "Files",
          icon: "mdi-file"
        }
      ],
      course: null,
      courseId: null
    };
  }
};
</script>
