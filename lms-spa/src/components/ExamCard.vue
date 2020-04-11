<template>
  <div class="q-pa-xs col-xs-12 col-sm-6 col-md-4 col-lg-3 grid-style-transition">
    <q-card flat bordered>
      <q-card-section class="q-py-md">
        <span class="ellipsis-text">EXAM 1</span>
      </q-card-section>
      <q-separator />
      <q-card-section class="q-py-none"></q-card-section>
      <q-separator />
      <q-card-actions>
        <q-space />
        <q-btn
          @click="$router.push({ name: 'exam-details', params: { id: '123' } })"
          flat
          size="sm"
          class="bg-red-4 text-white"
        >START</q-btn>
        <q-space />
      </q-card-actions>
    </q-card>
  </div>
</template>

<script>
export default {
  name: "ExamCard",
  data() {
    return {};
  },
  methods: {}
};
</script>

<style scoped lang="sass">
</style>
