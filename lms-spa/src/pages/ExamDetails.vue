<template>
  <div>
    <div class="row justify-center">
      <div class="col-3 q-pa-md" v-show="centerQuestion">
        <q-card>
          <q-card-section class="text-center">General programming exam</q-card-section>
          <q-separator />
          <q-card-section>Started on 10:45, 10th of March 2019</q-card-section>
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
                <template v-for="(item, i) in questionInfo">
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
              :disabled="selectedQuestion + 1 >= questionInfo.length"
              @click="++selectedQuestion"
            />
          </q-card-section>
          <q-separator />
          <template v-for="(question, i) in questionInfo">
            <div :key="i" v-if="selectedQuestion === i">
              <q-card-section class="text-center">
                <span class="text-h6">Question {{ `${i + 1} - ${question.title}` }}</span>
                <div class="row q-pb-md q-pl-md">{{ question.question }}</div>
                <div class="justify-center row">
                  <div class="col-11">
                    <code-sheet :code="question.content" />
                  </div>
                </div>
              </q-card-section>
              <q-separator />
              <q-card-actions class="q-px-md">
                <answer-footer
                  :type="question.type"
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
      timeLeft: null,
      selectedQuestion: 0,
      selectedAnswer: null,
      questionTypes: null,
      resetAnswer: null,
      answeredQuestions: [],
      questionInfo: [
        {
          id: 1,
          title: "Javascript",
          question: "What is the output of this block of code?",
          content: `var numbers = [ 1, 2, 3, 4, 5 ];\nnumbers.sort((a, b) => { \n  if(a > b) return 1; \n  else return -1;\n});\nconsole.log(numbers);`,
          type: 1,
          userAnswers: null,
          answers: [
            {
              value: 1,
              label: "I don't know"
            },
            {
              value: 2,
              label: "Compilation error"
            },
            {
              value: 3,
              label: "Array(1, 2, 3, 4, 5)"
            },
            {
              value: 4,
              label: "Array(5, 4, 3, 2, 1)"
            }
          ]
        },
        {
          id: 2,
          title: "C++",
          question:
            "How would One initialize an instance of this generic class?",
          content: `template <class T>\nclass Array {\npublic:\n\tT* array;\n\tint NumberOfElements;\n\tArray(int n);\n\t~Array() { delete[] array; }\n}\n\ntemplate <class T>\nArray<T>::Array(int n) {\n\tNumberOfElements = n;\n\tarray = new T[n];\n}`,
          type: 1,
          userAnswers: null,
          answers: [
            {
              value: 1,
              label: "Array(double) P[50];"
            },
            {
              value: 2,
              label: "Array<double> P[50];"
            },
            {
              value: 3,
              label: "Array P[50];"
            },
            {
              value: 4,
              label: "Array<double> P[50];"
            }
          ]
        },
        {
          id: 3,
          title: "C++ 2",
          question: "What is the output of this block of code?",
          content: `template\nclass A\n{\nprotected:\n\tT x;\npublic:\n\tA(T x) { this->x = x; }\n\tvoid print()\n\t{\n\t\tcout << x << " ";\n\t}\n};\n\nint main()\n{\n\tA a('A');\n\tA b('A');\n\ta.print(); b.print();\n}`,
          type: 2,
          userAnswers: [],
          answers: [
            {
              value: 1,
              label:
                "It outputs nothing because it is not possible to use char values instead of integer ones"
            },
            {
              value: 2,
              label: "65 65"
            },
            {
              value: 3,
              label: "A A"
            },
            {
              value: 4,
              label: "A 65"
            },
            {
              value: 5,
              label: "65 A"
            }
          ]
        }
      ]
    };
  },
  methods: {
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
      // Val od RADIO dolazi kao jedna vrijednost, dok od CHECKBOX kao polje
      if (
        this.questionInfo[this.selectedQuestion].type ==
        this.questionTypes.CHECKBOX
      ) {
        if (val.length == 0 || val == null) {
          this.answeredQuestions = this.answeredQuestions.filter(
            x => x != this.selectedQuestion - 1
          );
          return;
        }
      }
      this.questionInfo[this.selectedQuestion].userAnswers = val;
      this.answeredQuestions.push(this.selectedQuestion - 1);
    },
    _resetAnswer() {
      if (
        this.questionInfo[this.selectedQuestion].type ==
        this.questionTypes.CHECKBOX
      ) {
        this.questionInfo[this.selectedQuestion].userAnswers = [];
      } else {
        this.questionInfo[this.selectedQuestion].userAnswers = null;
      }
      this.resetAnswer = !this.resetAnswer;
      this.answeredQuestions = this.answeredQuestions.filter(
        x => x != this.selectedQuestion - 1
      );
    }
  },
  created() {
    var countDownDate = new Date();
    countDownDate.setHours(countDownDate.getHours() + 4);
    countDownDate = countDownDate.getTime();
    setInterval(() => {
      var now = new Date().getTime();
      var distance = countDownDate - now;
      var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((distance % (1000 * 60)) / 1000);
      if (seconds < 10) {
        seconds = "0" + seconds;
      }
      this.timeLeft = `${minutes}:${seconds}`;
    }, 100);
    this.questionTypes = { RADIO: 1, CHECKBOX: 2 };
  }
};
</script>

<style scoped>
.gore-desno {
  position: absolute;
  top: 10px;
  right: 5px;
  margin: 5px;
}
</style>