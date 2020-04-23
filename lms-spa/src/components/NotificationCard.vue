<template>
  <q-card flat bordered>
    <q-card-section class="q-py-sm">
      <div class="text-overline">{{ notificationData.name }}</div>
      <span class="ellipsis-text">{{ notificationData.description }}</span>
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  name: "NotificationCard",
  props: ["notificationData"],
  data() {
    return {};
  },
  methods: {}
};
</script>
