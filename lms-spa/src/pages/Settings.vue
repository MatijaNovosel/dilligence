<template>
  <q-page class="q-pa-md">
    <div class="row full-width">
      <div class="col-12">
        <span class="text-weight-light text-h5"> {{ $t('settings') }} </span>
      </div>
      <div class="col-12 q-py-sm">
        <q-separator />
      </div>
      <div class="col-12 q-pb-md">
        <q-toggle size="sm" :label="$t('darkMode')" v-model="settings.darkMode"></q-toggle>
      </div>
      <div class="col-12 q-pb-md">
        <q-select
          dense
          outlined
          v-model="settings.locale"
          :label="$t('locale')"
          :options="localeOptions"
          behavior="menu"
          style="max-width: 30%"
          emit-value
        />
      </div>
    </div>
  </q-page>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import KorisnikService from "../services/api/korisnik";

export default {
  name: "Settings",
  data() {
    return {
      localeOptions: [
        {
          value: "hr",
          label: "Croatian"
        },
        {
          value: "en",
          label: "English"
        }
      ],
      settings: {
        darkMode: false,
        locale: "en"
      }
    };
  },
  mounted() {
    this.settings = Object.assign({}, this.user.settings);
    this.$watch(
      "settings",
      val => {
        let user = Object.assign({}, this.user);
        user.settings = Object.assign({}, val);
        this.setUserData(user);
        this.$i18n.locale = user.settings.locale;
        KorisnikService.updateSettings(this.user.id, user.settings).then(() => {
          this.$q.notify({
            type: "positive",
            message: "Settings successfully updated!"
          });
        });
      },
      { immediate: false, deep: true }
    );
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    ...mapActions(["setUserData"])
  }
};
</script>
