<template>
  <q-page class="row justify-center items-center">
    <q-card flat class="login-card">
      <q-card-section class="text-center">
        <q-img style="width: 150px; height: 150px;" src="../assets/tvz-logo.svg"></q-img>
      </q-card-section>
      <q-card-section class="q-py-none">
        <ValidationObserver ref="observer">
          <ValidationProvider
            immediate
            mode="lazy"
            name="username"
            rules="required|min:4"
            v-slot="{ invalid, dirty, errors }"
          >
            <q-input
              :error="invalid && dirty"
              :error-message="errors[0]"
              square
              dense
              filled
              :label="$t('username')"
              v-model="username"
              required
            />
          </ValidationProvider>
          <ValidationProvider
            immediate
            mode="lazy"
            name="password"
            rules="required|min:4"
            v-slot="{ invalid, dirty, errors }"
          >
            <q-input
              :error="invalid && dirty"
              :error-messages="errors[0]"
              square
              dense
              filled
              :label="$t('password')"
              v-model="password"
              type="password"
              required
              class="q-pt-sm"
            />
          </ValidationProvider>
        </ValidationObserver>
      </q-card-section>
      <q-card-actions class="justify-center">
        <q-btn @click="submit" :loading="loading" color="primary">{{ $t('signIn') }}</q-btn>
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script>
import AuthService from "../services/api/auth";
import StudentService from "../services/api/student";
import { mapActions } from "vuex";
import { required, min } from "vee-validate/dist/rules";
import {
  ValidationProvider,
  ValidationObserver,
  extend,
  validate
} from "vee-validate";

extend("min", min);
extend("required", {
  ...required,
  message: this.$t("error.required")
});

export default {
  data() {
    return {
      username: null,
      password: null,
      valid: true,
      loading: false
    };
  },
  components: {
    ValidationProvider,
    ValidationObserver
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
              message: this.$t("successfullyLoggedIn")
            });
            const { id, token, settings, privileges, name, surname } = {
              ...data.payload
            };
            let user = { id, name, surname, token, settings, privileges };
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
      this.$refs.observer.validate().then(result => {
        if (result) {
          this.login();
        } else {
          this.$q.notify({
            type: "negative",
            message: this.$t("error.invalid")
          });
        }
      });
    }
  }
};
</script>

<style scoped lang="sass">
.login-card
  width: 25%;
  background: none;
</style>