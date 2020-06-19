<template>
  <div>
    <div class="row">
      <div class="col-12">
        <div class="row q-col-gutter-sm">
          <q-option-group
            size="sm"
            v-model="showNotifications"
            :options="showNotificationsOptions"
            type="checkbox"
            color="primary"
            inline
            @input="showNotificationsValueChanged"
          />
          <q-toggle
            @input="showNotificationsValueChanged"
            v-model="sortByNew"
            size="sm"
            label="Sort by newer"
          />
        </div>
        <div class="q-ml-md q-mb-md" :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']">
          *
          <q-icon size="xs" class="q-mr-xs" name="mdi-mouse" />Right click on notifications (or long tap on phones) for more options
        </div>
      </div>
      <q-skeleton v-show="notificationLoading" class="q-mx-sm" width="100%" height="150px" square />
      <template v-if="notifications && notifications.length != 0">
        <div class="col-12 q-pa-sm" v-for="(notification, i) in notifications" :key="i">
          <notification-card
            @delete="deleteNotification"
            @archive="archiveNotification"
            color="green-5"
            :value="notification"
            :courseId="courseId"
          />
        </div>
      </template>
      <div v-show="!notificationLoading" v-else class="col-12 q-my-sm">
        <div>{{ $i18n.t('noData') }}</div>
      </div>
    </div>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="newNotificationDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar
          :style="`border-bottom: 1px solid ${$q.dark.isActive ? 'rgba(255, 255, 255, 0.6)' : 'rgba(0, 0, 0, 0.12)'};`"
        >
          <span style="font-size: 18px;">Create new notification</span>
          <q-space />
          <q-btn
            @click="resetNewNotificationDialog"
            :ripple="false"
            dense
            size="sm"
            flat
            round
            icon="mdi-close-thick"
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
              <q-icon
                :style="{ color: newNotification.color }"
                name="colorize"
                class="cursor-pointer"
              >
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-color no-header class="container-border" v-model="newNotification.color" />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-input
            dense
            outlined
            v-model="newNotification.expiresAt"
            :error="$v.newNotification.expiresAt.$invalid"
            error-message="The date must not be before the current date!"
            :label="$i18n.t('dueDate')"
            readonly
            hint="The date at which the notification becomes archived"
          >
            <template v-slot:prepend>
              <q-icon name="mdi-calendar-month" class="cursor-pointer">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-date
                    minimal
                    class="container-border"
                    v-model="newNotification.expiresAt"
                    mask="YYYY-MM-DD HH:mm"
                  />
                </q-popup-proxy>
              </q-icon>
            </template>
            <template v-slot:append>
              <q-icon name="mdi-alarm" class="cursor-pointer">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-time
                    :class="$q.dark.isActive ? 'border-dark' : 'border-light'"
                    v-model="newNotification.expiresAt"
                    mask="YYYY-MM-DD HH:mm"
                    format24h
                  />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-file
            dense
            multiple
            outlined
            v-model="newNotification.files"
            class="q-pr-sm"
            clearable
            label="Attachments"
          >
            <template v-slot:append>
              <q-icon name="mdi-paperclip" />
            </template>
          </q-file>
          <q-editor
            :content-style="{ [$v.newNotification.description.$invalid && $v.newNotification.description.$dirty && 'border']: '1px solid #C10015' }"
            v-model="newNotification.description"
            @input="$v.newNotification.description.$touch()"
            min-height="5rem"
          />
          <div
            v-if="$v.newNotification.description.$invalid && $v.newNotification.description.$dirty"
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
          <q-checkbox
            v-model="newNotification.addFilesToCourse"
            size="xs"
            class="q-mt-none"
            label="Add attachments to course files"
            :disable="!newNotification.files"
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
    <q-page-sticky
      position="bottom-right"
      :offset="[18, 18]"
      v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageNotifications, Privileges.CanSendNotifications) && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
    >
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
import { format, add } from "date-fns";
import {
  mustBeBeforeCurrentDate,
  mustNotBeEmptyHtml,
  clearedHtmlMustBeAtLeastNCharacters
} from "../../helpers/helpers";

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
        mustNotBeEmptyHtml,
        clearedHtmlMustBeAtLeastNCharacters
      },
      expiresAt: {
        mustBeBeforeCurrentDate
      }
    }
  },
  data() {
    return {
      sortByNew: true,
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
      notificationLoading: false,
      newNotificationDialog: false,
      notifications: null,
      courseId: null,
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      },
      newNotification: {
        title: "Title goes here!",
        description: "Description goes here!",
        color: "#285de0",
        expiresAt: format(add(new Date(), { days: 1 }), "yyyy-MM-dd HH:mm"),
        sendEmail: false,
        files: null,
        addFilesToCourse: false
      }
    };
  },
  methods: {
    format,
    showNotificationsValueChanged: debounce(function() {
      this.getNotifications();
    }, 1500),
    createNotification() {
      let formData = new FormData();
      let notification = this.newNotification;

      formData.append("submittedById", this.user.id);
      formData.append("courseId", this.courseId);

      for (let [key, value] of Object.entries(notification)) {
        if (key != "files") {
          formData.append(key, value);
        }
      }

      if (notification.files != null) {
        notification.files.forEach(file => formData.append("files", file));
      }

      NotificationService.createNotification(formData, this.courseId).then(
        () => {
          this.getNotifications(this.courseId);
          this.resetNewNotificationDialog();
        }
      );
    },
    resetNewNotificationDialog() {
      this.newNotification = {
        title: "Title goes here!",
        description: "Description goes here!",
        color: "#285de0",
        expiresAt: format(add(new Date(), { days: 1 }), "yyyy-MM-dd HH:mm"),
        sendEmail: false,
        files: null,
        addFilesToCourse: false
      };
      this.newNotificationDialog = false;
    },
    getNotifications() {
      this.notifications = null;
      this.notificationLoading = true;
      CourseService.getNotifications(
        this.courseId,
        this.showNotifications.includes(1),
        this.showNotifications.includes(0),
        this.sortByNew
      )
        .then(({ data }) => {
          this.notifications = data;
        })
        .finally(() => {
          this.notificationLoading = false;
        });
    },
    deleteNotification(id) {
      NotificationService.deleteNotification(this.courseId, id).then(() => {
        this.getNotifications();
      });
    },
    archiveNotification(id) {
      NotificationService.archiveNotification(this.courseId, id).then(() => {
        this.getNotifications();
      });
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
  border-radius: 6px;
</style>
