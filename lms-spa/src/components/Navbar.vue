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
      <q-btn
        flat
        dense
        round
        :disabled="notificationMenuOpen"
        color="grey-8"
        :icon="(notificationCount && notificationCount > 0) ? 'mdi-bell-ring' : 'mdi-bell'"
        @click="getNotifications"
      >
        <q-badge
          v-if="notificationCount && notificationCount > 0"
          color="red"
          floating
        >{{ notificationCount }}</q-badge>
        <q-menu
          fit
          v-model="notificationMenuOpen"
          anchor="bottom left"
          self="top left"
          max-height="300px"
        >
          <q-list
            separator
            style="min-width: 350px; border: 1px solid #e0dede; border-radius: 8px;"
            v-if="notificationCount > 0"
          >
            <q-item dense class="justify-center items-center">
              <q-space />
              <span>Notifications</span>
              <q-space />
              <q-btn size="sm" flat round dense icon="mdi-dots-horizontal">
                <q-menu>
                  <q-list dense>
                    <q-item clickable>
                      <q-item-section @click="markAllAsSeen">Mark all as seen</q-item-section>
                    </q-item>
                  </q-list>
                </q-menu>
              </q-btn>
            </q-item>
            <q-item
              class="no-select no-padding-item"
              :key="i"
              v-for="(notification, i) in notifications"
            >
              <q-item-section
                avatar
                :style="{ 
                  'background-color': notification.color, 
                  [i == notifications.length - 1 && 'border-bottom-left-radius']: '8px' 
                }"
                class="q-mr-md color-tag"
              />
              <q-item-section>
                <q-item-label>{{ notification.title }}</q-item-label>
                <q-item-label caption>{{ notification.course }}</q-item-label>
              </q-item-section>
              <q-item-section side>
                <q-item-label>
                  <q-btn
                    round
                    flat
                    size="sm"
                    icon="mdi-eye-off"
                    @click="markAsSeen(notification.id)"
                  >
                    <q-tooltip>Mark as seen</q-tooltip>
                  </q-btn>
                  <q-btn
                    round
                    flat
                    size="sm"
                    icon="mdi-location-enter"
                    @click="$router.push({ name: 'course-details', params: { id: notification.courseId } })"
                  >
                    <q-tooltip>Go to course site</q-tooltip>
                  </q-btn>
                </q-item-label>
              </q-item-section>
            </q-item>
          </q-list>
          <q-list dense style="min-width: 300px" v-else>
            <q-item class="no-select text-center">
              <q-item-section>All clear!</q-item-section>
            </q-item>
          </q-list>
        </q-menu>
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
import ConnectionMixin from "../mixins/connectionMixin";

export default {
  name: "Navbar",
  mixins: [ConnectionMixin],
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    ...mapActions(["removeUserData"]),
    markAsSeen(notificationId) {
      NotificationService.markNotificationAsSeen(
        [notificationId],
        this.user.id
      ).then(() => {
        this.notifications = this.notifications.filter(
          x => x.id != notificationId
        );
        this.getNotificationCount();
      });
    },
    markAllAsSeen() {
      NotificationService.markNotificationAsSeen(
        this.notifications.map(x => x.id),
        this.user.id
      ).then(() => {
        this.notifications = null;
        this.getNotificationCount();
      });
    },
    getNotifications() {
      this.loading = true;
      NotificationService.getNotifications(this.user.id)
        .then(({ data }) => {
          this.notifications = data;
        })
        .finally(() => {
          this.loading = false;
        });
    },
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
    this.startConnection("notification-hub");
    this.connection.on("newNotification", () => {
      this.getNotificationCount();
    });
    this.getNotificationCount();
  },
  data() {
    return {
      notificationMenuOpen: false,
      notificationCount: null,
      notifications: null,
      loading: false
    };
  }
};
</script>

<style lang="sass">
.q-item__section--avatar
  min-width: 10px
.no-padding-item
  padding-top: 0px !important
  padding-bottom: 0px !important
  padding-left: 0px !important
.q-item__section--avatar
  cursor: auto !important
</style>