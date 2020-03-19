<template>
	<q-page>
		<div class="q-pa-md row justify-center">
			<div class="col-2 q-mr-lg">
				<q-expansion-item label="Contacts" class="chat-tab">
					<q-card>
						<q-separator />
						<q-card-section class="q-py-xs q-px-none">
							<q-list separator dense>
								<q-item @click="$router.push('/chat')" clickable v-for="n in 6" :key="n">
									<q-item-section avatar class="q-pl-md">
										<q-avatar size="30px">
											<img src="https://cdn.quasar.dev/img/avatar2.jpg" />
										</q-avatar>
									</q-item-section>
									<q-item-section>Ivica</q-item-section>
								</q-item>
							</q-list>
						</q-card-section>
					</q-card>
				</q-expansion-item>
			</div>
			<div class="col-9">
				<div class="border-box q-pa-sm">
					<q-chat-message
						name="User 1"
						avatar="../assets/default-user.jpg"
						:text="['MESSAGE 1']"
						stamp="7 minutes ago"
						sent
						bg-color="blue-5"
					/>
					<q-chat-message
						name="User 2"
						avatar="../assets/default-user.jpg"
						:text="[
            'MESSAGE 2',
            'MESSAGE 3'
          ]"
						size="6"
						stamp="4 minutes ago"
						text-color="black"
						bg-color="blue-2"
					/>
				</div>
			</div>
		</div>
	</q-page>
</template>

<script>
import FileCabinet from "../components/FileCabinet";
import KolegijService from "../services/api/kolegij";
import {
	HubConnectionBuilder,
	LogLevel,
	HttpTransportType
} from "@aspnet/signalr";
import apiConfig from "../api.config";

export default {
	name: "Test",
	components: {
		FileCabinet
	},
	created() {
		/* this.connection = new HubConnectionBuilder()
			.withUrl("http://localhost:5000/vijesti-hub")
			.configureLogging(LogLevel.Information)
			.build();
		this.connection.start();
		this.connection.on("EVENT", response => {
			this.vijesti = [ ...this.vijesti, response.payload ];
		}); */
		KolegijService.getKolegijSidebar(147).then(({ data }) => {
			this.sidebarContents = data;
		});
		this.getData();
	},
	methods: {},
	data() {
		return {
			connection: null
		};
	},
	beforeDestroy() {
		// this.connection.stop();
	}
};
</script>

<style scoped lang="sass">
.my-card
  width: 100%
  max-width: 350px
  margin: 5px
.border-box
  position: relative
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 10px
  width: 100%
.chat-tab
  background-color: white
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-top-left-radius: 6px
  border-top-right-radius: 6px
  user-select: none
</style>