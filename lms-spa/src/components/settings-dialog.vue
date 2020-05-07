<template>
  <q-dialog v-model="value" persistent no-esc-dismiss>
    <q-card style="width: 50%; max-width: 90vw;">
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
            <q-option-group
              @input="blacklistChange"
              v-model="userData.blacklist"
              :options="options"
              class="q-mt-sm"
              dense
              type="checkbox"
              color="primary"
            />
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
import { mapActions, mapGetters } from "vuex";
import UserService from "../services/api/user";
import { debounce } from "debounce";

export default {
  name: "settings-dialog",
  props: ["value"],
  data() {
    return {
      userData: {
        name: null,
        surname: null,
        email: null,
        blacklist: [],
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
      options: [
        {
          label: "Analogni sklopovi E",
          value: 1
        },
        {
          label: "Mehatronika",
          value: 2
        },
        {
          label: "Web aplikacije u Javi",
          value: 3
        }
      ]
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
      this.$q.notify({
        type: "positive",
        message: "Blacklist updated!"
      });
    }, 1500),
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
  mounted() {
    this.userData = JSON.parse(JSON.stringify(this.user));
    this.$watch(
      "userData.settings",
      val => {
        let user = { ...this.user };
        user.settings = { ...val };
        this.setUserData(user);
        this.$i18n.locale = user.settings.locale;
        UserService.updateSettings(this.user.id, user.settings).then(() => {
          this.$q.notify({
            type: "positive",
            message: "Settings successfully updated!"
          });
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
