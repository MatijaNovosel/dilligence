<template>
  <q-page class="q-pa-md">
    <template v-if="course">
      <div class="row">
        <div class="col-12 q-pb-md text-center">
          <span class="text-weight-light text-h5">{{ course.name }}</span>
        </div>
        <div class="col-12">
          <q-card flat bordered>
            <q-tabs
              v-model="tab"
              dense
              class="text-grey"
              active-color="primary"
              indicator-color="primary"
              align="justify"
              narrow-indicator
            >
              <q-tab name="home" :label="$i18n.t('home')" />
              <q-tab name="notifications" :label="$i18n.t('notifications')" />
              <q-tab name="participants" :label="$i18n.t('participants')" />
              <q-tab name="exams" :label="$i18n.t('exams')" />
            </q-tabs>
            <q-separator />
            <q-tab-panels v-model="tab">
              <q-tab-panel name="home">{{ tab }}</q-tab-panel>
              <q-tab-panel name="notifications">
                <div class="row wrap justify-center items-center content-center">
                  <template v-if="notifications && notifications.length != 0">
                    <div class="col-12 q-my-sm" v-for="(notification, i) in notifications" :key="i">
                      <NotificationCard color="green-5" :value="notification" />
                    </div>
                  </template>
                  <div v-else class="col-12 q-my-sm">
                    <div>{{ $i18n.t('noData') }}</div>
                  </div>
                  <div class="col-12 q-my-sm text-right">
                    <q-btn
                      :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
                      size="sm"
                    >New notification</q-btn>
                  </div>
                </div>
              </q-tab-panel>
              <q-tab-panel name="participants">{{ tab }}</q-tab-panel>
              <q-tab-panel name="exams">{{ tab }}</q-tab-panel>
            </q-tab-panels>
          </q-card>
        </div>
      </div>
    </template>
  </q-page>
</template>

<script>
import CourseService from "../services/api/course";
import NotificationCard from "../components/NotificationCard";

export default {
  name: "CourseDetails",
  components: { NotificationCard },
  created() {
    this.getData();
  },
  methods: {
    getNotifications() {
      CourseService.getNotifications(this.course.id).then(({ data }) => {
        this.notifications = data;
      });
    },
    getData() {
      CourseService.getCourse(this.$route.params.id).then(({ data }) => {
        this.course = data;
      });
    }
  },
  data() {
    return {
      course: null,
      notifications: null,
      tab: "home"
    };
  },
  watch: {
    tab: {
      deep: true,
      immediate: false,
      handler(val) {
        if (val == "notifications") {
          this.getNotifications();
        }
      }
    }
  }
};
</script>
