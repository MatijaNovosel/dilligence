<template>
  <div>
    <v-row justify="center">
      <v-col cols="3" v-show="centerQuestion">
        <v-card class="mx-auto">
          <v-card-title class="text-center">
            General programming exam
          </v-card-title>
          <v-card-subtitle>
            Started on 10:45, 10th of March 2019
          </v-card-subtitle>
          <v-divider />
          <v-card-text>
            <v-row class="px-4 pt-2 mb-n4">
              <v-text-field dense filled type="text" v-model="timeLeft" label="Time left" prepend-icon="mdi-clock-outline" readonly />
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
                  <v-chip v-for="n in [ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ]" :key="n" color="primary" text-color="white">
                    {{ n }}
                  </v-chip>
                </v-chip-group>
              </v-col>
            </v-row>
          </v-card-actions>
        </v-card>
      </v-col>
      <v-col :cols="questionCols">
        <v-card class="mx-auto">
          <v-btn icon text class="gore-desno" @click="hideInfoCard">
            <v-icon>
              {{ centerQuestion ? 'mdi-lock' : 'mdi-lock-open' }}
            </v-icon>
          </v-btn>
          <v-card-title class="text-center">
            {{ questionInfo[selectedQuestion].title }}
          </v-card-title>
          <v-divider />
          <v-card-text>
            <v-row class="pl-5">
              {{ questionInfo[selectedQuestion].question }}
            </v-row>
            <v-row justify="center">
              <v-col cols="11">
                <code-sheet :code="questionInfo[selectedQuestion].content" />
              </v-col> 
            </v-row>
          </v-card-text>
          <v-divider />
          <v-card-actions class="px-8">
            <radio-footer v-if="questionInfo[selectedQuestion].type == questionTypes.RADIO" v-bind:answers="questionInfo[selectedQuestion].answers" />
            <checkbox-footer v-else-if="questionInfo[selectedQuestion].type == questionTypes.CHECKBOX" v-bind:answers="questionInfo[selectedQuestion].answers" />
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import CodeSheet from '../components/CodeSheet';
import RadioFooter from '../components/AnswerFooter/RadioFooter';
import CheckboxFooter from '../components/AnswerFooter/CheckboxFooter';
import store from '../store/store';

export default { 
  components: {
    CodeSheet,
    RadioFooter,
    CheckboxFooter
  },
  data() {
    return { 
      centerQuestion: true,
      questionCols: 9,
      timeLeft: null,
      selectedQuestion: 0,
      questionTypes: null,
      questionInfo: [{
        title: "Question 1 - Javascript",
        question: "What is the output of this block of code?",
        content: `var numbers = [ 1, 2, 3, 4, 5 ];\nnumbers.sort((a, b) => { \n  if(a > b) return 1; \n  else return -1;\n});\nconsole.log(numbers);`,
        type: 1,
        answers:[{  
          content: "I don't know"
        }, {
          content: "Compilation error"  
        }, {
          content: "Array(1, 2, 3, 4, 5)"
        }, {
          content: "Array(5, 4, 3, 2, 1)"
        }]
      }, {
        title: "Question 2 - C++",
        question: "How would One initialize an instance of this generic class?",
        content: `template <class T>\nclass Array {\npublic:\n\tT* array;\n\tint NumberOfElements;\n\tArray(int n);\n\t~Array() { delete[] array; }\n}\n\ntemplate <class T>\nArray<T>::Array(int n) {\n\tNumberOfElements = n;\n\tarray = new T[n];\n}`,
        type: 1,
        answers: [{  
          content: "Array(double) P[50];"
        }, {
          content: "Array<double> P[50];"  
        }, {
          content: "Array P[50];"
        }, {
          content: "Array<double> P[50];"
        }]
      }]
    }
  },
  methods: {
    hideInfoCard() {
      this.centerQuestion = !this.centerQuestion;
      this.questionCols = this.centerQuestion ? 9 : 12;
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
      if(seconds < 10) {
        seconds = '0' + seconds;  
      }
      this.timeLeft = `${minutes}:${seconds}`;
    }, 1000);
    this.questionTypes = store.getters.QUESTION_TYPES;
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