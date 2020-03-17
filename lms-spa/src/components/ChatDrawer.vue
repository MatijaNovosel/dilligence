<template>
	<q-drawer
		:mini="miniState"
		mini-to-overlay
		@mouseover="miniState = false"
		@mouseout="miniState = true"
		side="right"
		:width="250"
		show-if-above
		bordered
	>
    <q-separator />
		<q-list separator class="rounded-borders" style="max-width: 350px">
			<template v-for="(user, index) in 10">
				<q-item clickable v-ripple :key="user + index">
					<q-item-section avatar>
						<q-avatar>
							<img src="../assets/default-user.jpg" />
						</q-avatar>
					</q-item-section>
					<q-item-section>
						<q-item-label lines="1">Ivica Todoric</q-item-label>
					</q-item-section>
				</q-item>
			</template>
		</q-list>
    <q-separator />
	</q-drawer>
</template>

<script>
import { mapGetters } from "vuex";

export default {
	name: "ChatDrawer",
	props: ["drawerTrigger"],
	data() {
		return {
			miniState: true,
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
