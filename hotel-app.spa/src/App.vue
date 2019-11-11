<template>
  <v-app v-bind:class="{ bg: this.$router.currentRoute.name == 'login' }">
    <!-- Navbar -->
    <Navbar v-show="this.$router.currentRoute.name != 'login'"> </Navbar>
    <!-- Content -->
    <v-content>
      <v-container fluid>
        <router-view />
      </v-container>
    </v-content>
    <!-- Notifications -->
    <notifications group="notification" class="mt-2" />
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
  .bg {
    background: url('https://i.imgur.com/gn9elxg.png') !important;
  }
</style>