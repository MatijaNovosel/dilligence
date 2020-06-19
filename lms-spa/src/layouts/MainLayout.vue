<template>
  <q-layout view="hHh LpR fFf">
    <div v-if="!['/login', '/register'].includes($router.currentRoute.path)">
      <navbar @drawerState="drawer = !drawer" />
      <drawer :drawerTrigger="drawer" />
    </div>
    <q-page-container
      :class="{'svg-bg': ['/login', '/register'].includes($router.currentRoute.path) }"
    >
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script>
import Navbar from "../components/navbar";
import Drawer from "../components/drawer";

export default {
  name: "MainLayout",
  components: {
    navbar: Navbar,
    drawer: Drawer
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
  width: 30px
  height: 30px
.q-item__section--avatar
  cursor: pointer
.border-box-image
  width: 30%
  height: 30%
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 10px
.svg-bg
  background-position: center center
  background-image: url("../assets/nav-bg.svg")
  background-size: cover
  overflow: hidden
</style>