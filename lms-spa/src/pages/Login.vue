<template>
  <q-page class="row justify-center items-center">
    <q-card flat class="login-card">
      <q-card-section class="text-center">
        <q-img style="width: 150px; height: 150px;" src="../assets/tvz-logo.svg"></q-img>
      </q-card-section>
      <q-card-section class="q-py-none">
        <q-input
          @input="$v.loginForm.username.$touch"
          square
          dense
          filled
          :label="$i18n.t('username')"
          v-model="loginForm.username"
          :error="$v.loginForm.username.$invalid && $v.loginForm.username.$dirty"
          error-message="This field is required!"
        />
        <q-input
          @input="$v.loginForm.password.$touch"
          square
          dense
          filled
          :label="$i18n.t('password')"
          v-model="loginForm.password"
          type="password"
          :error="$v.loginForm.password.$invalid && $v.loginForm.password.$dirty"
          error-message="This field is required!"
          class="q-pt-sm"
        />
      </q-card-section>
      <q-card-actions class="justify-center q-mt-sm">
        <q-btn
          :disabled="$v.loginForm.$invalid"
          @click="login"
          :loading="loading"
          color="primary"
        >{{ $i18n.t('signIn') }}</q-btn>
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script>
import AuthService from "../services/api/auth";
import { mapActions } from "vuex";
import { required, minLength } from "vuelidate/lib/validators";

export default {
  validations: {
    loginForm: {
      username: {
        required,
        minLength: minLength(4)
      },
      password: {
        required,
        minLength: minLength(4)
      }
    }
  },
  data() {
    return {
      loginForm: {
        username: "",
        password: ""
      },
      loading: false
    };
  },
  methods: {
    ...mapActions(["setUserData"]),
    login() {
      this.loading = true;
      AuthService.login({
        username: this.loginForm.username,
        password: this.loginForm.password
      })
        .then(({ data }) => {
          if (data.isSuccess) {
            this.$q.notify({
              type: "positive",
              message: this.$i18n.t("successfullyLoggedIn")
            });
            data.payload.blacklist = [];
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
    }
  }
};
</script>

<style scoped lang="sass">
.login-card
  width: 25%;
  background: none;
</style>