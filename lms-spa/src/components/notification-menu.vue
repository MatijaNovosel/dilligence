<template>
  <q-btn
    @mouseenter="tooltipHover = true"
    @mouseleave="tooltipHover = false"
    flat
    dense
    round
    :disabled="notificationMenuOpen"
    color="grey-8"
    :icon="(notificationCount && notificationCount > 0) ? 'mdi-bell-ring' : 'mdi-bell'"
    @click="getNotifications"
  >
    <q-tooltip v-model="tooltipHover">
      <span>Notifications</span>
    </q-tooltip>
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
      <q-list separator class="list-border" v-if="notificationCount > 0" :class="$q.dark.isActive ? 'border-dark' : 'border-light'">
        <q-item dense class="justify-center items-center">
          <q-space />
          <span>Notifications</span>
          <q-space />
          <q-btn size="sm" flat round dense icon="mdi-dots-horizontal">
            <q-menu>
              <q-list dense style="border-radius: 6px;" :class="$q.dark.isActive ? 'border-dark' : 'border-light'">
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
              <q-btn round flat size="sm" icon="mdi-eye-off" @click="markAsSeen(notification.id)">
                <q-tooltip>Mark as seen</q-tooltip>
              </q-btn>
              <q-btn
                round
                flat
                size="sm"
                icon="mdi-location-enter"
                @click="$router.push({ name: 'course-details-notifications', params: { id: notification.courseId } })"
              >
                <q-tooltip>Go to course site</q-tooltip>
              </q-btn>
            </q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
      <q-list dense v-else class="list-border" :class="$q.dark.isActive ? 'border-dark' : 'border-light'">
        <q-item class="no-select text-center">
          <q-item-section>No notifications!</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
  </q-btn>
</template>

<script>
import { mapGetters } from "vuex";
import ConnectionMixin from "../mixins/connectionMixin";
import NotificationService from "../services/api/notification";

export default {
  name: "notification-menu",
  mixins: [ConnectionMixin],
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
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
      this.tooltipHover = false;
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
    }
  },
  created() {
    this.startConnection("notification-hub");
    this.connection.on("newNotification", () => {
      this.getNotificationCount();
    });
    this.connection.on("deleteNotification", () => {
      this.getNotificationCount();
    });
    this.getNotificationCount();
  },
  data() {
    return {
      tooltipHover: false,
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
.list-border
  min-width: 350px
  border-radius: 8px
.q-menu
  box-shadow: none
</style>