<template>
  <div>
    <v-row dense no-gutters>
      <v-col v-for="(answer, index) in answers" :key="answer.id" cols="12" class="my-n4">
        <v-checkbox v-model="selectedAnswer" 
                    :label="answer.content" 
                    :value="index" 
                    dense
                    multiple
                    color="primary">
        </v-checkbox>
      </v-col>
    </v-row>
  </div>
</template>

<script>

  export default {
    props: [ 'answers', 'reset', 'selectedAnswers' ],
    data() {
      return {
        selectedAnswer: []
      }
    },
    watch: {
      selectedAnswer: {
        immediate: false,
        handler() {
          this.$emit("answerChanged", this.selectedAnswer); 
        }
      },
      reset: {
        immediate: false,
        handler() {
          this.selectedAnswer = [];
        }
      },
      selectedAnswers: {
        immediate: true,
        deep: true,
        handler(val) {
          this.selectedAnswer = val;
        }
      }
    }
  }
  
</script>