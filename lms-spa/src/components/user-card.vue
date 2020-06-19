<template>
  <q-card flat bordered :style="{ 'width': cardWidth }">
    <q-menu touch-position context-menu v-if="user.id != value.id">
      <q-list
        :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
        dense
        separator
        style="min-width: 100px; border-radius: 6px;"
        v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanKickParticipants, Privileges.CanMuteParticipants) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
      >
        <q-item
          clickable
          v-close-popup
          @click="muteParticipant(value.id)"
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanMuteParticipants) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
        >
          <q-item-section>{{ value.muted ? 'Unmute participant' : 'Mute participant' }}</q-item-section>
        </q-item>
        <q-item
          clickable
          v-close-popup
          @click="$router.push({ name: 'profile', params: { id: value.id } })"
        >
          <q-item-section>View profile</q-item-section>
        </q-item>
        <q-item
          clickable
          v-close-popup
          @click="kickParticipant(value.id)"
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanKickParticipants) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
        >
          <q-item-section>Kick participant</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
    <q-icon
      class="absolute-top-right"
      size="sm"
      color="red-8"
      name="mdi-volume-off"
      v-show="value.muted && !value.admin"
    />
    <img style="width: 180px; height: 180px;" :src="generatePictureSource(value.picture)" />
    <q-card-section>
      <div class="text-h6">{{ `${value.name} ${value.surname}` }}</div>
      <div class="text-subtitle2">{{ `Username: ${value.username}` }}</div>
    </q-card-section>
  </q-card>
</template>

<script>
import { generatePictureSource } from "../helpers/helpers";
import UserMixin from "../mixins/userMixin";
import CourseService from "../services/api/course";

export default {
  name: "user-card",
  props: ["value", "courseId"],
  mixins: [UserMixin],
  data() {
    return {};
  },
  computed: {
    cardWidth() {
      if (this.$q.screen.xs) {
        return "10em";
      }
      return "13em";
    }
  },
  methods: {
    generatePictureSource,
    kickParticipant(id) {
      CourseService.kickParticipant({
        userId: id,
        courseId: this.courseId
      }).then(() => {
        this.$emit("mute");
      });
    },
    muteParticipant(id) {
      let payload = {
        courseId: this.courseId,
        userId: this.value.id,
        mute: !this.value.muted
      };
      CourseService.muteParticipant(payload).then(() => {
        this.$emit("mute");
      });
    }
  }
};
</script>
