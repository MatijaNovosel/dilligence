<template>
	<q-page class="row justify-center items-center">
		<q-card flat class="login-card">
			<q-card-section class="text-center q-py-none">
				<q-img style="width: 150px; height: 150px;" src="../assets/tvz-logo.svg"></q-img>
			</q-card-section>
			<q-card-section>
				<q-form>
          <q-input square dense filled label="Username" v-model="username" required />
          <q-input square dense filled label="Password" v-model="password" type="password" required class="q-pt-sm" />
				</q-form>
			</q-card-section>
			<q-card-actions class="justify-center">
				<q-btn @click="submit" class="ma-2" :loading="loading" color="primary">Sign in</q-btn>
			</q-card-actions>
		</q-card>
	</q-page>
</template>

<script>
export default {
	data() {
		return {
			username: null,
			password: null,
			valid: false,
			loading: false
		};
	},
	methods: {
		login() {
			/*
			AuthService.login({
				Username: this.username,
				Password: this.password
			})
				.then(({ data }) => {
					if (data.isSuccess) {
						const id = data.payload.id;
						const token = data.payload.token;
						StudentService.getStudent(id).then(({ data }) => {
							let user = {
								id,
								name: data.ime,
								surname: data.prezime,
								jmbag: data.jmbag,
								token
							};
							this.setUserData(user);
							NotificationService.success(
								"Login successful",
								"You were successfully logged in!"
							);
							this.$router.push("/home");
						});
					} else {
						throw new Error();
					}
				})
				.catch(() => {
					NotificationService.error("Error", "Unable to log in!");
				})
				.finally(() => {
					this.loading = false;
        });
        */
		},
		submit() {
			/*
			this.$v.$touch();
			if (this.$v.$invalid) {
				NotificationService.error(
					"Empty input",
					"Please enter valid data into the form."
				);
			} else {
				this.loading = true;
				this.login();
      }
      */
		}
	}
};
</script>

<style scoped lang="sass">
.login-card
  width: 25%;
  background: none;
</style>