<template>
  <q-header elevated>
    <q-toolbar :class="$q.dark.isActive ? 'bg-dark' : 'bg-white'">
      <q-btn
        flat
        dense
        round
        :color="$q.dark.isActive ? 'white' : 'dark'"
        icon="menu"
        aria-label="Menu"
        @click="$emit('drawerState')"
      />
      <q-img class="navbar-img q-ml-md" src="../assets/tvz-logo.svg"></q-img>
      <span :class="($q.dark.isActive ? 'text-white' : 'text-black') + ' text-h6 q-ml-sm'">LMS</span>
      <span class="text-grey q-ml-xs">{{ $t('author') }}</span>
      <q-space />
      <div
        :class="($q.dark.isActive ? 'text-white' : 'text-black') + ' q-pr-md'"
      >Quasar v{{ $q.version }}</div>
      <q-btn
        flat
        dense
        round
        color="grey-8"
        icon="mdi-cog"
        aria-label="Menu"
        @click="$router.push('/settings')"
      />
      <q-btn
        flat
        dense
        round
        :color="$q.dark.isActive ? 'primary' : 'red-7'"
        icon="power_settings_new"
        aria-label="Menu"
        @click="logout"
      />
    </q-toolbar>
  </q-header>
</template>

<script>
import { mapActions } from "vuex";

export default {
  name: "Navbar",
  methods: {
    ...mapActions(["removeUserData"]),
    logout() {
      this.removeUserData();
      this.$q.notify({
        type: "positive",
        message: "Successfully logged out!"
      });
      this.$q.dark.set(false);
      this.$router.push("/login");
    }
  }
};
</script>
