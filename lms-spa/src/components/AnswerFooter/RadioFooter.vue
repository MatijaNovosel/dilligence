<template>
  <q-option-group dense v-model="selectedAnswer" :options="answers" @input="change" />
</template>

<script>
export default {
  props: ["answers", "reset", "selectedAnswers"],
  data() {
    return {
      selectedAnswer: null
    };
  },
  methods: {
    change() {
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
};
</script>