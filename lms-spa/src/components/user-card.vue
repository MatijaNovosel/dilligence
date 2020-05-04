<template>
  <q-card flat bordered style="width: 10em">
    <img
      :src="value.picture == null ? require('../assets/default-user.jpg') : 'data:image/png;base64,' + value.picture"
    />
    <q-card-section>
      <div class="text-h6">{{ `${value.name} ${value.surname}` }}</div>
      <div class="text-subtitle2">{{ `Username: ${value.username}` }}</div>
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  name: "user-card",
  props: ["value"],
  data() {
    return {};
  },
  methods: {}
};
</script>
