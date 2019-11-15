<template>
  <div>
    <!-- Navbar -->
    <Navbar v-show="this.$router.currentRoute.name != 'login'"> </Navbar>
    <!-- Content -->
    <v-content>
      <v-container fluid>
        <router-view />
      </v-container>
    </v-content>
    <!-- Notifications -->
    <notifications position="bottom right" group="notification" class="mt-2" />
  </div>
</template>

<script>
  import Navbar from "@/components/navbar.vue";

  export default {
    components: {
      Navbar
    },
    data() {
      return { }
    },
    computed: { },
    mounted() { }
  }
</script>
