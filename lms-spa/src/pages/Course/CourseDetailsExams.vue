<template>
  <div>
    <div class="row" v-if="hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)">
      <div class="col-12 q-pb-md">
        <span>Unfinished exams</span>
      </div>
      <div class="col-12 q-pb-md">
        <div class="row q-col-gutter-sm">
          <div class="col-3" :key="i" v-for="(unfinishedExam, i) in unfinishedExams">
            <q-card class="border-box-dark">
              <q-card-section
                class="q-py-sm text-center text-subtitle2"
              >Unfinished exam (ID {{ unfinishedExam.id }})</q-card-section>
              <q-separator />
              <q-card-actions class="justify-center">
                <q-btn
                  size="sm"
                  class="q-px-md"
                  :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
                  @click="$router.push({ name: 'exam-edit', params: { id: unfinishedExam.id } })"
                >View</q-btn>
                <q-btn
                  size="sm"
                  class="q-px-md"
                  :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
                  @click="deleteExam(unfinishedExam.id)"
                >Delete</q-btn>
              </q-card-actions>
            </q-card>
          </div>
        </div>
      </div>
      <div class="col-12 q-pb-md">
        <span>Created exams</span>
      </div>
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
    deleteExam(id) {
      // Delete exam which is unfinished ...
      /*
      ExamService.deleteExam(id).then(() => {
        this.getUnfinishedExams();
      });
      */
    },
    getUnfinishedExams() {
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
    this.getUnfinishedExams();
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