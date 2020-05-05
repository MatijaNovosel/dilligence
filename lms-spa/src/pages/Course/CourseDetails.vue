<template>
  <q-page class="q-pa-md">
    <template v-if="course">
      <div class="row">
        <div class="col-12 q-py-md text-center">
          <span class="text-weight-light text-h5">{{ course.name }}</span>
        </div>
        <div class="col-12 text-center q-mb-md">
          <q-btn-group outline>
            <q-btn
              v-for="(subRoute, i) in subRoutes"
              :key="i"
              :icon="subRoute.icon"
              size="sm"
              :label="subRoute.label"
              :class="$route.name == subRoute.name ? $q.dark.isActive ? 'border-bottom-dark' : 'border-bottom-light' : ''"
              @click="$router.push({ name: subRoute.name })"
            />
          </q-btn-group>
        </div>
        <div class="col-12 q-mb-lg">
          <q-separator />
        </div>
        <div class="col-12">
          <router-view />
        </div>
      </div>
    </template>
  </q-page>
</template>

<script>
import CourseService from "../../services/api/course";
import UserCard from "../../components/user-card";

export default {
  name: "CourseDetails",
  components: {
    "user-card": UserCard
  },
  created() {
    this.courseId = this.$route.params.id;
    this.getData();
  },
  methods: {
    getData() {
      CourseService.getCourse(this.courseId).then(({ data }) => {
        this.course = data;
      });
    },
    getParticipants() {
      CourseService.getCourseUsers(this.courseId).then(({ data }) => {
        this.participants = data.results;
      });
    }
  },
  data() {
    return {
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
        }
      ],
      participants: null,
      course: null,
      courseId: null,
      tab: "home"
    };
  }
};
</script>

<style lang="sass">
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
.dialog-toolbar
  min-height: 30px
.container-border
  border: 1px solid #e0dede
</style>