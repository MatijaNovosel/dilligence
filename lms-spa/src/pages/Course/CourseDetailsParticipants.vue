<template>
  <div>
    <template v-if="users">
      <user-card :value="user" v-for="(user, i) in users" :key="i" />
    </template>
  </div>
</template>

<script>
import CourseService from "../../services/api/course";
import UserCard from "../../components/user-card";

export default {
  name: "CourseDetailsParticipants",
  components: {
    "user-card": UserCard
  },
  created() {
    this.courseId = this.$route.params.id;
    this.getUsers();
  },
  methods: {
    getUsers() {
      CourseService.getCourseUsers(this.courseId).then(({ data }) => {
        this.users = data.results;
      });
    }
  },
  data() {
    return {
      courseId: null,
      users: null
    };
  }
};
</script>
