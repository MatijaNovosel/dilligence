<template>
  <q-page>
    <div class="q-pa-md row justify-center">
      <div class="col-2 q-mr-lg" v-if="!$q.screen.xs && !$q.screen.sm">
        <q-expansion-item
          v-model="open"
          label="Contacts"
          class="chat-tab"
          :class="$q.dark.isActive ? 'border-dark' : 'border-light'"
        >
          <q-card>
            <q-separator />
            <div v-if="chats && chats.length != 0">
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
                        <img
                          :src="chat.firstParticipant.id == user.id ? generateUserPictureSource(chat.secondParticipant.picture) : generateUserPictureSource(chat.firstParticipant.picture)"
                        />
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
              <q-card-section class="text-center">No contacts found!</q-card-section>
            </div>
          </q-card>
        </q-expansion-item>
      </div>
      <div :class="$q.screen.xs || $q.screen.sm ? 'col-12' : 'col-9'">
        <div class="row">
          <div class="col-12" style="position: relative;">
            <chat-panel
              :scrollTrigger="scrollTrigger"
              @deleteMessage="deleteMessage"
              :messages="activeChatMessages"
              :activeChat="activeChat"
            />
          </div>
          <div class="col-12 q-py-md">
            <q-input
              :error="$v.message.$invalid && $v.message.$dirty"
              error-message="This field is required!"
              :readonly="!activeChat"
              :label="$i18n.t('enterMessage')"
              dense
              v-model="message"
              outlined
              @input="$v.message.$touch"
            >
              <template v-slot:append>
                <q-btn
                  :disabled="!activeChat || $v.message.$invalid"
                  :ripple="false"
                  dense
                  class="q-px-md q-mr-md"
                  size="sm"
                  color="primary"
                  @click="sendMessage"
                >{{ $i18n.t('send') }}</q-btn>
              </template>
            </q-input>
          </div>
        </div>
      </div>
    </div>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="newChatDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
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
          <q-skeleton v-show="userSearchLoading" style="width: 100%" type="rect" />
          <q-list
            v-if="foundUsers"
            separator
            dense
            :class="`border-box-${$q.dark.isActive ? 'dark' : 'light'}`"
          >
            <q-item v-if="foundUsers == null || foundUsers.length == 0">
              <q-item-section class="text-center">No users found!</q-item-section>
            </q-item>
            <q-item v-for="(user, i) in foundUsers" :key="i">
              <q-item-section avatar class="q-pl-md">
                <q-avatar size="30px">
                  <img :src="generateUserPictureSource(user.picture)" />
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
                >
                  <q-tooltip>Start new chat</q-tooltip>
                </q-btn>
              </q-item-section>
            </q-item>
          </q-list>
        </q-card-section>
      </q-card>
    </q-dialog>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab
        size="xs"
        direction="left"
        :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
        fab
        icon="add"
      >
        <q-fab-action
          @click="newChatDialog = true"
          icon="mdi-chat-processing"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New chat"
        />
      </q-fab>
    </q-page-sticky>
  </q-page>
</template>

<script>
import UserService from "../services/api/user";
import ChatService from "../services/api/chat";
import ChatPanel from "../components/chat-panel";
import ConnectionMixin from "../mixins/connectionMixin";
import { mapGetters } from "vuex";
import { required, minLength } from "vuelidate/lib/validators";
import { generateUserPictureSource } from "../helpers/helpers";

export default {
  name: "Chat",
  mixins: [ConnectionMixin],
  components: {
    "chat-panel": ChatPanel
  },
  validations: {
    message: {
      required,
      minLength: minLength(4)
    }
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
    window.addEventListener("keydown", this.enterPressed, false);
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    generateUserPictureSource,
    resetNewChatDialog() {
      this.newChatDialog = false;
      this.newChatSearch = this.foundUsers = null;
    },
    searchUsers() {
      this.userSearchLoading = true;
      ChatService.getAvailableUsers(this.user.id)
        .then(({ data }) => {
          this.foundUsers = data;
        })
        .finally(() => {
          this.userSearchLoading = false;
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
      if (this.$v.message.$invalid) {
        this.$v.message.$touch();
        return;
      }
      ChatService.sendMessage({
        content: this.message,
        chatId: this.activeChat.id,
        userId: this.user.id
      }).then(() => (this.message = null));
      this.$v.$reset();
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
    },
    enterPressed(e) {
      if (e.keyCode == 13) {
        this.sendMessage();
      }
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
      activeChatMessages: null,
      userSearchLoading: false,
      dialogStyle: {
        width: "70%",
        "max-width": "50vw"
      }
    };
  },
  destroyed() {
    window.removeEventListener("keydown", this.enterPressed, false);
  }
};
</script>

<style lang="sass">
.chat-tab
  border-top-left-radius: 6px
  border-top-right-radius: 6px
  user-select: none
.dialog-toolbar
  min-height: 30px
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
</style>