<template>
  <v-app>
    <Navbar v-show="this.$router.currentRoute.name != 'login'"> </Navbar>
    <!-- Content -->
    <v-content>
      <v-container fluid>
        <router-view />
      </v-container>
    </v-content>
  </v-app>
</template>

<script>
import Navbar from './components/navbar.vue';

export default {
  name: 'App',
  components: {
    Navbar
  },
  data() {
    return { }
  }
};
</script>

<style>
  * {
    font-family: 'Roboto', sans-serif;
  }
</style>