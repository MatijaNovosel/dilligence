<template>
	<q-page>
		<div class="row">
			<q-space />
			<div class="col-3">
				<div class="row justify-center">
					<q-img style="width: 150px; height: 150px;" src="../assets/tvz-logo.svg"></q-img>
				</div>
			</div>
			<q-space />
		</div>
		<q-form>
			<div class="row justify-center text-center">
				<div class="col-3">
					<div class="row">
            <div class="col-12 q-py-sm">
              <q-field square dense filled label="Username" v-model="username"></q-field>
            </div>
            <div class="col-12">
						  <q-field square dense filled label="Password" v-model="password" type="password"></q-field>
            </div>
            <div class="col-12 q-py-sm">
              <q-btn @click="submit" class="ma-2" :loading="loading" color="primary">Sign in</q-btn>
            </div>
					</div>
				</div>
			</div>
		</q-form>
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