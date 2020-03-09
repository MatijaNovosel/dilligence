<template>
	<q-drawer :width="225" v-model="leftDrawerOpen" show-if-above bordered content-class="drawer-bg">
		<q-list dense>
			<q-item class="q-my-md">
				<q-item-section avatar>
					<q-avatar size="40px">
						<img src="../assets/default-user.jpg" />
					</q-avatar>
				</q-item-section>
				<q-item-section>
					<q-item-label lines="1">Matija Novosel</q-item-label>
					<q-item-label caption lines="2">
						<span class="text-weight-bold">JMBAG:</span> 0246073749
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
					<q-item
						@click="$router.push(sublink.route)"
						class="text-caption"
						clickable
						v-ripple
					>{{ sublink.text }}</q-item>
				</q-list>
			</q-expansion-item>
		</q-list>
	</q-drawer>
</template>

<script>
export default {
	name: "Drawer",
	props: ["leftDrawerOpen"],
	data() {
		return {
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
	}
};
</script>
