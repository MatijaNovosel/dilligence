<template>
  <q-card flat bordered>
    <q-badge class="gore-desno">
      <q-icon size="xs" name="mdi-clock-fast" />
      <span class="q-pl-xs">{{ timeLeft }}</span>
    </q-badge>
    <q-card-section class="q-py-sm">
      <div class="text-overline">{{ examData.exam.subject }}</div>
      <span class="ellipsis-text">{{ examData.exam.naziv }}</span>
    </q-card-section>
    <q-separator />
    <q-card-actions>
      <q-space />
      <q-btn
        @click="$router.push({ name: 'exam-details', params: { id: examData.id } })"
        flat
        size="sm"
        class="bg-red-4 text-white"
      >{{ buttonText }}</q-btn>
      <q-space />
    </q-card-actions>
  </q-card>
</template>

<script>
export default {
  name: "ExamCard",
  props: ["examData"],
  data() {
    return {};
  },
  computed: {
    timeLeft() {
      if (this.examData != null) {
        return this.$options.filters.countdownFilter(
          this.examData.exam.timeNeeded
        );
      }
    },
    buttonText() {
      if (this.examData != null) {
        if (this.examData.started && this.examData.terminated) {
          return this.$t("view");
        } else if (this.examData.started && !this.examData.terminated) {
          return this.$t("continue");
        } else {
          return this.$t("start");
        }
      }
    }
  },
  methods: {}
};
</script>

<style scoped lang="sass">
.gore-desno
  position: absolute
  top: 10px
  right: 10px
</style>
