<template>
  <q-card
    flat
    bordered
    :style="{ 'width': cardWidth }"
    @click="$router.push({ name: 'profile', params: { id: value.id } })"
  >
    <img :src="generateUserPictureSource(value.picture)" />
    <q-card-section>
      <div class="text-h6">{{ `${value.name} ${value.surname}` }}</div>
      <div class="text-subtitle2">{{ `Username: ${value.username}` }}</div>
    </q-card-section>
  </q-card>
</template>

<script>
import { generateUserPictureSource } from "../helpers/helpers";

export default {
  name: "user-card",
  props: ["value"],
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
