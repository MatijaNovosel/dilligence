<template>
	<q-page class="row justify-center items-center">
		<q-card flat class="login-card">
			<q-card-section class="text-center q-py-none">
				<q-img style="width: 150px; height: 150px;" src="../assets/tvz-logo.svg"></q-img>
			</q-card-section>
			<q-card-section>
				<q-form>
					<q-input square dense filled label="Username" v-model="username" required />
					<q-input
						square
						dense
						filled
						label="Password"
						v-model="password"
						type="password"
						required
						class="q-pt-sm"
					/>
				</q-form>
			</q-card-section>
			<q-card-actions class="justify-center">
				<q-btn @click="submit" class="ma-2" :loading="loading" color="primary">Sign in</q-btn>
			</q-card-actions>
		</q-card>
	</q-page>
</template>

<script>
import { required, minLength } from "vuelidate/lib/validators";
import AuthService from "../services/api/auth";

export default {
	data() {
		return {
			username: null,
			password: null,
			valid: false,
			loading: false
		};
	},
	validations: {
		username: {
			required,
			minLength: minLength(4)
		},
		password: {
			required,
			minLength: minLength(4)
		}
	},
	methods: {
		login() {
			AuthService.login({
				Username: this.username,
				Password: this.password
			}).then(({ response }) => {
				console.log(response);
			});
		},
		submit() {
			this.$v.$touch();
			if (this.$v.$invalid) {
				console.log("Invalid!");
			} else {
				this.login();
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