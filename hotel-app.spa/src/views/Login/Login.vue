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
    <v-form>
      <v-row justify="center">
        <v-col cols="3" class="mt-10">
          <v-row>
            <v-text-field label="Username" 
                          solo
                          v-model="username"
                          :error-messages="usernameErrors"
                          required
                          @input="$v.username.$touch()"
                          @blur="$v.username.$touch()"> 
            </v-text-field>
          </v-row>
          <v-row>
            <v-text-field label="Password" 
                          solo
                          v-model="password"
                          :error-messages="passwordErrors"
                          required
                          type="password"
                          @input="$v.password.$touch()"
                          @blur="$v.password.$touch()"> 
            </v-text-field>
          </v-row>
          <v-row align-content="center" align="center">
            <v-spacer />
            <v-btn @click="submit" 
                  class="ma-2"
                  :disabled="$v.$invalid" 
                  :loading="loading"
                  color="primary">
                  Sign in
            </v-btn>
            <v-spacer />
          </v-row>
        </v-col>
      </v-row>
    </v-form>
    <key-listener keyCode="13" 
                  event="keydown" 
                  @pressed="submit"> 
    </key-listener>
  </div>
</template>

<script>
  import { validationMixin } from 'vuelidate'
  import { required, maxLength, minLength } from 'vuelidate/lib/validators'
  import { mapActions } from 'vuex';
  import AuthService from '../../services/api/auth';
  import StudentService from '../../services/api/student';
  import NotificationService from '../../services/notification';
  import KeyListener from '../../components/KeyListener';

  export default { 
    components: {
      'key-listener': KeyListener  
    },
    mixins: [validationMixin],
    validations: {
      username: { 
        required, 
        maxLength: maxLength(10), 
        minLength: minLength(4) 
      },
      password: { 
        required, 
        maxLength: maxLength(10), 
        minLength: minLength(4) 
      }
    },
    data() {
      return {
        username: null,
        password: null,
        valid: false,
        loading: false
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
        }).then((response) => {
          if(response.status == 200) {
            const id = response.data.user.id;
            const token = response.data.token;

            StudentService.getStudent(1271).then(({ data }) => {
              var user = {
                id,
                name: data.ime,
                surname: data.prezime,
                jmbag: data.jmbag,
                token
              };
              
              this.setUserData(user);
              NotificationService.success("Login successful", "You were successfully logged in!");
              this.$router.push('/home');
            });
          }
        }).catch(() => {
          this.loading = false;
          NotificationService.error('Error', 'Unable to log in!');
        });
      },
      submit() {
        this.$v.$touch();
        if(this.$v.$invalid) {
          NotificationService.error("Empty input", "Please enter valid data into the form.");
        } else {
          this.loading = true;
          this.login();
        }
      },
    },
    computed: {
      usernameErrors() {
        const errors = [];
        if (!this.$v.username.$dirty) return errors;
        !this.$v.username.maxLength && errors.push('Username must be at most 8 characters long');
        !this.$v.username.minLength && errors.push('Username must be at least 4 characters long');
        !this.$v.username.required && errors.push('Username is required.');
        return errors;
      },
      passwordErrors() {
        const errors = [];
        if (!this.$v.password.$dirty) return errors;
        !this.$v.password.maxLength && errors.push('Password must be at most 8 characters long');
        !this.$v.password.minLength && errors.push('Password must be at least 4 characters long');
        !this.$v.password.required && errors.push('Password is required.');
        return errors;
      }
    }
  };

</script>