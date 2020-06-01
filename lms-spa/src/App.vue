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