<template>
	<q-layout view="hHh lpR fff">
    <div v-if="$router.currentRoute.path != '/login'">
      <Navbar @drawerState="drawer = !drawer" />
		  <Drawer :drawerTrigger="drawer" />
    </div>
		<q-page-container :class="{' drawer-bg': $router.currentRoute.path == '/login' }">
			<router-view />
		</q-page-container>
	</q-layout>
</template>

<script>
import Navbar from "../components/Navbar";
import Drawer from "../components/Drawer";

export default {
	name: "MainLayout",
	components: {
		Navbar,
		Drawer
  },
	data() {
		return {
			drawer: null
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