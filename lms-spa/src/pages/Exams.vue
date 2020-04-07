<template>
  <div>
    <div class="row justify-center">
      <div class="col-3 q-pa-md" v-show="centerQuestion">
        <q-card>
          <q-card-section class="text-center">General programming exam</q-card-section>
          <q-card-section>Started on 10:45, 10th of March 2019</q-card-section>
          <q-separator />
          <q-card-section>
            <div class="row">
              <q-input class="full-width" dense filled type="text" v-model="timeLeft" label="Time left" readonly />
            </div>
          </q-card-section>
          <q-separator />
          <q-card-actions class="q-px-md">
            <div class="row">
              <div class="col-12">
                <span class="q-pb-sm">Progress:</span>
              </div>
              <div class="col-12">
                <q-option-group
                  v-model="selectedQuestion"
                  :options="options"
                  color="primary"
                  inline
                  dense
                />
                <template v-for="(item, i) in questionInfo">
                  <q-chip
                    color="primary"
                    square
                    text-color="white"
                    :key="i"
                    :class="{ 
                      odgovoreno: answeredQuestions.includes(i - 1),
                      notOdgovoreno: !answeredQuestions.includes(i - 1) 
                    }"
                  >{{ i + 1 }}</q-chip>
                </template>
                <!--
                <v-chip-group column v-model="selectedQuestion" mandatory>
                  <v-chip
                    v-for="(item, index) in questionInfo"
                    :key="index"
                    :class="{ 
                            odgovoreno: answeredQuestions.includes(index - 1),
                            notOdgovoreno: !answeredQuestions.includes(index - 1) 
                          }"
                  >{{ index + 1 }}</v-chip>
                </v-chip-group>
                -->
              </div>
            </div>
          </q-card-actions>
          <q-separator />
          <q-card-actions class="justify-center">
            <div class="row q-my-sm">
              <q-btn class="primary" @click="finishExamDialog = true">Finish exam</q-btn>
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
              @click="selectedQuestion = selectedQuestion - 1 < 0 ? selectedQuestion : --selectedQuestion"
            />
            <q-btn
              flat
              dense
              round
              :icon="centerQuestion ? 'mdi-lock' : 'mdi-lock-open'"
              text
              @click="hideInfoCard"
            />
            <q-btn flat dense round icon="mdi-autorenew" text v-on="on" @click="_resetAnswer" />
            <q-btn
              flat
              dense
              round
              icon="mdi-chevron-right"
              text
              @click="selectedQuestion = selectedQuestion + 1 >= questionInfo.length ? selectedQuestion : ++selectedQuestion"
            />
          </q-card-section>
          <q-separator />
          <q-card-section class="text-center">
            <span class="text-h6">Question {{ `${selectedQuestion + 1} - ${questionInfo[selectedQuestion].title}` }}</span>
            <div class="row q-pb-md q-pl-md">{{ questionInfo[selectedQuestion].question }}</div>
            <div class="justify-center row">
              <div class="col-11">
                <code-sheet :code="questionInfo[selectedQuestion].content" />
              </div>
            </div>
          </q-card-section>
          <q-separator />
          <!--
          <q-card-actions class="q-px-md">
            <radio-footer
              @answerChanged="answerChanged"
              :selectedAnswers="selectedAnswers"
              :reset="resetAnswer"
              v-if="questionInfo[selectedQuestion].type == questionTypes.RADIO"
              :answers="questionInfo[selectedQuestion].answers"
            />
            <checkbox-footer
              @answerChanged="answerChanged"
              :selectedAnswers="selectedAnswers"
              :reset="resetAnswer"
              v-else-if="questionInfo[selectedQuestion].type == questionTypes.CHECKBOX"
              :answers="questionInfo[selectedQuestion].answers"
            />
          </q-card-actions>
          -->
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
// import RadioFooter from "../components/AnswerFooter/RadioFooter";
// import CheckboxFooter from "../components/AnswerFooter/CheckboxFooter";

