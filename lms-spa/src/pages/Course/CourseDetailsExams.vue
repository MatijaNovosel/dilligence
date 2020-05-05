<template>
  <div>
    Exams go here!
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          icon="mdi-email-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New exam"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
export default {
  name: "CourseDetailsExams",
  data() {
    return {};
  }
};
</script>
