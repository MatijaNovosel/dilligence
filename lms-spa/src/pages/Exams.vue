<template>
  <div>
    <v-row justify="center">
      <v-col cols="3" v-show="centerQuestion">
        <v-card class="mx-auto">
          <v-card-title class="text-center">General programming exam</v-card-title>
          <v-card-subtitle>Started on 10:45, 10th of March 2019</v-card-subtitle>
          <v-divider />
          <v-card-text>
            <v-row class="px-4 pt-2 mb-n4">
              <v-text-field
                dense
                filled
                type="text"
                v-model="timeLeft"
                label="Time left"
                prepend-icon="mdi-clock-outline"
                readonly
              />
            </v-row>
          </v-card-text>
          <v-divider />
          <v-card-actions class="px-4">
            <v-row>
              <v-col cols="12" class="mb-n8">
                <p class="font-weight-light subtitle-1">Progress:</p>
              </v-col>
              <v-col cols="12">
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
              </v-col>
            </v-row>
          </v-card-actions>
          <v-divider />
          <v-card-actions class="px-4">
            <v-row justify="center" class="my-3">
              <v-btn class="primary" @click="finishExamDialog = true">Finish exam</v-btn>
            </v-row>
          </v-card-actions>
        </v-card>
      </v-col>
      <v-col :cols="questionCols">
        <v-card class="mx-auto">
          <v-card-title class="text-center">
            Question {{ `${selectedQuestion + 1} - ${questionInfo[selectedQuestion].title}` }}
            <v-spacer />
            <v-btn
              icon
              text
              @click="selectedQuestion = selectedQuestion - 1 < 0 ? selectedQuestion : --selectedQuestion"
            >
              <v-icon>mdi-chevron-left</v-icon>
            </v-btn>
            <v-btn icon text @click="hideInfoCard">
              <v-icon>{{ centerQuestion ? 'mdi-lock' : 'mdi-lock-open' }}</v-icon>
            </v-btn>
            <v-tooltip v-model="show" bottom>
              <template v-slot:activator="{ on }">
                <v-btn v-on="on" icon text @click="_resetAnswer">
                  <v-icon>mdi-autorenew</v-icon>
                </v-btn>
              </template>
              <span>Reset question</span>
            </v-tooltip>
            <v-btn
              icon
              text
              @click="selectedQuestion = selectedQuestion + 1 >= questionInfo.length ? selectedQuestion : ++selectedQuestion"
            >
              <v-icon>mdi-chevron-right</v-icon>
            </v-btn>
          </v-card-title>
          <v-divider />
          <v-card-text>
            <v-row class="pl-5">{{ questionInfo[selectedQuestion].question }}</v-row>
            <v-row justify="center">
              <v-col cols="11">
                <code-sheet :code="questionInfo[selectedQuestion].content" />
              </v-col>
            </v-row>
          </v-card-text>
          <v-divider />
          <v-card-actions class="px-8 mb-n2">
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
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
    <v-dialog v-model="finishExamDialog" persistent max-width="500">
      <v-card>
        <v-system-bar color="primary" />
        <v-card-text
          class="text-center mt-8"
        >Are you sure? You still have {{ timeLeft }} of time left!</v-card-text>
        <v-card-actions class="pb-5">
          <v-spacer />
          <v-btn color="green" class="white--text" @click="finishExamDialog = false">Yes</v-btn>
          <v-btn color="red" class="white--text" @click="finishExamDialog = false">No</v-btn>
          <v-spacer />
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import CodeSheet from "../components/CodeSheet";
import RadioFooter from "../components/AnswerFooter/RadioFooter";
import CheckboxFooter from "../components/AnswerFooter/CheckboxFooter";
import store from "../store/store";

export default {
  components: {
    CodeSheet,
    RadioFooter,
    CheckboxFooter
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
        },
        {
          title: "C++ 3",
          question:
            "What is the output of this block of code? Type X if there is a compilation error and ? if it outputs unpredictable values.",
          content: `template void Ispis(T* x)\n{\n\tcout << *x << endl;\n}\nint _tmain(int argc, TCHAR* argv[])\n{\n\tint x = 9;\n\tIspis(x);\n\treturn 0;\n}`,
          type: 2,
          answers: [
            {
              content: "Heyyy",
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
    this.questionTypes = store.getters.QUESTION_TYPES;
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