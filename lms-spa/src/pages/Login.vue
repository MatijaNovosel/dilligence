<template>
  <q-page class="row justify-center items-center">
    <q-card flat class="login-card">
      <q-card-section class="text-center">
        <q-img style="width: 150px; height: 150px;" src="../assets/tvz-logo.svg"></q-img>
      </q-card-section>
      <q-card-section class="q-py-none">
        <q-input square dense filled :label="$i18n.t('username')" v-model="username" required />
        <q-input
          square
          dense
          filled
          :label="$i18n.t('password')"
          v-model="password"
          type="password"
          required
          class="q-pt-sm"
        />
      </q-card-section>
      <q-card-actions class="justify-center">
        <q-btn @click="submit" :loading="loading" color="primary">{{ $i18n.t('signIn') }}</q-btn>
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script>
import AuthService from "../services/api/auth";
import { mapActions } from "vuex";

export default {
  data() {
    return {
      username: null,
      password: null,
      valid: true,
      loading: false
    };
  },
  methods: {
    ...mapActions(["setUserData"]),
    login() {
      this.loading = true;
      AuthService.login({
        Username: this.username,
        Password: this.password
      })
        .then(({ data }) => {
          if (data.isSuccess) {
            this.$q.notify({
              type: "positive",
              message: this.$i18n.t("successfullyLoggedIn")
            });
            data.payload.settings.selfNotifications = true;
            let user = { ...data.payload };
            this.setUserData(user);
            this.$router.push("/");
          } else {
            throw new Error();
          }
        })
        .catch(error => {
          this.$q.notify({
            type: "negative",
            message: error.message
          });
        })
        .finally(() => {
          this.loading = false;
        });
    },
    submit() {
      if (true) {
        this.login();
      } else {
        this.$q.notify({
          type: "negative",
          message: this.$i18n.t("error.invalid")
        });
      }
    }
  }
};
</script>

<style scoped lang="sass">
.login-card
  width: 25%;
  background: none;
</style>