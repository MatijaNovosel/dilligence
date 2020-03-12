<template>
	<q-header elevated>
		<q-toolbar class="bg-white">
			<q-btn
				flat
				dense
				round
				color="primary"
				icon="menu"
				aria-label="Menu"
				@click="$emit('drawerState')"
			/>
			<q-img class="navbar-img q-ml-md" src="../assets/tvz-logo.svg"></q-img>
			<span class="text-black text-h6 q-ml-sm">LMS</span>
			<span class="text-grey q-ml-xs">by Matija</span>
			<q-space />
			<div class="text-black q-pr-md">Quasar v{{ $q.version }}</div>
			<q-btn
				flat
				dense
				round
				color="red-7"
				icon="power_settings_new"
				aria-label="Menu"
				@click="logout"
			/>
		</q-toolbar>
	</q-header>
</template>

<script>
import { mapActions } from "vuex";

export default {
	name: "Navbar",
	methods: {
		...mapActions(["removeUserData"]),
		logout() {
			this.removeUserData();
			this.$q.notify({
				type: "positive",
				message: "Successfully logged out!"
			});
			this.$router.push("/login");
		}
	}
};
</script>
