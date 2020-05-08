<template>
  <div>
    Exams go here!
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          icon="mdi-email-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New exam"
          @click="createNewExam"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
import ExamService from "../../services/api/exam";
import UserMixin from "../../mixins/userMixin";

export default {
  name: "CourseDetailsExams",
  mixins: [UserMixin],
  methods: {
    createNewExam() {
      // Create new exam instance, get the id and send it as a parameter to route
      ExamService.createExam({
        courseId: this.courseId,
        createdById: this.user.id
      }).then(({ data }) => {
        this.$router.push({ name: "exam-edit", params: { id: data.payload } });
      });
    }
  },
  created() {
    this.courseId = this.$route.params.id;
  },
  data() {
    return {
      courseId: null
    };
  }
};
</script>

<style lang="sass">
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
</style>