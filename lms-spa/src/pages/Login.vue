<template>
  <q-page class="row justify-center items-center">
    <q-card flat :style="$q.screen.xs || $q.screen.sm ? loginCardSmall : loginCardNormal">
      <q-card-section class="text-center q-mb-lg">
        <q-img class="spin" style="width: 150px; height: 165px; " src="../assets/logo-outlined.png"></q-img>
      </q-card-section>
      <q-card-section class="q-py-none">
        <q-input
          @input="$v.loginForm.username.$touch"
          square
          dense
          filled
          no-error-icon
          hide-bottom-space
          :label="$i18n.t('username')"
          v-model="loginForm.username"
          :error="$v.loginForm.username.$invalid && $v.loginForm.username.$dirty"
        />
        <q-input
          @input="$v.loginForm.password.$touch"
          square
          dense
          filled
          no-error-icon
          hide-bottom-space
          :label="$i18n.t('password')"
          v-model="loginForm.password"
          :type="!passwordShow ? 'password' : 'text'"
          :error="$v.loginForm.password.$invalid && $v.loginForm.password.$dirty"
          class="q-pt-sm"
        >
          <template v-slot:append>
            <q-btn
              flat
              round
              size="sm"
              :icon="!passwordShow ? 'mdi-eye' : 'mdi-eye-off'"
              @click="passwordShow = !passwordShow"
            />
          </template>
        </q-input>
      </q-card-section>
      <q-card-section class="q-pb-none q-pt-sm q-ml-md text-subtitle1">
        New to LMS?
        <span class="sign-up-text text-bold text-primary">SIGN UP</span>
      </q-card-section>
      <q-card-actions class="justify-center q-mt-sm">
        <q-btn
          :disabled="$v.loginForm.$invalid"
          @click="login"
          :loading="loading"
          color="primary"
          class="q-px-lg"
        >{{ $i18n.t('signIn') }}</q-btn>
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script>
import AuthService from "../services/api/auth";
import NotificationService from "../services/notification/notifications";
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
      passwordShow: false,
      loginForm: {
        username: "",
        password: ""
      },
      loading: false,
      loginCardSmall: {
        width: "80%",
        background: "none"
      },
      loginCardNormal: {
        width: "35%",
        background: "none"
      }
    };
  },
  created() {
    window.addEventListener("keydown", this.enterPressed, false);
  },
  destroyed() {
    window.removeEventListener("keydown", this.enterPressed, false);
  },
  methods: {
    ...mapActions(["setUserData"]),
    enterPressed(e) {
      if (e.keyCode == 13) {
        this.login();
      }
    },
    login() {
      if (this.$v.loginForm.$invalid) {
        NotificationService.showError("Invalid data!");
        return;
      }
      this.loading = true;
      AuthService.login({
        username: this.loginForm.username,
        password: this.loginForm.password
      })
        .then(({ data }) => {
          if (data.isSuccess) {
            NotificationService.showSuccess("Successfully logged in!");
            let user = { ...data.payload };
            this.setUserData(user);
            this.$router.push("/");
          } else {
            throw new Error();
          }
        })
        .catch(error => {
          NotificationService.showError(error.message);
        })
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>

<style>

@-moz-keyframes spin { 100% { -moz-transform: rotate(360deg); } }
@-webkit-keyframes spin { 100% { -webkit-transform: rotate(360deg); } }
@keyframes spin { 100% { -webkit-transform: rotate(360deg); transform:rotate(360deg); } }

.spin {
  -webkit-animation:spin 30s linear infinite;
  -moz-animation:spin 30s linear infinite;
  animation:spin 30s linear infinite;
}

.sign-up-text:hover {
	cursor: pointer
}

</style>