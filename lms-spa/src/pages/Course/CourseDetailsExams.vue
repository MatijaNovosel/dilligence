<template>
  <div>
    Exams go here!
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          icon="mdi-email-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New exam"
          @click="createNewExam"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
export default {
  name: "CourseDetailsExams",
  methods: {
    createNewExam() {
      // Create new exam instance, get the id and send it as a parameter to route
      this.$router.push({ name: "exam-edit" });
    }
  },
  data() {
    return {};
  }
};
</script>

<style lang="sass">
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
</style>