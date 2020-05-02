<template>
  <q-option-group
    class="q-py-sm"
    dense
    v-model="selectedAnswer"
    :options="answers"
    :type="type == 1 ? 'radio' : 'checkbox'"
    @input="change"
  />
</template>

<script>
export default {
  name: "answer-footer",
  props: ["answers", "reset", "selectedAnswers", "type"],
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
        if (this.type == 1) {
          this.selectedAnswer = null;
        } else {
          this.selectedAnswer = [];
        }
      }
    },
    selectedAnswers: {
      deep: true,
      immediate: true,
      handler(val) {
        this.selectedAnswer = val;
      }
    }
  }
};
</script>