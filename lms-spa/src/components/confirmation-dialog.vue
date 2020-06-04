<template>
  <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="open" persistent>
    <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
      <q-toolbar
        :class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
        class="text-white small-toolbar"
      />
      <q-card-section class="q-py-md text-center">Are you sure?</q-card-section>
      <q-separator />
      <q-card-actions class="justify-center q-py-md">
        <q-btn class="q-mr-sm" color="green-6" size="sm" @click="confirm(true)">Yes</q-btn>
        <q-btn class="q-mr-sm" color="red-8" size="sm" @click="closeDialog">No</q-btn>
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script>
import UserMixin from "../mixins/userMixin";

export default {
  name: "confirmation-dialog",
  mixins: [UserMixin],
  props: ["open"],
  data() {
    return {
      answer: false,
      dialogStyle: {
        width: "40%",
        "max-width": "90vw"
      }
    };
  },
  methods: {
    closeDialog() {
      this.$emit("close");
    },
    confirm() {
      this.$emit("confirm");
    }
  }
};
</script>

<style lang="sass">
.small-toolbar
  min-height: 15px !important
</style>