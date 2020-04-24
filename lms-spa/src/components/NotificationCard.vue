<template>
  <q-card flat bordered>
    <q-bar dense :class="`bg-${color}`">
      <span>{{ value.title }}</span>
    </q-bar>
    <q-card-section class="q-py-sm">
      <span v-html="value.description" />
    </q-card-section>
    <q-separator />
    <q-card-section class="q-py-sm">
      <span class="text-caption">{{ $i18n.t('submittedBy') }}: {{ value.submittedBy }}</span>
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  name: "NotificationCard",
  props: ["value", "color"],
  data() {
    return {};
  },
  methods: {}
};
</script>
