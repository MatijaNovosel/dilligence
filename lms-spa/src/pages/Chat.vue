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
                <q-item clickable v-for="(chat, i) in chats" :key="i" @click="getChatDetails(chat)">
                  <q-item-section avatar class="q-pl-md">
                    <q-avatar size="30px">
                      <img src="../assets/default-user.jpg" />
                    </q-avatar>
                  </q-item-section>
                  <q-item-section
                    class="ellipsis"
                  >{{ chat.firstParticipant.id == user.id ? chat.secondParticipant.username : chat.firstParticipant.username }}</q-item-section>
                </q-item>
              </q-list>
            </q-card-section>
          </q-card>
        </q-expansion-item>
      </div>
      <div class="col-9">
        <div class="row">
          <div class="col-12" style="position: relative;">
            <ChatPanel
              :scrollTrigger="scrollTrigger"
              @deleteMessage="deleteMessage"
              :messages="activeChatMessages"
            />
          </div>
          <div class="col-12 q-py-md">
            <div class="row">
              <div class="col-11">
                <q-input
                  label="Enter a message..."
                  dense
                  v-model="message"
                  outlined
                  bg-color="white"
                >
                  <template v-slot:append>
                    <q-btn :ripple="false" dense size="sm" color="primary" @click="sendMessage">Send</q-btn>
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
          <q-input
            dense
            multiple
            outlined
            v-model="newChatSearch"
            clearable
            label="Search users..."
          >
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
import ChatPanel from "../components/ChatPanel";
import {
  HubConnectionBuilder,
  LogLevel,
  HttpTransportType
} from "@aspnet/signalr";
import apiConfig from "../api.config";
import { mapGetters } from "vuex";

export default {
  name: "Chat",
  components: {
    ChatPanel
  },
  created() {
    this.getChats(this.user.id);
    this.connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/chat-hub")
      .configureLogging(LogLevel.Information)
      .build();
    this.connection.start();
    this.connection.on("messageSent", message => {
      this.activeChatMessages = [...this.activeChatMessages, message];
      this.scrollTrigger = !this.scrollTrigger;
    });
    this.connection.on("messageDeleted", id => {
      this.activeChatMessages = this.activeChatMessages.filter(x => x.id != id);
    });
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    resetNewChatDialog() {
      this.newChatDialog = false;
      this.newChatSearch = this.foundUsers = null;
    },
    searchUsers() {
      ChatService.getAvailableUsers(this.user.id).then(({ data }) => {
        this.foundUsers = data;
      });
    },
    getChats(id) {
      KorisnikService.getChats(id).then(({ data }) => {
        this.chats = data;
        if (this.chats.length != 0) {
          this.getChatDetails(this.chats[0]);
        }
      });
    },
    getChatDetails(chat) {
      if (this.activeChat != null && this.activeChat.id == chat.id) {
        return;
      }
      this.activeChat = chat;
      ChatService.getChatDetails(chat.id).then(({ data }) => {
        this.activeChatMessages = data.messages;
        this.scrollTrigger = !this.scrollTrigger;
      });
    },
    sendMessage() {
      ChatService.sendMessage({
        content: this.message,
        chatId: this.activeChat.id,
        userId: this.user.id
      }).then(() => (this.message = null));
    },
    startNewChat(id) {
      ChatService.createNewChat({
        firstParticipantId: this.user.id,
        secondParticipantId: id
      }).then(({ data }) => {
        this.getChats(this.user.id);
        this.searchUsers();
      });
    },
    deleteMessage(id) {
      ChatService.deleteMessage(id);
    }
  },
  data() {
    return {
      scrollTrigger: false,
      foundUsers: null,
      newChatSearch: null,
      newChatDialog: false,
      open: true,
      connection: null,
      message: null,
      chats: null,
      activeChat: null,
      activeChatMessages: null
    };
  },
  beforeDestroy() {
    this.connection.stop();
  }
};
</script>

<style scoped lang="sass">
.my-card
  width: 100%
  max-width: 350px
  margin: 5px
.chat-tab
  background-color: white
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-top-left-radius: 6px
  border-top-right-radius: 6px
  user-select: none
.dialog-toolbar
  min-height: 30px
</style>