<template>
  <div>
    <v-row no-gutters>
      <v-spacer />
      <v-col cols="3">
        <v-row>
          <v-img contain src="../../assets/tvz-logotype.svg"></v-img>
        </v-row>
      </v-col>
      <v-spacer />
    </v-row>
    <v-row justify="center" class="mt-5">
      <v-col cols="3">
        <v-row>
          <v-text-field label="Username" v-model="username"> </v-text-field>
        </v-row>
        <v-row>
          <v-text-field label="Password" v-model="password"> </v-text-field>
        </v-row>
        <v-row class="mt-5" align-content="center" align="center">
          <v-spacer />
          <v-btn @click="login()" color="primary">Sign in</v-btn>
          <v-spacer />
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import AuthService from '../../services/api/auth';
import { mapActions } from 'vuex';

export default { 
  data() {
    return {
      username: null,
      password: null
    }
  },
  methods: {
    ...mapActions([
      'setUserData'
    ]),
    login() {
      AuthService.login({ 
        Username: this.username, 
        Password: this.password 
      })
      .then(response => {
        var user = {
          id: response.data.user.id,
          username: response.data.user.username,
          token: response.data.token
        };
        localStorage.setItem('token', response.data.token);
        localStorage.setItem('user', JSON.stringify(user));
        this.setUserData(user);
      });
    }
  }
};

</script>