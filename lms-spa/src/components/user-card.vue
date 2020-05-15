<template>
  <q-card
    flat
    bordered
    :style="{ 'width': cardWidth }"
    @click="$router.push({ name: 'profile', params: { id: value.id } })"
  >
    <q-menu touch-position context-menu>
      <q-list
        :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
        dense
        separator
        style="min-width: 100px; border-radius: 6px;"
        v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanKickParticipants, Privileges.CanMuteParticipants) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedToCourse)"
      >
        <q-item
          clickable
          v-close-popup
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanMuteParticipants) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedToCourse)"
        >
          <q-item-section>Mute participant</q-item-section>
        </q-item>
        <q-item
          clickable
          v-close-popup
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanKickParticipants) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedToCourse)"
        >
          <q-item-section>Kick participant</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
    <img :src="generateUserPictureSource(value.picture)" />
    <q-card-section>
      <div class="text-h6">{{ `${value.name} ${value.surname}` }}</div>
      <div class="text-subtitle2">{{ `Username: ${value.username}` }}</div>
    </q-card-section>
  </q-card>
</template>

<script>
import { generateUserPictureSource } from "../helpers/helpers";
import UserMixin from "../mixins/userMixin";

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
    generateUserPictureSource
  }
};
</script>
