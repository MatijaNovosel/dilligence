<template>
  <q-page class="q-pa-md">
    <div class="row full-width q-gutter-sm">
      <div class="col-12">
        <span class="text-weight-light text-h5">{{ $i18n.t('settings') }}</span>
      </div>
      <div class="col-12 q-pb-md">
        <q-separator />
      </div>
      <div class="col-12">
        <div class="row">
          <div class="col-3 text-center">
            <q-img
              class="image-box"
              width="65%"
              :src="user.picture == null ? require('../assets/default-user.jpg') : 'data:image/png;base64,' + user.picture"
            />
            <div class="q-mt-md text-subtitle1">Profile picture</div>
          </div>
          <div class="col-9">
            <div class="row q-pr-md q-gutter-sm">
              <div class="col-12">
                <q-input label="Username" dense outlined :value="user.username" />
              </div>
              <div class="col-12">
                <q-input label="Name" dense outlined :value="user.name" />
              </div>
              <div class="col-12">
                <q-input label="Surname" dense outlined :value="user.surname" />
              </div>
              <div class="col-12">
                <q-input label="Email" dense outlined />
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
              <div class="col-12">
                <q-select
                  dense
                  outlined
                  v-model="settings.locale"
                  :label="$i18n.t('locale')"
                  :options="localeOptions"
                  behavior="menu"
                  emit-value
                />
              </div>
              <div class="col-12">
                <q-toggle size="sm" :label="$i18n.t('darkMode')" v-model="settings.darkMode" />
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

export default {
  name: "Settings",
  data() {
    return {
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
      settings: {
        darkMode: false,
        locale: "en"
      },
      picture: null,
      imgSrc: null
    };
  },
  mounted() {
    this.settings = Object.assign({}, this.user.settings);
    this.imgSrc = "data:image/png;base64," + this.user.picture;
    this.$watch(
      "settings",
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