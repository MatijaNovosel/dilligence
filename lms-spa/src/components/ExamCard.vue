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
        <q-btn flat size="sm" class="bg-red-4 text-white" @click="unsubscribe(props.row.id)">START</q-btn>
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
.border-box
  position: relative
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 10px
  width: 100%
.bottom-left
  position: absolute
  bottom: 20px
  left: 20px
  z-index: 200
.aside
  position: absolute
  right: 5px
</style>
