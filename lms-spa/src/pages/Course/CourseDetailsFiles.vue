<template>
  <div>
    <div class="row">
      <div class="col-6" :key="content.id" v-for="content in sidebarContents">
        <file-cabinet :content="content" />
      </div>
    </div>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          icon="mdi-email-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New cabinet"
        />
        <q-fab-action
          icon="mdi-email-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="Upload file"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
import CourseService from "../../services/api/course";
import FileCabinet from "../../components/file-cabinet";

export default {
  name: "CourseDetailsFiles",
  components: {
    "file-cabinet": FileCabinet
  },
  created() {
    this.courseId = this.$route.params.id;
    this.getCourseFiles();
  },
  data() {
    return {
      courseId: null,
      sidebarContents: null
    };
  },
  methods: {
    getCourseFiles() {
      CourseService.getCourseSidebar(this.courseId).then(({ data }) => {
        this.sidebarContents = data;
      });
    }
  }
};
</script>

<style lang="sass">
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
</style>