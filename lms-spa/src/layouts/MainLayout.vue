<template>
	<q-layout view="hHh lpR fff">
		<q-header elevated>
			<q-toolbar class="bg-white">
				<q-btn
					flat
					dense
					round
					color="primary"
					icon="menu"
					aria-label="Menu"
					@click="leftDrawerOpen = !leftDrawerOpen"
				/>
				<q-img class="navbar-img q-ml-md" src="../assets/tvz-logo.svg"></q-img>
				<span class="text-black text-h6 q-ml-sm">LMS</span>
				<span class="text-grey q-ml-xs">by Matija</span>
				<q-toolbar-title>Quasar App</q-toolbar-title>
				<div>Quasar v{{ $q.version }}</div>
			</q-toolbar>
		</q-header>
		<q-drawer width="225" v-model="leftDrawerOpen" show-if-above bordered content-class="drawer-bg">
			<div class="row justify-center q-my-lg">
				<q-avatar size="62px">
					<img src="https://i.kym-cdn.com/photos/images/newsfeed/001/336/309/b85.jpg" />
				</q-avatar>
			</div>
			<q-list dense>
				<q-item
					active-class="text-red-7"
					clickable
					:to="link.route"
					v-for="link in links"
					:key="link.text"
				>
					<q-item-section avatar>
						<q-icon :name="link.icon" />
					</q-item-section>
					<q-item-section>
						<q-item-label>{{ link.text }}</q-item-label>
					</q-item-section>
				</q-item>
			</q-list>
		</q-drawer>
		<q-page-container>
			<router-view />
		</q-page-container>
	</q-layout>
</template>

<script>
export default {
	name: "MainLayout",
	data() {
		return {
			leftDrawerOpen: false,
			links: [
				{
					text: "Home",
					route: "/home",
					icon: "mdi-home"
				},
				{
					text: "Play",
					route: "/play",
					icon: "mdi-play"
				},
				{
					text: "Tutorials",
					route: "/tutorial",
					icon: "mdi-school"
				},
				{
					text: "My collection",
					route: "/collection",
					icon: "mdi-cards"
				},
				{
					text: "Market",
					route: "/market",
					icon: "mdi-shopping"
				},
				{
					text: "Account settings",
					route: "/account",
					icon: "mdi-cogs"
				}
			]
		};
	}
};
</script>

<style lang="sass">
.navbar-img
  width: 40px
  height: 40px
.drawer-bg
  background-image: url("../assets/nav-bg.svg") !important
  background-position: center center
  background-size: cover
  overflow: hidden !important
</style>