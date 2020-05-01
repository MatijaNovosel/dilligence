<template>
  <div>
    <div class="row justify-center" v-if="attempt">
      <div class="col-3 q-pa-md" v-show="centerQuestion">
        <q-card>
          <q-card-section class="text-center">{{ attempt.exam.name }}</q-card-section>
          <q-separator />
          <q-card-section>
            <div class="row">
              <q-input
                class="full-width"
                dense
                filled
                type="text"
                v-model="timeLeft"
                :label="$i18n.t('timeLeft')"
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
              <q-btn color="primary" @click="finishExamDialog = true">{{ $i18n.t('finishExam') }}</q-btn>
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
        <q-card-section class="text-center q-mt-md">{{ $i18n.t("areYouSure") }}</q-card-section>
        <q-card-actions class="q-pb-md">
          <q-space />
          <q-btn
            color="green"
            class="text-white"
            @click="finishExamDialog = false"
          >{{ $i18n.t("yes") }}</q-btn>
          <q-btn
            color="red"
            class="text-white"
            @click="finishExamDialog = false"
          >{{ $i18n.t("no") }}</q-btn>
          <q-space />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import AnswerFooter from "../components/AnswerFooter/AnswerFooter";
import ExamService from "../services/api/exam";
import UserMixin from "../mixins/userMixin";

export default {
  name: "ExamDetails",
  mixins: [UserMixin],
  components: {
    AnswerFooter
  },
  data() {
    return {
      finishExamDialog: false,
      centerQuestion: true,
      questionCols: 9,
      selectedQuestion: 0,
      selectedAnswer: null,
      time: null,
      questionTypes: null,
      resetAnswer: null,
      answeredQuestions: [],
      attempt: null,
      timerIntervalId: null,
      expired: false
    };
  },
  computed: {
    timeLeft() {
      if (this.attempt != null) {
        if (this.expired) {
          return this.$i18n.t("expired");
        }
        return this.$options.filters.countdownFilter(this.time);
      }
    }
  },
  methods: {
    updateAttempt() {
      let updatedAttempt = JSON.parse(JSON.stringify(this.attempt));
      updatedAttempt.exam.questions.forEach(x => {
        if (x.typeId === this.questionTypes.RADIO) {
          x.userAnswers =
            x.userAnswers === null || x.userAnswers === 0
              ? []
              : [x.userAnswers];
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
      this.updateAttempt();
    },
    getAttemptData(attemptId) {
      ExamService.getAttemptDetails(attemptId).then(({ data }) => {
        data.exam.questions.forEach(x => {
          if (x.typeId == this.questionTypes.RADIO) {
            x.userAnswers = x.userAnswers.length == 0 ? null : x.userAnswers[0];
          }
        });

        this.attempt = data;

        this.attempt.exam.questions.forEach((x, i) => {
          if (
            x.userAnswers != [] &&
            x.userAnswers != null &&
            x.userAnswers != 0
          ) {
            this.answeredQuestions.push(i - 1);
          }
        });

        let startDate = new Date(this.attempt.startedAt);
        let currentDate = new Date();
        let secondBetweenTwoDate = Math.abs(
          (currentDate.getTime() - startDate.getTime()) / 1000
        );

        this.time = this.attempt.exam.timeNeeded - secondBetweenTwoDate;
        if (this.time < 0) {
          this.expired = true;
        }

        this.timerIntervalId = setInterval(() => {
          this.time--;
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
  }
};
</script>