<template>
  <div>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          icon="mdi-newspaper-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New task"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
export default {
  name: "CourseDetailsTasks",
  data() {
    return {};
  }
};
</script>
