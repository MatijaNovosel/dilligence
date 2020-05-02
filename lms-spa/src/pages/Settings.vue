<template>
  <q-page class="q-pl-md q-pt-md">
    <div class="row full-width">
      <div class="col-12">
        <span class="text-weight-light text-h5">{{ $i18n.t('settings') }}</span>
      </div>
      <div class="col-12">
        <q-separator />
      </div>
      <div class="col-xs-12 col-md-6 q-mt-md">
        <div class="row q-pr-md">
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
          <div class="col-12 q-my-sm">
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
            <q-toggle size="sm" :label="$i18n.t('darkMode')" v-model="userData.settings.darkMode" />
          </div>
          <div class="col-12">
            <q-checkbox size="sm" label="Show popups" v-model="userData.settings.popups" />
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
    return {};
  }
};
</script>

<style lang="sass">
.image-box
  border-radius: 25px
  border: 1px solid #e0dede
</style>