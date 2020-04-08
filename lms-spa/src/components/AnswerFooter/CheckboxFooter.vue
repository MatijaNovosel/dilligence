<template>
  <q-option-group dense v-model="selectedAnswer" :options="answers" type="checkbox" @input="change" />
</template>

<script>
export default {
  props: ["answers", "reset", "selectedAnswers"],
  data() {
    return {
      selectedAnswer: []
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