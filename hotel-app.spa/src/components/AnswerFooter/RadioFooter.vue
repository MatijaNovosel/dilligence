<template>
  <div>
    <v-radio-group column v-model="selectedAnswer" @change="changeRadio">
      <v-radio v-for="answer in answers" 
              :key="answer.id" 
              :label="answer.content" 
              color="primary" 
              :value="answer.id">
      </v-radio>
    </v-radio-group>
  </div>
</template>

<script>

  export default {
    props: [ 'answers', 'reset', 'selectedAnswers' ],
    data() {
      return {
        selectedAnswer: null
      }
    },
    methods: {
      changeRadio() {
        this.$emit("answerChanged", this.selectedAnswer); 
      }  
    },
    watch: {
      reset: {
        immediate: false,
        handler() {
          this.selectedAnswer = null;
        }
      },
      selectedAnswers: {
        immediate: false,
        handler(val) {
          this.selectedAnswer = val[0];
        }
      }
    }
  }
  
</script>

<style scoped>

  .gore-desno {
    position: absolute;
    top: 5px;
    right: 5px;
    margin: 5px;
  }
  
</style>