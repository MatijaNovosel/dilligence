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
      >START</q-btn>
      <q-space />
    </q-card-actions>
  </q-card>
</template>

<script>
export default {
  name: "ExamCard",
  props: ["examData"],
  computed: {
    timeLeft() {
      const format = val => `0${Math.floor(val)}`.slice(-2);
      const hours = this.examData.timeLeft / 3600;
      const minutes = (this.examData.timeLeft % 3600) / 60;
      return [hours, minutes, this.examData.timeLeft % 60]
        .map(format)
        .join(":");
    }
  },
  data() {
    return {};
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
