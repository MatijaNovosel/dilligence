<template>
  <v-app v-bind:class="{ bg: this.$router.currentRoute.name == 'login' }">
      <router-view />
  </v-app>
</template>

<script>
export default {
  name: 'App',
  data () {
    return {
    }
  }
}
</script>

<style>
  * {
    font-family: 'Roboto', sans-serif;
  }
  .bg {
    background: url('https://i.imgur.com/gn9elxg.png') !important;
    background-position: center center !important;
    background-size: cover !important;
  }
</style>