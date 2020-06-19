<template>
  <q-page class="row justify-center items-center">
    <q-card flat :style="$q.screen.xs || $q.screen.sm ? loginCardSmall : loginCardNormal">
      <q-card-section class="text-center q-mb-lg">
        <q-img class="spin" style="width: 150px; height: 165px; " src="../assets/logo-outlined.png"></q-img>
      </q-card-section>
      <q-card-section class="q-py-none q-col-gutter-sm">
        <q-input
          @input="usernameInput"
          square
          dense
          filled
          no-error-icon
          :hide-bottom-space="!usernameExists"
          :error-message="usernameExists ? 'This username is already in use!' : ''"
          :label="$i18n.t('username')"
          v-model="registerForm.username"
          :error="$v.registerForm.username.$invalid && $v.registerForm.username.$dirty || usernameExists"
        />
        <q-input
          @input="$v.registerForm.name.$touch"
          square
          dense
          filled
          no-error-icon
          hide-bottom-space
          label="Name"
          v-model="registerForm.name"
          :error="$v.registerForm.name.$invalid && $v.registerForm.name.$dirty"
        />
        <q-input
          @input="$v.registerForm.surname.$touch"
          square
          dense
          filled
          no-error-icon
          hide-bottom-space
          label="Surname"
          v-model="registerForm.surname"
          :error="$v.registerForm.surname.$invalid && $v.registerForm.surname.$dirty"
        />
        <q-input
          @input="$v.registerForm.email.$touch"
          square
          dense
          filled
          no-error-icon
          hide-bottom-space
          label="Email"
          v-model="registerForm.email"
          :error="$v.registerForm.email.$invalid && $v.registerForm.email.$dirty"
        />
        <q-input
          @input="$v.registerForm.password.$touch"
          square
          dense
          filled
          no-error-icon
          hide-bottom-space
          :label="$i18n.t('password')"
          v-model="registerForm.password"
          :type="!passwordShow ? 'password' : 'text'"
          :error="$v.registerForm.password.$invalid && $v.registerForm.password.$dirty"
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
        Already have an account?
        <span class="sign-up-text text-bold text-primary">SIGN IN</span>
      </q-card-section>
      <q-card-actions class="justify-center q-mt-sm">
        <q-btn
          :disabled="$v.registerForm.$invalid"
          @click="register"
          :loading="loading"
          color="primary"
          class="q-px-lg"
        >Sign up</q-btn>
        <q-btn @click="reset" flat round size="sm" color="white" icon="mdi-restart" />
      </q-card-actions>
    </q-card>
  </q-page>
</template>

<script>
import AuthService from "../services/api/auth";
import NotificationService from "../services/notification/notifications";
import { required, minLength, email } from "vuelidate/lib/validators";
import { debounce } from "debounce";

export default {
  validations: {
    registerForm: {
      name: {
        required,
        minLength: minLength(4)
      },
      surname: {
        required,
        minLength: minLength(4)
      },
      username: {
        required,
        minLength: minLength(4)
      },
      password: {
        required,
        minLength: minLength(4)
      },
      email: {
        email,
        required,
        minLength: minLength(4)
      }
    }
  },
  data() {
    return {
      passwordShow: false,
      usernameExists: false,
      registerForm: {
        username: "",
        password: "",
        name: "",
        surname: "",
        email: ""
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
  methods: {
    usernameInput() {
      this.$v.registerForm.username.$touch();
      this.checkUsername();
    },
    reset() {
      this.registerForm = {
        username: "",
        password: "",
        name: "",
        surname: "",
        email: ""
      };
      this.usernameExists = false;
      this.$v.$reset();
    },
    checkUsername: debounce(function() {
      if (
        this.registerForm.username == null ||
        this.registerForm.username === ""
      ) {
        return;
      }
      AuthService.checkUsername(this.registerForm.username).then(({ data }) => {
        this.usernameExists = data;
      });
    }, 750),
    register() {
      if (this.$v.registerForm.$invalid) {
        NotificationService.showError("Invalid data!");
        return;
      }
      AuthService.register(this.registerForm).then(() => {
        NotificationService.showSuccess("Registered successfully!");
        this.$router.push({ name: "login" });
      });
    }
  }
};
</script>

<style>
@-moz-keyframes spin {
  100% {
    -moz-transform: rotate(360deg);
  }
}
@-webkit-keyframes spin {
  100% {
    -webkit-transform: rotate(360deg);
  }
}
@keyframes spin {
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}

.spin {
  -webkit-animation: spin 30s linear infinite;
  -moz-animation: spin 30s linear infinite;
  animation: spin 30s linear infinite;
}

.sign-up-text:hover {
  cursor: pointer;
}
</style>