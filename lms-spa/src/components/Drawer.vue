<template>
	<q-drawer :width="250" v-model="drawerOpen" show-if-above bordered content-class="drawer-bg">
		<q-list dense>
			<q-item class="q-my-md">
				<q-item-section avatar>
					<q-avatar size="40px" @click="$emit('avatarClicked')">
						<img src="../assets/default-user.jpg" />
					</q-avatar>
				</q-item-section>
				<q-item-section>
					<q-item-label lines="1">{{ `${user.name} ${user.surname}` }}</q-item-label>
					<q-item-label caption lines="2">
						<span class="text-weight-bold">JMBAG:</span>
						{{ user.jmbag }}
					</q-item-label>
				</q-item-section>
			</q-item>
			<q-separator />
			<q-expansion-item
				group="drawer"
				dense
				dense-toggle
				class="text-weight-regular"
				v-for="link in links"
				:label="link.text"
				:key="link.text"
			>
				<q-list :key="i" dense v-for="(sublink, i) in link.sublinks">
					<q-item @click="redirect(sublink.route)" class="text-caption" clickable v-ripple>
						<span class="q-mt-xs q-pl-md">{{ sublink.text }}</span>
					</q-item>
				</q-list>
			</q-expansion-item>
		</q-list>
	</q-drawer>
</template>

<script>
import { mapGetters } from "vuex";

export default {
	name: "Drawer",
	props: ["drawerTrigger"],
	data() {
		return {
			drawerOpen: false,
			links: [
				{
					icon: "mdi-bullhorn",
					text: "Generalno",
					route: { name: "/" },
					sublinks: [
						{
							text: "Početna stranica",
							route: { name: "home" }
						},
						{
							text: "Popis zaposlenika",
							route: { name: "employees" }
						},
						{
							text: "Test",
							route: { name: "test" }
						}
					]
				},
				{
					icon: "mdi-file-document",
					text: "Kolegiji",
					route: { name: "subjects" },
					sublinks: [
						{
							text: "Popis kolegija",
							route: { name: "subjects" }
						},
						{
							text: "Moji kolegiji",
							route: { name: "my-subjects" }
						}
					]
				},
				{
					icon: "mdi-test-tube",
					text: "Zadatci",
					route: { name: "exams" },
					sublinks: [
						{
							text: "Exams",
							route: { name: "exams" }
						}
					]
				}
			]
		};
	},
	methods: {
		redirect(route) {
			if (this.$router.currentRoute.name === route.name) {
				return;
			}
			this.$router.push(route);
		}
	},
	watch: {
		drawerTrigger() {
			this.drawerOpen = !this.drawerOpen;
		}
	},
	computed: {
		...mapGetters(["user"])
	}
};
</script>

<style scoped>
.q-drawer {
	background: none;
}
</style>
