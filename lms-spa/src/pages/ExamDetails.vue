<template>
  <div>
    <div class="row justify-center" v-if="attempt">
      <div class="col-3 q-pa-md" v-show="centerQuestion">
        <q-card>
          <q-card-section class="text-center">{{ attempt.exam.naziv }}</q-card-section>
          <q-separator />
          <q-card-section>
            <div class="row">
              <q-input
                class="full-width"
                dense
                filled
                type="text"
                v-model="timeLeft"
                label="Time left"
                readonly
              />
            </div>
          </q-card-section>
          <q-separator />
          <q-card-actions class="q-px-md">
            <div class="row">
              <div class="col-12">
                <template v-for="(item, i) in attempt.exam.questions">
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
          <q-separator />
          <q-card-actions class="justify-center">
            <div class="row q-my-sm">
              <q-btn color="primary" @click="finishExamDialog = true">Finish exam</q-btn>
            </div>
          </q-card-actions>
        </q-card>
      </div>
      <div :class="'q-pa-md col-' + questionCols">
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
            <q-btn flat dense round icon="mdi-autorenew" text @click="_resetAnswer" />
            <q-btn
              flat
              dense
              round
              icon="mdi-chevron-right"
              text
              :disabled="selectedQuestion + 1 >= attempt.exam.questions.length"
              @click="++selectedQuestion"
            />
          </q-card-section>
          <q-separator />
          <template v-for="(question, i) in attempt.exam.questions">
            <div :key="i" v-if="selectedQuestion === i">
              <q-card-section>
                <div class="row">
                  <div class="col-12 text-center">
                    <span class="text-h6">Question {{ `${i + 1} - ${question.title}` }}</span>
                  </div>
                  <div class="col-12 border-box q-mt-md">
                    <p class="q-pa-md" v-html="question.content"></p>
                    <!-- <code-sheet :code="question.content" /> -->
                  </div>
                </div>
              </q-card-section>
              <q-separator />
              <q-card-actions class="q-px-md">
                <answer-footer
                  :type="question.typeId"
                  :selectedAnswers="question.userAnswers"
                  :reset="resetAnswer"
                  :answers="question.answers"
                  @answerChanged="answerChanged"
                />
              </q-card-actions>
            </div>
          </template>
        </q-card>
      </div>
    </div>
    <q-dialog v-model="finishExamDialog" persistent max-width="500">
      <q-card>
        <q-system-bar color="primary" />
        <q-card-section
          class="text-center q-mt-md"
        >Are you sure? You still have {{ timeLeft }} of time left!</q-card-section>
        <q-card-actions class="q-pb-md">
          <q-space />
          <q-btn color="green" class="text-white" @click="finishExamDialog = false">Yes</q-btn>
          <q-btn color="red" class="text-white" @click="finishExamDialog = false">No</q-btn>
          <q-space />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import CodeSheet from "../components/CodeSheet";
import AnswerFooter from "../components/AnswerFooter/AnswerFooter";
import ExamService from "../services/api/exam";
import { mapGetters } from "vuex";

export default {
  name: "ExamDetails",
  components: {
    CodeSheet,
    AnswerFooter
  },
  data() {
    return {
      finishExamDialog: false,
      centerQuestion: true,
      questionCols: 9,
      selectedQuestion: 0,
      selectedAnswer: null,
      questionTypes: null,
      resetAnswer: null,
      answeredQuestions: [],
      attempt: null,
      timerIntervalId: null
    };
  },
  computed: {
    ...mapGetters(["user"]),
    timeLeft() {
      if (this.attempt != null)
        return this.$options.filters.countdownFilter(this.attempt.timeLeft);
    }
  },
  methods: {
    updateAttempt() {
      let updatedAttempt = JSON.parse(JSON.stringify(this.attempt));
      updatedAttempt.exam.questions.forEach(x => {
        if (x.typeId === this.questionTypes.RADIO) {
          x.userAnswers = x.userAnswers === null ? [] : [x.userAnswers];
        }
      });
      ExamService.updateAttemptCommand(updatedAttempt);
    },
    chipColor(i) {
      if (i === this.selectedQuestion) {
        if (this.answeredQuestions.includes(this.selectedQuestion - 1)) {
          return this.$q.dark.isActive ? "grey-2" : "blue-2";
        }
        return this.$q.dark.isActive ? "grey-4" : "blue-4";
      }
      if (this.answeredQuestions.includes(i - 1)) {
        return this.$q.dark.isActive ? "grey-6" : "blue-6";
      }
      return this.$q.dark.isActive ? "grey-8" : "blue-8";
    },
    hideInfoCard() {
      this.centerQuestion = !this.centerQuestion;
      this.questionCols = this.centerQuestion ? 9 : 12;
    },
    answerChanged(val) {
      if (
        this.attempt.exam.questions[this.selectedQuestion].typeId ==
        this.questionTypes.CHECKBOX
      ) {
        if (val.length == 0 || val == null) {
          this.answeredQuestions = this.answeredQuestions.filter(
            x => x != this.selectedQuestion - 1
          );
          return;
        }
      }
      this.attempt.exam.questions[this.selectedQuestion].userAnswers = val;
      this.answeredQuestions.push(this.selectedQuestion - 1);
      this.updateAttempt();
    },
    _resetAnswer() {
      /*

        SPREMI U BACKEND OVDJE

      */
      let exam = this.attempt.exam;
      if (
        exam.questions[this.selectedQuestion].typeId ==
        this.questionTypes.CHECKBOX
      ) {
        exam.questions[this.selectedQuestion].userAnswers = [];
      } else {
        exam.questions[this.selectedQuestion].userAnswers = null;
      }
      this.resetAnswer = !this.resetAnswer;
      this.answeredQuestions = this.answeredQuestions.filter(
        x => x != this.selectedQuestion - 1
      );
    },
    getAttemptData(attemptId) {
      ExamService.getAttemptDetails(attemptId).then(({ data }) => {
        data.exam.questions.forEach(
          x =>
            (x.userAnswers =
              x.typeId == this.questionTypes.CHECKBOX ? [] : null)
        );
        this.attempt = data;
        this.timerIntervalId = setInterval(() => {
          this.attempt.timeLeft--;
        }, 1000);
      });
    }
  },
  beforeDestroy() {
    clearInterval(this.timerIntervalId);
  },
  created() {
    this.questionTypes = { RADIO: 1, CHECKBOX: 2 };
    const attemptId = this.$route.params.id;
    this.getAttemptData(attemptId);
    // STVORI UNLOAD METODU OVDJE
  }
};
</script>

<style scoped lang="sass">
.gore-desno
  position: absolute
  top: 10px
  right: 5px
  margin: 5px
.border-box
  position: relative
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 10px
</style>