<template>
  <div>
    <div class="row">
      <div class="col-12" v-for="(answer, index) in answers" :key="answer.id">
        <q-checkbox
          v-model="selectedAnswer"
          :label="answer.content"
          :value="index"
          dense
          multiple
          color="primary"
        ></q-checkbox>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["answers", "reset", "selectedAnswers"],
  data() {
    return {
      selectedAnswer: []
    };
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
};
</script>