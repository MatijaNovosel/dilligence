<template>
  <div>
    <div class="row justify-center" v-if="exam">
      <div class="col-3 q-pa-md" v-show="centerQuestion">
        <q-card>
          <q-card-section class="text-center">{{ exam.name }}</q-card-section>
          <q-separator />
          <q-card-actions class="q-px-md">
            <div class="row">
              <div class="col-12">
                <template v-for="(item, i) in exam.questions">
                  <q-chip
                    :color="chipColor(i)"
                    clickable
                    square
                    :key="i"
                    @click="selectedQuestion = i"
                    style="cursor: pointer; user-select: none;"
                  >{{ i + 1 }}</q-chip>
                </template>
              </div>
            </div>
          </q-card-actions>
        </q-card>
      </div>
      <div :class="'q-pt-md q-pr-md col-' + questionCols">
        <q-card>
          <q-card-section class="text-center">
            <q-btn
              flat
              dense
              round
              icon="mdi-chevron-left"
              :disabled="selectedQuestion - 1 < 0"
              @click="--selectedQuestion"
            />
            <q-btn
              flat
              dense
              round
              :icon="centerQuestion ? 'mdi-lock' : 'mdi-lock-open'"
              text
              @click="hideInfoCard"
            />
            <q-btn
              flat
              dense
              round
              icon="mdi-chevron-right"
              text
              :disabled="selectedQuestion + 1 >= exam.questions.length"
              @click="++selectedQuestion"
            />
          </q-card-section>
          <q-separator />
          <template v-for="(question, i) in exam.questions">
            <div :key="i" v-if="selectedQuestion === i">
              <q-card-section>
                <div class="row">
                  <div class="col-12 text-center">
                    <span
                      class="text-h6"
                    >{{ `${$i18n.t('question')} ${i + 1} - ${question.title}` }}</span>
                  </div>
                  <div
                    class="col-12 q-mt-md"
                    :class="`border-box-${$q.dark.isActive ? 'dark' : 'light'}`"
                  >
                    <p class="q-pa-md" v-html="question.content"></p>
                  </div>
                </div>
              </q-card-section>
              <q-separator />
              <q-card-actions class="q-px-md">
                <q-option-group
                  class="q-py-sm"
                  disable
                  :value="question.typeId == 1 ? null : []"
                  :options="question.answers"
                  :type="question.typeId == 1 ? 'radio' : 'checkbox'"
                />
              </q-card-actions>
            </div>
          </template>
        </q-card>
      </div>
      <div class="col-12 text-right">
        <q-btn
          class="q-mt-md q-mr-md"
          size="sm"
          @click="$router.push({ name: 'course-details-exams', params: { id: exam.courseId } })"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
        >Back</q-btn>
      </div>
    </div>
  </div>
</template>

<script>
import AnswerFooter from "../components/AnswerFooter/answer-footer";
import ExamService from "../services/api/exam";
import UserMixin from "../mixins/userMixin";

export default {
  name: "ExamPreview",
  mixins: [UserMixin],
  components: {
    "answer-footer": AnswerFooter
  },
  data() {
    return {
      centerQuestion: true,
      questionCols: 9,
      selectedQuestion: 0,
      questionTypes: null,
      examId: null,
      exam: null
    };
  },
  methods: {
    hideInfoCard() {
      this.centerQuestion = !this.centerQuestion;
      this.questionCols = this.centerQuestion ? 9 : 12;
    },
    chipColor(i) {
      if (i === this.selectedQuestion) {
        return this.$q.dark.isActive ? "grey-4" : "blue-4";
      }
      return this.$q.dark.isActive ? "grey-8" : "blue-8";
    }
  },
  created() {
    this.examId = this.$route.params.id;
    ExamService.getExamPreview(this.examId).then(({ data }) => {
      this.exam = data;
    });
  }
};
</script>