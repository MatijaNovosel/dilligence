<template>
  <div>
    <!-- Navbar -->
    <navbar v-show="this.$router.currentRoute.name != 'login'"> </navbar>
    <!-- Content -->
    <v-content>
      <v-container fluid>
        <keep-alive>
          <router-view v-if="$route.meta.keepAlive" />
        </keep-alive>
        <router-view v-if="!$route.meta.keepAlive" />
      </v-container>
    </v-content>
    <!-- Notifications -->
    <notifications position="bottom right" group="notification" class="mt-2" />
  </div>
</template>

<script>
  import navbar from "@/components/navbar.vue";

  export default {
    components: {
      navbar
    },
    data() {
      return { }
    },
  }
</script>
