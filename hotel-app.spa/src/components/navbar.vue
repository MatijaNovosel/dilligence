<template>
	<div>
		<v-navigation-drawer disable-route-watcher app clipped v-model="drawer" disable-resize-watcher>
			<v-list dense nav>
				<v-list-item @click="$router.push('/account')">
					<v-list-item-avatar class="avatar-border">
						<v-img src="../assets/default-user.jpg" />
					</v-list-item-avatar>
					<v-list-item-content>
						<v-list-item-title>{{ `${user.name} ${user.surname}` }}</v-list-item-title>
						<v-list-item-subtitle>{{ `JMBAG: ${user.jmbag}` }}</v-list-item-subtitle>
					</v-list-item-content>
				</v-list-item>
				<v-divider />
				<v-list-group v-for="link in links" :key="link.text" no-action>
					<template v-slot:activator>
						<v-list-item active-class="highlighted" class="px-0">
							<v-list-item-action>
								<v-icon>{{ link.icon }}</v-icon>
							</v-list-item-action>
							<v-list-item-content class="ml-n5">
								<v-list-item-title>{{ link.text }}</v-list-item-title>
							</v-list-item-content>
						</v-list-item>
					</template>
					<v-list-item
						v-for="sublink in link.sublinks"
						:key="sublink.text"
						nav
						@click="sublink.route != null ? goToUrl(sublink.route) : ''"
						active-class="highlighted"
						:class="sublink.route != null && sublink.route.name === $route.name ? 'highlighted' : ''"
					>
						<v-list-item-content class="ml-n5">
							<v-list-item-title>
								<v-icon class="mr-2" size="15">mdi-circle-slice-8</v-icon>
								{{ sublink.text }}
							</v-list-item-title>
						</v-list-item-content>
					</v-list-item>
				</v-list-group>
				<v-divider class="my-2" />
			</v-list>
		</v-navigation-drawer>
		<v-app-bar app clipped-left>
			<v-app-bar-nav-icon @click.stop="drawer = !drawer" />
			<v-toolbar-title class="mt-1">
				<v-avatar size="40" class="mr-5">
					<v-img src="../assets/tvz-logo.svg" />
				</v-avatar>
				<span class="title">LMS</span>
				<span class="font-weight-light subtitle-2">_by Matija</span>
			</v-toolbar-title>
			<v-spacer />
			<v-col cols="2">
				<v-menu nudge-bottom="40" bottom origin="center center" v-model="searchMenu">
					<template v-slot:activator="{ }">
						<v-text-field dense class="mt-7 txt-fld" outlined v-model="searchText" clearable />
					</template>
					<v-list flat dense>
						<v-list-item v-for="subject in subjects" :key="subject.id + subject.naziv">
							<v-list-item-title>{{ subject.naziv }}</v-list-item-title>
						</v-list-item>
					</v-list>
				</v-menu>
			</v-col>
			<v-btn icon color="grey" @click="searchSubjects">
				<v-icon size="30">mdi-magnify</v-icon>
			</v-btn>
			<v-menu offset-y>
				<template v-slot:activator="{ on }">
					<v-btn icon color="grey" v-on="on">
						<v-icon size="30">mdi-bell</v-icon>
					</v-btn>
				</template>
				<v-list>
					<v-list-item v-for="(item, i) in notifications" :key="i" router to="/notification">
						<v-list-item-title>{{ item.title }}</v-list-item-title>
						<v-list-item-action>
							<v-icon color="blue">mdi-information</v-icon>
						</v-list-item-action>
					</v-list-item>
				</v-list>
			</v-menu>
			<v-tooltip v-model="show" bottom>
				<template v-slot:activator="{ on }">
					<v-btn icon @click="logout" v-on="on" class="mx-3">
						<v-icon color="red" size="30">mdi-power</v-icon>
					</v-btn>
				</template>
				<span>Log out</span>
			</v-tooltip>
		</v-app-bar>
		<key-listener keyCode="27" event="keydown" @pressed="searchMenu = false"></key-listener>
	</div>
</template>

<script>
import { mapGetters } from "vuex";
import AuthService from "../services/api/auth";
import KolegijService from "../services/api/kolegij";
import KeyListener from "../components/KeyListener";

export default {
	components: {
		KeyListener
	},
	data() {
		return {
			drawer: false,
			searchMenu: false,
			searchText: null,
			show: false,
			subjects: null,
			notifications: [
				{ title: "Baze podataka - nova vijest" },
				{ title: "Baze podataka - novi privitak" },
				{ title: "Nova obavijest studentske referade" }
			],
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
	computed: {
		...mapGetters(["user"])
	},
	methods: {
		async logout() {
			this.drawer = false;
			await AuthService.logout();
			this.$router.push("/login");
		},
		goToUrl(route) {
			if (this.$router.currentRoute.name == route.name) {
				return;
			}
			this.drawer = false;
			this.$router.push(route);
		},
		getSubjectData() {
			KolegijService.getKolegiji(
				null,
				this.searchText,
				null,
				null,
				null,
				0,
				null
			).then(({ data }) => {
				this.subjects = data.results;
			});
		},
		searchSubjects() {
			this.searchMenu = true;
			this.getSubjectData();
		}
	}
};
</script>

<style>
.avatar-border {
	border: 1px solid #d1d1c2;
}
.v-navigation-drawer {
	background-image: url("../assets/nav-bg.svg");
	background-position: center center;
	background-size: cover;
}
.v-list .v-list-item--active {
	color: #007bff !important;
}
.v-list .v-list-item--active .v-icon {
	color: #007bff !important;
}
.v-menu__content {
	box-shadow: 0px 5px 5px -3px rgba(0, 0, 0, 0.2),
		0px 0px 0px 0px rgba(0, 0, 0, 0.14), 0px 0px 0px 2px rgba(0, 0, 0, 0.12);
}
</style>