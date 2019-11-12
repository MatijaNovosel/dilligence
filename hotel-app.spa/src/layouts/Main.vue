<template>
  <div>
    <navbar v-if="$store.getters.isAuthenticated" />
    <v-content>
      <v-container fluid>
        <keep-alive>
          <router-view v-if="$route.meta.keepAlive" />
        </keep-alive>
        <router-view v-if="!$route.meta.keepAlive" />
      </v-container>
    </v-content>
    <v-footer app>
      <v-row dense no-gutters>
        <v-col>
          <p>v{{ appData != null ? appData.version : "" }}</p>
        </v-col>
      </v-row>
    </v-footer>
  </div>
</template>

<script>
  import { mapState } from 'vuex';
  import Navbar from "@/components/navbar.vue";

  export default {
    components: {
      Navbar
    },
    data() {
      return {
      }
    },

    computed: {
      ...mapState(['appData']),
      another() {
        return 5;
      }
    },

    mounted() {
      if (this.$store.getters.appData == null) {
        this.$store.dispatch("appData");
      }
    }
  }
</script>
