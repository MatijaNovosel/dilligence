<template>
  <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="value" persistent>
    <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
      <q-toolbar
        :style="`border-bottom: 1px solid ${$q.dark.isActive ? 'rgba(255, 255, 255, 0.6)' : 'rgba(0, 0, 0, 0.12)'};`"
      >
        <span style="font-size: 18px;">Settings</span>
        <q-space />
        <q-btn
          @click="closeDialog"
          :ripple="false"
          dense
          size="sm"
          flat
          round
          icon="mdi-close-thick"
        />
      </q-toolbar>
      <q-card-section>
        <q-select
          dense
          outlined
          v-model="userData.settings.locale"
          :label="$i18n.t('locale')"
          :options="localeOptions"
          emit-value
          map-options
        />
        <div class="q-my-sm" :class="[$q.dark.isActive ? 'border-box-dark' : 'border-box-light']">
          <div class="q-pa-sm">
            <span>Course blacklist:</span>
            <div
              :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
            >(You will not receive notifications or emails from these courses)</div>
            <template v-if="options">
              <q-option-group
                v-if="options.length != 0"
                @input="blacklistChange"
                v-model="blacklist"
                :options="options"
                class="q-mt-sm"
                dense
                type="checkbox"
                color="primary"
              />
              <div class="q-my-sm text-subtitle1" v-else>You are not subscribed to any courses!</div>
            </template>
          </div>
        </div>
        <div>
          <q-toggle size="sm" :label="$i18n.t('darkMode')" v-model="userData.settings.darkMode" />
        </div>
        <q-checkbox size="sm" label="Show popups" v-model="userData.settings.popups" />
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script>
import UserService from "../services/api/user";
import NotificationService from "../services/notification/notifications";
import { mapActions, mapGetters } from "vuex";
import { debounce } from "debounce";

export default {
  name: "settings-dialog",
  props: ["value"],
  data() {
    return {
      dialogStyle: { width: "55%", "max-width": "90vw" },
      userData: {
        name: null,
        surname: null,
        email: null,
        settings: {
          darkMode: false,
          locale: "en",
          popups: false
        }
      },
      localeOptions: [
        {
          value: "hr",
          label: this.$i18n.t("croatian")
        },
        {
          value: "en",
          label: this.$i18n.t("english")
        }
      ],
      options: null,
      blacklist: []
    };
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    ...mapActions(["removeUserData", "setUserData"]),
    closeDialog() {
      this.open = false;
      this.$emit("closed");
    },
    blacklistChange: debounce(function() {
      UserService.updateBlacklist({
        userId: this.user.id,
        courseIds: this.blacklist
      }).then(() => {
        NotificationService.showSuccess("Blacklist updated!");
      });
    }, 1500)
  },
  mounted() {
    UserService.getBlacklist(this.user.id).then(({ data }) => {
      let blacklistOptions = [];
      data.forEach(x =>
        blacklistOptions.push({ label: x.name, value: x.courseId })
      );
      this.options = blacklistOptions;
      this.blacklist = data
        .map(x => {
          if (x.blacklisted) {
            return x.courseId;
          }
        })
        .filter(x => x != null);
    });
    this.userData = JSON.parse(JSON.stringify(this.user));
    this.$watch(
      "userData.settings",
      val => {
        let user = { ...this.user };
        user.settings = { ...val };
        this.setUserData(user);
        this.$i18n.locale = user.settings.locale;
        UserService.updateSettings(this.user.id, user.settings).then(() => {
          NotificationService.showSuccess("Settings successfully updated!");
        });
        this.localeOptions = [
          {
            value: "hr",
            label: this.$i18n.t("croatian")
          },
          {
            value: "en",
            label: this.$i18n.t("english")
          }
        ];
      },
      { immediate: false, deep: true }
    );
  }
};
</script>
