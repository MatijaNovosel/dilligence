<template>
  <q-layout view="hHh LpR fFf">
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
.picture-dialog
	width: 80vh
	max-width: 80vw
.navbar-img
	width: 40px
	height: 40px
.q-item__section--avatar
	cursor: pointer
.border-box-image
	width: 30%
	height: 30%
	border: 1px solid rgba(0, 0, 0, 0.12)
	border-radius: 10px
</style>