export default {
  components: {
    CodeSheet
    // RadioFooter,
    // CheckboxFooter
  },
  data() {
    return {
      show: false,
      finishExamDialog: false,
      centerQuestion: true,
      questionCols: 9,
      timeLeft: null,
      selectedQuestion: 0,
      selectedAnswers: null,
      selectedAnswer: null,
      questionTypes: null,
      resetAnswer: null,
      answeredQuestions: [],
      questionInfo: [
        {
          title: "Javascript",
          question: "What is the output of this block of code?",
          content: `var numbers = [ 1, 2, 3, 4, 5 ];\nnumbers.sort((a, b) => { \n  if(a > b) return 1; \n  else return -1;\n});\nconsole.log(numbers);`,
          type: 1,
          answers: [
            {
              content: "I don't know",
              answered: false
            },
            {
              content: "Compilation error",
              answered: false
            },
            {
              content: "Array(1, 2, 3, 4, 5)",
              answered: false
            },
            {
              content: "Array(5, 4, 3, 2, 1)",
              answered: false
            }
          ]
        },
        {
          title: "C++",
          question:
            "How would One initialize an instance of this generic class?",
          content: `template <class T>\nclass Array {\npublic:\n\tT* array;\n\tint NumberOfElements;\n\tArray(int n);\n\t~Array() { delete[] array; }\n}\n\ntemplate <class T>\nArray<T>::Array(int n) {\n\tNumberOfElements = n;\n\tarray = new T[n];\n}`,
          type: 1,
          answers: [
            {
              content: "Array(double) P[50];",
              answered: false
            },
            {
              content: "Array<double> P[50];",
              answered: false
            },
            {
              content: "Array P[50];",
              answered: false
            },
            {
              content: "Array<double> P[50];",
              answered: false
            }
          ]
        },
        {
          title: "C++ 2",
          question: "What is the output of this block of code?",
          content: `template\nclass A\n{\nprotected:\n\tT x;\npublic:\n\tA(T x) { this->x = x; }\n\tvoid print()\n\t{\n\t\tcout << x << " ";\n\t}\n};\n\nint main()\n{\n\tA a('A');\n\tA b('A');\n\ta.print(); b.print();\n}`,
          type: 2,
          answers: [
            {
              content:
                "It outputs nothing because it is not possible to use char values instead of integer ones",
              answered: false
            },
            {
              content: "65 65",
              answered: false
            },
            {
              content: "A A",
              answered: false
            },
            {
              content: "A 65",
              answered: false
            },
            {
              content: "65 A",
              answered: false
            }
          ]
        }
      ]
    };
  },
  methods: {
    hideInfoCard() {
      this.centerQuestion = !this.centerQuestion;
      this.questionCols = this.centerQuestion ? 9 : 12;
    },
    answerChanged(val) {
      if (
        this.questionInfo[this.selectedQuestion].type ==
        this.questionTypes.CHECKBOX
      ) {
        this.questionInfo[this.selectedQuestion].answers.forEach((x, i) => {
          x.answered = val.includes(i) ? true : false;
        });
      } else {
        this.questionInfo[this.selectedQuestion].answers.forEach((x, i) => {
          x.answered = i == val ? true : false;
        });
      }

      this.answeredQuestions.push(this.selectedQuestion - 1);
    },
    _resetAnswer() {
      this.questionInfo[this.selectedQuestion].answers.map(
        x => (x.answered = false)
      );
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
    }, 1000);
    this.questionTypes = { CHECKBOX: 1 };
  },
  watch: {
    selectedQuestion: {
      immediate: false,
      handler(val) {
        let selection = [];
        this.questionInfo[val].answers.forEach((x, i) => {
          if (x.answered) selection.push(i);
        });
        this.selectedAnswers = selection;
      }
    }
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
.odgovoreno {
  background-color: #f0f0f0 !important;
}
.notOdgovoreno {
  background-color: #007bff !important;
  color: white !important;
}
</style>