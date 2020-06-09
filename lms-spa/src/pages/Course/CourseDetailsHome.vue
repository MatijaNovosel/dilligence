<template>
  <div>
    <div class="absolute-top-right">
      <q-btn
        v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse)"
        @click="editLandingPage"
        size="sm"
        class="q-mr-sm"
        flat
      >{{`${editMode ? 'Stop editing' : 'Edit landing page'}`}}</q-btn>
      <q-btn
        size="sm"
        flat
        v-if="editMode && hasCoursePrivileges(courseId, Privileges.CanManageCourse)"
        @click="saveLandingPage"
      >Save</q-btn>
    </div>
    <div v-if="loading" class="col-12 text-center q-mt-lg">
      <q-spinner size="3em" />
    </div>
    <template v-else>
      <div class="row" v-if="editMode">
        <div class="col-12">
          <q-editor v-model="landingPage" />
        </div>
      </div>
      <div v-else v-html="landingPage" />
    </template>
  </div>
</template>

<script>
import CourseService from "../../services/api/course";
import UserMixin from "../../mixins/userMixin";
import NotificationService from "../../services/notification/notifications";

export default {
  name: "CourseDetailsHome",
  mixins: [UserMixin],
  created() {
    this.courseId = this.$route.params.id;
    this.getLandingPage();
  },
  methods: {
    saveLandingPage() {
      CourseService.updateLandingPage(
        { content: this.landingPage, courseId: this.courseId },
        this.courseId
      ).then(() => {
        NotificationService.showSuccess("Landing page successfully updated!");
      });
      this.editMode = !this.editMode;
    },
    editLandingPage() {
      this.editMode = !this.editMode;
    },
    getLandingPage() {
      this.loading = true;
      CourseService.getLandingPage(this.courseId)
        .then(({ data }) => {
          this.landingPage = data;
        })
        .finally(() => {
          this.loading = false;
        });
    }
  },
  data() {
    return {
      loading: false,
      editMode: false,
      courseId: null,
      landingPage: ""
    };
  }
};
</script>
