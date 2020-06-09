<template>
  <div class="justify-center">
    <div>Grades go here!</div>
  </div>
</template>

<script>
import UserMixin from "../../mixins/userMixin";

export default {
  name: "CourseDetailsGrades",
  mixins: [UserMixin],
  created() {
    this.courseId = this.$route.params.id;
  },
  methods: {},
  data() {
    return {};
  }
};
</script>
