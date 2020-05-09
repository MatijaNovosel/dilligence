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
      <q-img class="navbar-img q-ml-md" src="../assets/tvz-logo.svg" />
      <span :class="($q.dark.isActive ? 'text-white' : 'text-black') + ' text-h6 q-ml-sm'">LMS</span>
      <span
        v-show="!$q.screen.xs && !$q.screen.sm"
        class="text-grey q-ml-xs"
      >{{ $i18n.t('author') }}</span>
      <q-space />
      <div
        v-show="!$q.screen.xs && !$q.screen.sm"
        class="text-subtitle1"
        :class="($q.dark.isActive ? 'text-white' : 'text-black') + ' q-pr-md'"
      >Quasar v{{ $q.version }}</div>
      <notification-menu />
      <q-btn
        class="q-mx-sm"
        flat
        dense
        round
        color="grey-8"
        icon="mdi-cog"
        @mouseenter="settingsTooltip = true"
        @mouseleave="settingsTooltip = false"
        @click="openSettingsDialog"
      >
        <q-tooltip v-model="settingsTooltip">
          <span>Settings</span>
        </q-tooltip>
      </q-btn>
      <q-btn
        flat
        dense
        round
        class="q-mr-md"
        color="red-7"
        icon="power_settings_new"
        @click="logout"
      >
        <q-tooltip>
          <span>Log out</span>
        </q-tooltip>
      </q-btn>
      <settings-dialog :value="settingsDialog" @closed="settingsDialog = false" />
    </q-toolbar>
  </q-header>
</template>

<script>
import { mapActions } from "vuex";
import SettingsDialog from "../components/settings-dialog";
import NotificationMenu from "../components/notification-menu";

export default {
  name: "navbar",
  components: {
    "settings-dialog": SettingsDialog,
    "notification-menu": NotificationMenu
  },
  methods: {
    ...mapActions(["removeUserData"]),
    openSettingsDialog() {
      [this.settingsDialog, this.settingsTooltip] = [true, false];
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
  data() {
    return {
      settingsDialog: false,
      settingsTooltip: false
    };
  }
};
</script>
