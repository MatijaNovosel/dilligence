<template>
  <q-page>
    <div class="q-pa-md row justify-center">
      <div class="col-2 q-mr-lg">
        <q-expansion-item v-model="open" :label="$i18n.t('recentChats')" class="chat-tab">
          <q-card>
            <q-separator />
            <div v-if="chats && chats.length != 0">
              <q-card-section class="q-pb-sm q-pt-none">
                <q-input dense :label="$i18n.t('searchUsers')"></q-input>
              </q-card-section>
              <q-separator />
              <q-card-section class="q-py-xs q-px-none">
                <q-list separator dense>
                  <q-item
                    clickable
                    v-for="(chat, i) in chats"
                    :key="i"
                    @click="getChatDetails(chat)"
                  >
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
            </div>
            <div v-else>
              <q-card-section class="text-center">{{ $i18n.t('noRecentChats') }}</q-card-section>
            </div>
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
            <div class="row items-center">
              <div class="col-11">
                <q-input
                  :readonly="!activeChat"
                  :label="$i18n.t('enterMessage')"
                  dense
                  v-model="message"
                  outlined
                >
                  <template v-slot:append>
                    <q-btn
                      :disabled="!activeChat"
                      :ripple="false"
                      dense
                      size="sm"
                      color="primary"
                      @click="sendMessage"
                    >{{ $i18n.t('send') }}</q-btn>
                  </template>
                </q-input>
              </div>
              <div class="col-1 text-center">
                <q-btn
                  class="q-pa-xs"
                  size="sm"
                  dense
                  color="primary"
                  @click="newChatDialog = true"
                >{{ $i18n.t('newChat') }}</q-btn>
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
            :label="$i18n.t('searchUsers')"
          >
            <template v-slot:prepend>
              <q-icon name="mdi-magnify" />
            </template>
            <template v-slot:append>
              <q-btn
                :ripple="false"
                dense
                size="sm"
                color="primary"
                @click="searchUsers"
              >{{ $i18n.t('search') }}</q-btn>
            </template>
          </q-input>
        </q-card-section>
        <q-card-section class="q-pt-none">
          <div class="text-center" v-if="foundUsers && foundUsers.length == 0">No users found!</div>
          <q-list v-else separator dense class="border-box">
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
import UserService from "../services/api/user";
import ChatService from "../services/api/chat";
import ChatPanel from "../components/ChatPanel";
import ConnectionMixin from "../mixins/connectionMixin";
import { mapGetters } from "vuex";

export default {
  name: "Chat",
  mixins: [ConnectionMixin],
  components: {
    ChatPanel
  },
  created() {
    this.getChats(this.user.id);
    this.startConnection("chat-hub");
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
      UserService.getChats(id).then(({ data }) => {
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
      message: null,
      chats: null,
      activeChat: null,
      activeChatMessages: null
    };
  }
};
</script>

<style scoped lang="sass">
.my-card
  width: 100%
  max-width: 350px
  margin: 5px
.chat-tab
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-top-left-radius: 6px
  border-top-right-radius: 6px
  user-select: none
.dialog-toolbar
  min-height: 30px
</style>