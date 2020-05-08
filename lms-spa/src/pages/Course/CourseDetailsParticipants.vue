<template>
  <div>
    <div class="row q-gutter-sm justify-center">
      <template v-if="participants">
        <user-card class="user-card" :value="participant" v-for="(participant, i) in participants" :key="i" />
      </template>
    </div>
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
        this.participants = data.results;
      });
    }
  },
  data() {
    return {
      courseId: null,
      participants: null
    };
  }
};
</script>

<style lang="sass">
.user-card
  transition: all .2s ease-in-out;
.user-card:hover
  transform: scale(0.9);
  cursor: pointer
</style>
