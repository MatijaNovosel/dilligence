<template>
  <q-card flat bordered style="width: 10em">
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
  methods: {
    generateUserPictureSource
  }
};
</script>
