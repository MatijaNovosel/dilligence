<template>
  <div>
    <div class="row" v-if="hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)">
      <div class="col-12 q-pb-md">
        <span>Unfinished exams</span>
      </div>
      <div class="col-12 q-pb-md">
        <div class="row q-col-gutter-sm">
          <div class="col-xs-6 col-md-4" :key="i" v-for="(unfinishedExam, i) in unfinishedExams">
            <q-card class="border-box-dark">
              <q-menu touch-position context-menu>
                <q-list
                  :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
                  dense
                  separator
                  style="min-width: 100px; border-radius: 6px;"
                >
                  <q-item
                    @click="$router.push({ name: 'exam-edit', params: { id: unfinishedExam.id } })"
                    clickable
                    v-close-popup
                  >
                    <q-item-section>View</q-item-section>
                  </q-item>
                  <q-item @click="deleteExam(unfinishedExam.id)" clickable v-close-popup>
                    <q-item-section>Delete</q-item-section>
                  </q-item>
                </q-list>
              </q-menu>
              <q-card-section
                class="q-py-sm text-center text-subtitle2"
              >Unfinished exam (ID {{ unfinishedExam.id }})</q-card-section>
            </q-card>
          </div>
        </div>
      </div>
      <div class="col-12 q-pb-md">
        <span>Created exams</span>
      </div>
      <div class="col-12 q-pb-md">
        <div class="row q-col-gutter-sm">
          <div class="col-xs-6 col-md-4" :key="i" v-for="(finishedExam, i) in finishedExams">
            <q-card class="border-box-dark">
              <q-menu touch-position context-menu>
                <q-list
                  :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
                  dense
                  separator
                  style="min-width: 100px; border-radius: 6px;"
                >
                  <q-item clickable v-close-popup>
                    <q-item-section>View grades</q-item-section>
                  </q-item>
                  <q-item clickable v-close-popup>
                    <q-item-section>Delete</q-item-section>
                  </q-item>
                  <q-item
                    @click="enableExamSolving(finishedExam.id)"
                    clickable
                    v-close-popup
                    v-if="!finishedExam.enabled"
                  >
                    <q-item-section>Enable solving</q-item-section>
                  </q-item>
                  <q-item
                    clickable
                    v-close-popup
                    @click="$router.push({ name: 'exam-preview', params: { id: finishedExam.id } })"
                  >
                    <q-item-section>Preview</q-item-section>
                  </q-item>
                </q-list>
              </q-menu>
              <q-card-section
                class="q-py-sm text-center text-subtitle2"
              >Exam (ID {{ finishedExam.id }})</q-card-section>
              <q-separator />
              <q-card-section class="q-py-sm">
                <span class="text-subtitle2">Name:</span>
                <span class="q-ml-sm">{{ finishedExam.name }}</span>
              </q-card-section>
              <q-separator />
              <q-card-section class="q-py-sm">
                <q-icon
                  size="xs"
                  :color="finishedExam.enabled ? 'green' : 'red'"
                  class="q-mr-md"
                  :name="finishedExam.enabled ? 'mdi-check' : 'mdi-close-thick'"
                />
                <span class="text-subtitle2">{{ finishedExam.enabled ? 'Enabled' : 'Not enabled' }}</span>
              </q-card-section>
            </q-card>
          </div>
        </div>
      </div>
    </div>
    <div v-else class="row">
      <div class="col-12 q-pb-md">
        <span>Available exams</span>
      </div>
      <div class="col-12 q-pb-md">
        <div class="row q-col-gutter-sm">
          <div class="col-xs-6 col-md-4" :key="i" v-for="(finishedExam, i) in availableExams">
            <q-card class="border-box-dark">
              <q-menu touch-position context-menu>
                <q-list
                  :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
                  dense
                  separator
                  style="min-width: 100px; border-radius: 6px;"
                >
                  <q-item clickable v-close-popup>
                    <q-item-section>Start attempt</q-item-section>
                  </q-item>
                </q-list>
              </q-menu>
              <q-card-section
                class="q-py-sm text-center text-subtitle2"
              >Exam (ID {{ finishedExam.id }})</q-card-section>
              <q-separator />
              <q-card-section class="q-py-sm">
                <span class="text-subtitle2">Name:</span>
                <span class="q-ml-sm">{{ finishedExam.name }}</span>
              </q-card-section>
            </q-card>
          </div>
        </div>
      </div>
    </div>
    <q-page-sticky
      position="bottom-right"
      :offset="[18, 18]"
      v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageExams, Privileges.CanCreateExams) 
      && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
    >
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
    enableExamSolving(id) {
      ExamService.enableExamSolving(id, this.courseId).then(() => {
        this.getFinishedExams();
      });
    },
    getUnfinishedExams() {
      ExamService.getUnfinishedExams(this.user.id, this.courseId).then(
        ({ data }) => {
          this.unfinishedExams = data;
        }
      );
    },
    getFinishedExams() {
      ExamService.getFinishedExams(this.user.id, this.courseId).then(
        ({ data }) => {
          this.finishedExams = data;
        }
      );
    },
    getFinishedExamsForUser() {
      ExamService.getAvailableExams(this.courseId, this.user.id).then(({ data }) => {
        this.availableExams = data;
      });
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
    if (
      this.hasCoursePrivileges(
        this.courseId,
        this.Privileges.IsInvolvedWithCourse
      )
    ) {
      this.getUnfinishedExams();
      this.getFinishedExams();
    } else {
      this.getFinishedExamsForUser();
    }
  },
  data() {
    return {
      courseId: null,
      unfinishedExams: null,
      finishedExams: null,
      availableExams: null
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