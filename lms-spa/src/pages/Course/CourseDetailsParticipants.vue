<template>
  <div>
    <div class="row">
      <div class="col-12 q-mb-md">
        <div class="row q-col-gutter-sm">
          <div class="col-xs-12 col-md-4">
            <q-input
              @input="inputChanged"
              outlined
              v-model="searchData.name"
              dense
              label="Name"
              clearable
            />
          </div>
          <div class="col-xs-12 col-md-4">
            <q-input
              @input="inputChanged"
              outlined
              v-model="searchData.surname"
              dense
              label="Surname"
              clearable
            />
          </div>
          <div class="col-xs-12 col-md-4">
            <q-input
              @input="inputChanged"
              outlined
              v-model="searchData.username"
              dense
              label="Username"
              clearable
            />
          </div>
          <div class="q-ml-md" :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']">
            *
            <q-icon size="xs" class="q-mr-xs" name="mdi-mouse" />Right click on participants (or long tap on phones) for more options
          </div>
        </div>
      </div>
      <template v-if="participants">
        <user-card
          class="user-card q-mx-sm"
          :courseId="courseId"
          :value="participant"
          v-for="(participant, i) in participants"
          :key="i"
        />
      </template>
    </div>
  </div>
</template>

<script>
import CourseService from "../../services/api/course";
import UserCard from "../../components/user-card";
import { debounce } from "debounce";

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
      CourseService.getCourseUsers(
        this.courseId,
        this.searchData.name,
        this.searchData.surname,
        this.searchData.username
      ).then(({ data }) => {
        this.participants = data.results;
      });
    },
    inputChanged: debounce(function() {
      this.getUsers();
    }, 1000)
  },
  data() {
    return {
      courseId: null,
      participants: null,
      searchData: {
        name: null,
        surname: null,
        username: null
      }
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
