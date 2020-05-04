<template>
  <div>This is the landing page of the course, make sure to keep it pretty for the students!</div>
</template>

<script>
export default {
  name: "CourseDetailsHome",
  created() {
    console.log(this.$route.params.id);
  },
  data() {
    return {};
  }
};
</script>
