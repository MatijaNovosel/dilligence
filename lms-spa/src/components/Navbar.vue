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
      <span class="text-grey q-ml-xs">{{ $i18n.t('author') }}</span>
      <q-space />
      <div
        class
        :class="($q.dark.isActive ? 'text-white' : 'text-black') + ' q-pr-md'"
      >Quasar v{{ $q.version }}</div>
      <q-btn flat dense round color="grey-8" icon="mdi-bell">
        <q-badge color="red" floating>{{ notificationCount }}</q-badge>
      </q-btn>
      <q-btn
        class="q-mx-sm"
        flat
        dense
        round
        color="grey-8"
        icon="mdi-cog"
        @click="$router.push('/settings')"
      />
      <q-btn
        flat
        dense
        round
        class="q-mr-md"
        :color="$q.dark.isActive ? 'primary' : 'red-7'"
        icon="power_settings_new"
        @click="logout"
      />
    </q-toolbar>
  </q-header>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import NotificationService from "../services/api/notification";

export default {
  name: "Navbar",
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    ...mapActions(["removeUserData"]),
    getNotificationCount() {
      NotificationService.getTotalNotifications(this.user.id).then(
        ({ data }) => {
          this.notificationCount = data;
        }
      );
    },
    logout() {
      this.removeUserData();
      this.$q.notify({
        type: "positive",
        message: this.$i18n.t("successfullyLoggedOut")
      });
      this.$q.dark.set(false);
      this.$router.push("/login");
    }
  },
  created() {
    this.getNotificationCount();
  },
  data() {
    return {
      notificationCount: null
    };
  }
};
</script>
