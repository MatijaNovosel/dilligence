<template>
  <q-card>
    <q-menu touch-position context-menu>
      <q-list
        :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
        dense
        separator
        style="min-width: 100px; border-radius: 6px;"
      >
        <q-item clickable v-close-popup @click="deleteNotificationTrigger">
          <q-item-section>Delete notification</q-item-section>
        </q-item>
        <q-item clickable v-close-popup>
          <q-item-section>Archive notification</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
    <q-bar dense :style="`background-color: ${value.color}; height: 10px;`" />
    <q-card-section class="q-py-sm">
      <div class="text-h6">{{ value.title }}</div>
      <div
        class="text-subtitle2"
      >{{ `${value.submittedBy}, ${$options.filters.dateFilter(value.submittedAt)}` }}</div>
    </q-card-section>
    <q-separator />
    <q-card-section class="q-py-sm">
      <span v-html="value.description" />
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  name: "notification-card",
  props: ["value"],
  data() {
    return {};
  },
  methods: {
    deleteNotificationTrigger() {
      this.$emit("deleteNotification", this.value.id);
    }
  }
};
</script>
