<template>
	<q-layout view="hHh LpR fFf">
		<div v-if="$router.currentRoute.path != '/login'">
			<Navbar @drawerState="drawer = !drawer" />
			<Drawer :drawerTrigger="drawer" />
		</div>
		<Footer v-if="$router.currentRoute.path != '/login'" />
		<q-page-container :class="{' drawer-bg': $router.currentRoute.path == '/login' }">
			<router-view />
		</q-page-container>
	</q-layout>
</template>

<script>
import Navbar from "../components/Navbar";
import Drawer from "../components/Drawer";
import Footer from "../components/Footer";

export default {
	name: "MainLayout",
	components: {
		Navbar,
		Drawer,
		Footer
	},
	data() {
		return {
			drawer: null
		};
	}
};
</script>

<style lang="sass">
.picture-dialog
  width: 80vh
  max-width: 80vw
.navbar-img
  width: 40px
  height: 40px
.drawer-bg
  background-image: url("../assets/nav-bg.svg") !important
  background-position: center center
  background-size: cover
  overflow: hidden !important
.q-item__section--avatar
  cursor: pointer
.border-box-image
  width: 30%
  height: 30%
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 10px
</style>