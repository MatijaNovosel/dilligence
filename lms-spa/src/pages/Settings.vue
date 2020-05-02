<template>
  <q-page class="q-px-sm q-pt-md">
    <div class="row full-width q-gutter-sm">
      <div class="col-12">
        <span class="text-weight-light text-h5">{{ $i18n.t('settings') }}</span>
      </div>
      <div class="col-12 q-pb-md">
        <q-separator />
      </div>
      <div class="col-12">
        <div class="row">
          <div class="col-xs-12 col-md-3 text-center">
            <q-img
              class="image-box"
              :width="$q.screen.sm || $q.screen.xs ? '300px' : '75%'"
              height="300px"
              :src="user.picture == null ? require('../assets/default-user.jpg') : 'data:image/png;base64,' + user.picture"
            />
            <div
              class="q-my-md"
              :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
            >Profile picture</div>
          </div>
          <div class="col-xs-12 col-md-6">
            <div class="row q-pr-md q-gutter-sm">
              <div
                class="q-pl-sm"
                :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
              >Personal information</div>
              <div class="col-12">
                <q-input readonly label="Username" dense outlined :value="user.username" />
              </div>
              <div class="col-12">
                <q-form class="q-gutter-sm" @input="personalInformationChange">
                  <q-input label="Name" dense outlined v-model="userData.name" />
                  <q-input label="Surname" dense outlined v-model="userData.surname" />
                  <q-input label="Email" dense outlined v-model="userData.email" />
                </q-form>
              </div>
              <div class="col-12">
                <q-file
                  accept=".jpg, .png"
                  dense
                  outlined
                  v-model="picture"
                  clearable
                  label="Upload new picture"
                  @input="uploadPicture"
                >
                  <template v-slot:prepend>
                    <q-icon name="mdi-image" />
                  </template>
                </q-file>
              </div>
              <div
                class="q-pl-sm q-pt-sm"
                :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
              >Application settings</div>
              <div class="col-12">
                <q-select
                  dense
                  outlined
                  v-model="userData.settings.locale"
                  :label="$i18n.t('locale')"
                  :options="localeOptions"
                  emit-value
                />
              </div>
              <div class="col-12">
                <div :class="`border-box-${$q.dark.isActive ? 'dark' : 'light'}`">
                  <div class="q-pa-sm">
                    <span>Notification blacklist:</span>
                    <div
                      :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
                    >(You will not receive notifications from these courses)</div>
                    <q-option-group
                      @input="blacklistChange"
                      v-model="userData.blacklist"
                      :options="options"
                      class="q-mt-sm"
                      dense
                      type="checkbox"
                      color="primary"
                      emit-options
                    />
                  </div>
                </div>
              </div>
              <div class="col-12">
                <q-toggle
                  size="sm"
                  :label="$i18n.t('darkMode')"
                  v-model="userData.settings.darkMode"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </q-page>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import UserService from "../services/api/user";
import { debounce } from "debounce";

export default {
  name: "Settings",
  data() {
    return {
      userData: {
        name: null,
        surname: null,
        email: null,
        blacklist: [],
        settings: {
          darkMode: false,
          locale: "en"
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
      picture: null,
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
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    ...mapActions(["setUserData"]),
    personalInformationChange: debounce(function() {
      let user = { ...this.user };
      [user.name, user.surname, user.email] = [
        this.userData.name,
        this.userData.surname,
        this.userData.email
      ];
      let payload = {
        userId: this.user.id,
        name: this.userData.name,
        surname: this.userData.surname,
        email: this.userData.email
      };
      UserService.updatePersonalInformation(payload).then(() => {
        this.setUserData(user);
        this.$q.notify({
          type: "positive",
          message: "Personal info updated!"
        });
      });
    }, 1500),
    blacklistChange: debounce(function() {
      this.$q.notify({
        type: "positive",
        message: "Blacklist updated!"
      });
    }, 1500),
    uploadPicture(file) {
      if (!file) {
        return;
      }
      let formData = new FormData();
      formData.append("picture", file);
      UserService.uploadPicture(this.user.id, formData).then(({ data }) => {
        let user = { ...this.user };
        user.picture = data.payload.picture.data;
        this.setUserData(user);
      });
      this.picture = null;
    }
  }
};
</script>

<style lang="sass">
.image-box
  border-radius: 25px
  border: 1px solid #e0dede
</style>