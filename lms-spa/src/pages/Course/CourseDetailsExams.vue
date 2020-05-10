<template>
  <div>
    <div class="row">
      <div
        class="border-box-dark"
        :key="i"
        @click="$router.push({ name: 'exam-edit', params: { id: unfinishedExam.id } })"
        v-for="(unfinishedExam, i) in unfinishedExams"
      >{{ unfinishedExam.id }}</div>
    </div>
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
    getExams() {
      ExamService.getUnfinishedExams(this.user.id, this.courseId).then(
        ({ data }) => {
          this.unfinishedExams = data;
        }
      );
    },
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
    this.getExams();
  },
  data() {
    return {
      courseId: null,
      unfinishedExams: null
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