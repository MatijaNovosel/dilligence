<template>
  <div id="q-app">
    <router-view />
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "App",
  computed: {
    ...mapGetters(["user"])
  },
  updated() {
    this.$q.dark.set(this.user.settings.darkMode);
  },
  watch: {
    user: {
      deep: true,
      immediate: false,
      handler(oldVal, newVal) {
        if (oldVal.settings.darkMode != newVal.settings.darkMode) {
          this.$q.dark.set(this.user.settings.darkMode);
        }
      }
    }
  }
};
</script>
