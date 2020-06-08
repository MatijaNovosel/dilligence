<template>
  <div id="q-app">
    <div class="row text-center justify-center q-mt-lg" v-if="wrongPlatform">
      <div class="col-12 text-h5 q-mb-lg">Please install a normal browser!</div>
      <div class="col-12">https://www.google.com/chrome/</div>
      <div class="col-12">https://www.mozilla.org/en-US/firefox/new/</div>
      <div class="col-12">https://brave.com/download/</div>
      <div class="col-12">https://www.opera.com/download</div>
    </div>
    <router-view v-else />
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import { Platform } from "quasar";

export default {
  name: "App",
  computed: {
    ...mapGetters(["user"]),
    wrongPlatform() {
      return Platform.is.edge || Platform.is.safari || Platform.is.ie;
    }
  },
  updated() {
    this.$q.dark.set(this.user.settings.darkMode);
    this.$i18n.locale = this.user.settings.locale;
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

<style lang="sass">
.q-body--force-scrollbar
	overflow-y: hidden !important;
</style>