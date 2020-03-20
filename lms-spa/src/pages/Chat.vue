<template>
	<q-page>
		<div class="q-pa-md row justify-center">
			<div class="col-2 q-mr-lg">
				<q-expansion-item v-model="open" label="Recent chats" class="chat-tab">
					<q-card>
						<q-separator />
						<q-card-section class="q-pb-sm q-pt-none">
							<q-input dense label="Search users..."></q-input>
						</q-card-section>
						<q-separator />
						<q-card-section class="q-py-xs q-px-none">
							<q-list separator dense>
								<q-item clickable v-for="(chat, i) in chats" :key="i" @click="getChatDetails(chat.id)">
									<q-item-section avatar class="q-pl-md">
										<q-avatar size="30px">
											<img src="../assets/default-user.jpg" />
										</q-avatar>
									</q-item-section>
									<q-item-section>{{ chat.firstParticipant.id == user.id ? chat.secondParticipant.username : chat.firstParticipant.username }}</q-item-section>
								</q-item>
							</q-list>
						</q-card-section>
					</q-card>
				</q-expansion-item>
			</div>
			<div class="col-9">
				<div class="row">
					<div class="col-12">
						<div class="border-box chat q-px-md q-py-md" v-if="activeChatMessages">
							<template v-for="message in activeChatMessages">
								<q-chat-message
									:key="message.id"
									:name="message.userId == user.id ? 'You' : message.username"
									avatar="../assets/default-user.jpg"
									:text="[message.content]"
									:style="message.userId == user.id ? 'text-align: right;' : 'text-align: left;'"
									size="5"
									:stamp="message.sentAt | timeStampFilter"
									:sent="message.userId == user.id"
									:bg-color="message.userId == user.id ? 'blue-5' : 'blue-2'"
								/>
							</template>
						</div>
					</div>
					<div class="col-12 q-py-md">
						<div class="row">
							<div class="col-11">
								<q-input label="Enter a message..." dense v-model="message" outlined bg-color="white">
									<template v-slot:append>
										<q-btn :ripple="false" dense size="sm" color="primary" @click="searchUsers">Send</q-btn>
									</template>
								</q-input>
							</div>
							<div class="col-1 text-center q-mt-xs">
								<q-btn
									class="q-pa-xs"
									size="sm"
									dense
									color="primary"
									@click="newChatDialog = true"
								>New chat</q-btn>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<q-dialog v-model="newChatDialog" persistent no-esc-dismiss>
			<q-card style="width: 50vw;">
				<q-toolbar class="bg-primary text-white dialog-toolbar">
					<q-space />
					<q-btn
						:ripple="false"
						dense
						size="sm"
						color="white"
						flat
						round
						icon="mdi-close-circle"
						@click="resetNewChatDialog"
					/>
				</q-toolbar>
				<q-card-section>
					<q-input dense multiple outlined v-model="newChatSearch" clearable label="Search users...">
						<template v-slot:prepend>
							<q-icon name="mdi-magnify" />
						</template>
						<template v-slot:append>
							<q-btn :ripple="false" dense size="sm" color="primary" @click="searchUsers">Search</q-btn>
						</template>
					</q-input>
				</q-card-section>
				<q-card-section class="q-pt-none">
					<q-list separator dense class="border-box">
						<q-item v-for="(user, i) in foundUsers" :key="i">
							<q-item-section avatar class="q-pl-md">
								<q-avatar size="30px">
									<img src="../assets/default-user.jpg" />
								</q-avatar>
							</q-item-section>
							<q-item-section>{{ user.username }}</q-item-section>
							<q-item-section side>
								<q-btn
									:ripple="false"
									dense
									size="sm"
									color="primary"
									flat
									round
									icon="mdi-comment-plus"
									@click="startNewChat(user.id)"
								/>
							</q-item-section>
						</q-item>
					</q-list>
				</q-card-section>
			</q-card>
		</q-dialog>
	</q-page>
</template>

<script>
import KorisnikService from "../services/api/korisnik";
import ChatService from "../services/api/chat";
import {
	HubConnectionBuilder,
	LogLevel,
	HttpTransportType
} from "@aspnet/signalr";
import apiConfig from "../api.config";
import { mapGetters } from "vuex";

export default {
	name: "Chat",
	created() {
		this.getChats(this.user.id);
		/* this.connection = new HubConnectionBuilder()
			.withUrl("http://localhost:5000/vijesti-hub")
			.configureLogging(LogLevel.Information)
			.build();
		this.connection.start();
		this.connection.on("EVENT", response => {
			this.vijesti = [ ...this.vijesti, response.payload ];
		}); */
	},
	computed: {
		...mapGetters(["user"])
	},
	methods: {
		resetNewChatDialog() {
			this.newChatDialog = false;
			this.newChatSearch = null;
			this.foundUsers = null;
		},
		searchUsers() {
			KorisnikService.searchKorisnik(this.newChatSearch).then(({ data }) => {
				this.foundUsers = data.results;
			});
		},
		getChats(id) {
			KorisnikService.getChats(id).then(({ data }) => {
				this.chats = data;
				if (this.chats.length != 0) {
					this.getChatDetails(this.chats[0].id);
				}
			});
		},
		getChatDetails(id) {
			if (this.activeChatId != null && this.activeChatId == id) {
				return;
			}
			this.activeChatId = id;
			ChatService.getChatDetails(id).then(({ data }) => {
				this.activeChatMessages = data.messages;
			});
		}
	},
	data() {
		return {
			foundUsers: null,
			newChatSearch: null,
			newChatDialog: false,
			open: true,
			connection: null,
			message: null,
			chats: null,
			activeChatId: null,
			activeChatMessages: null
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
.chat
  background-color: #f9f9f9
  overflow: auto
  max-height: 600px !important
  position: relative !important
</style>