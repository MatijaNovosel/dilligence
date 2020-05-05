<template>
  <div>
    <div class="row wrap">
      <div class="col-12">
        <q-option-group
          size="sm"
          v-model="showNotifications"
          :options="showNotificationsOptions"
          type="checkbox"
          color="primary"
          inline
          @input="showNotificationsValueChanged"
        />
        <div
          class="q-ml-md q-mb-md"
          :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
        >* Right click on notifications for more options</div>
      </div>
      <template v-if="notifications && notifications.length != 0">
        <div class="col-12 q-pa-sm" v-for="(notification, i) in notifications" :key="i">
          <notification-card
            @deleteNotification="deleteNotification"
            color="green-5"
            :value="notification"
          />
        </div>
      </template>
      <div v-else class="col-12 q-my-sm">
        <div>{{ $i18n.t('noData') }}</div>
      </div>
    </div>
    <q-dialog v-model="newNotificationDialog" persistent no-esc-dismiss>
      <q-card style="width: 70%; max-width: 90vw;">
        <q-toolbar
          :style="'background-color: ' + newNotification.color"
          class="text-white dialog-toolbar"
        >
          <span>Create new notification</span>
          <q-space />
          <q-btn
            :ripple="false"
            dense
            size="sm"
            color="white"
            flat
            round
            icon="mdi-close-circle"
            @click="resetNewNotificationDialog"
          />
        </q-toolbar>
        <q-card-section class="q-gutter-sm q-pb-none">
          <q-input
            hint="The title of the notification"
            :error="$v.newNotification.title.$invalid"
            error-message="This field is required!"
            dense
            outlined
            v-model="newNotification.title"
            label="Title"
          />
          <q-input
            hint="Color of the header"
            label="Header color"
            dense
            outlined
            v-model="newNotification.color"
            readonly
          >
            <template v-slot:append>
              <q-icon name="colorize" class="cursor-pointer">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-color class="container-border" v-model="newNotification.color" />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-input
            dense
            outlined
            v-model="newNotification.expiresAt"
            :label="$i18n.t('dueDate')"
            readonly
            hint="The date at which the notification becomes archived"
          >
            <template v-slot:append>
              <q-icon name="mdi-calendar-month" class="cursor-pointer">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-date
                    class="container-border"
                    v-model="newNotification.expiresAt"
                    mask="YYYY-MM-DD"
                  />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-editor
            :content-style="{ [$v.newNotification.description.$invalid && 'border']: '1px solid #C10015' }"
            v-model="newNotification.description"
            min-height="5rem"
          />
          <div
            v-if="$v.newNotification.description.$invalid"
            class="error-text q-pl-sm"
          >This field is required!</div>
          <div
            v-else
            class="q-pl-sm"
            :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
          >The main text of the notification</div>
          <q-checkbox
            v-model="newNotification.sendEmail"
            size="xs"
            class="q-mt-none"
            label="Send email to subscribed users"
          />
        </q-card-section>
        <q-card-actions class="justify-end q-pt-none">
          <q-btn
            :disabled="$v.newNotification.$invalid"
            class="q-mr-sm"
            color="primary"
            size="sm"
            @click="createNotification"
          >Send</q-btn>
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          @click="newNotificationDialog = true"
          icon="mdi-email-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New notification"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
import CourseService from "../../services/api/course";
import NotificationService from "../../services/api/notification";
import NotificationCard from "../../components/notification-card";
import { required, minLength } from "vuelidate/lib/validators";
import UserMixin from "../../mixins/userMixin";
import { debounce } from "debounce";

export default {
  name: "CourseDetailsHome",
  components: {
    "notification-card": NotificationCard
  },
  mixins: [UserMixin],
  created() {
    this.courseId = this.$route.params.id;
    this.getNotifications();
  },
  validations: {
    newNotification: {
      title: {
        required,
        minLength: minLength(4)
      },
      description: {
        required,
        minLength: minLength(4)
      }
    }
  },
  data() {
    return {
      showNotifications: [0],
      showNotificationsOptions: [
        {
          label: "Show non archived",
          value: 0
        },
        {
          label: "Show archived",
          value: 1
        }
      ],
      newNotificationDialog: false,
      notifications: null,
      courseId: null,
      newNotification: {
        title: "Title goes here!",
        description: "Description goes here!",
        color: "#285de0",
        expiresAt: "2020-07-20",
        sendEmail: false
      }
    };
  },
  methods: {
    showNotificationsValueChanged: debounce(function() {
      this.getNotifications();
    }, 1500),
    createNotification() {
      let notification = {
        ...this.newNotification,
        submittedById: this.user.id,
        courseId: this.courseId
      };
      NotificationService.createNotification(notification).then(() => {
        this.getNotifications(this.courseId);
        this.resetNewNotificationDialog();
      });
    },
    resetNewNotificationDialog() {
      this.newNotification = {
        title: "Title goes here!",
        description: "Description goes here!",
        color: "#285de0",
        expiresAt: "2020-07-20",
        sendEmail: false
      };
      this.newNotificationDialog = false;
    },
    getNotifications() {
      CourseService.getNotifications(
        this.courseId,
        this.showNotifications.includes(1),
        this.showNotifications.includes(0)
      ).then(({ data }) => {
        this.notifications = data;
      });
    },
    deleteNotification(id) {
      console.log(id);
    }
  },
  watch: {
    showNotifications: {
      deep: true,
      immediate: false,
      handler(newVal, oldVal) {
        // Never allow the value to be empty!!
        if (newVal.length == 0) {
          this.showNotifications = oldVal;
        }
      }
    }
  }
};
</script>

<style lang="sass">
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
.dialog-toolbar
  min-height: 30px
.container-border
  border: 1px solid #e0dede;
</style>
