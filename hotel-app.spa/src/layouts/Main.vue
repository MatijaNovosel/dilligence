<template>
  <div>
    <!-- Navbar -->
    <Navbar v-show="this.$router.currentRoute.name != 'login'"> </Navbar>
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
  import Navbar from "@/components/Navbar.vue";

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
