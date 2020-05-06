<template>
  <div>
    <q-btn
      @click="scrollToBottom"
      v-show="$refs.chat != undefined && $refs.chat.scrollSize > $refs.chat.containerHeight && $refs.chat.scrollPercentage < 0.8"
      size="sm"
      round
      color="primary"
      icon="mdi-menu-down"
      class="bottom-left"
    />
    <q-scroll-area
      ref="chat"
      :thumb-style="thumbStyle"
      :bar-style="barStyle"
      :class="`border-box-${$q.dark.isActive ? 'dark' : 'light'}`"
      class="q-px-lg q-py-md"
      style="height: 600px;"
    >
      <template v-if="messages">
        <q-chat-message
          v-for="message in messages"
          :key="message.id"
          :avatar="message.userId == user.id ? generateUserPictureSource(user.picture) : (activeChat.firstParticipant.id == user.id ? generateUserPictureSource(activeChat.secondParticipant.picture) : generateUserPictureSource(activeChat.firstParticipant.picture))"
          :style="message.userId == user.id ? 'text-align: right;' : 'text-align: left;'"
          size="6"
          class="q-mx-md"
          :stamp="message.sentAt | timeStampFilter"
          :sent="message.userId == user.id"
          :text-color="$q.dark.isActive ? 'white' : 'black'"
          :bg-color="message.userId == user.id ? $q.dark.isActive ? 'dark' : 'blue-5' : $q.dark.isActive ? 'grey-9' : 'blue-2'"
        >
          <span>{{ message.content }}</span>
          <q-btn
            v-if="message.userId == user.id"
            size="xs"
            flat
            round
            text-color="grey-7"
            icon="mdi-delete"
            @click="$emit('deleteMessage', message.id)"
          />
        </q-chat-message>
      </template>
      <div
        v-else
        style="height: 585px;"
        class="row justify-center items-center"
      >No chats active, start a conversation by clicking below!</div>
    </q-scroll-area>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Vue from "vue";
import { generateUserPictureSource } from "../helpers/helpers";

export default {
  name: "chat-panel",
  props: ["activeChat", "messages", "scrollTrigger"],
  data() {
    return {
      thumbStyle: {
        right: "4px",
        borderRadius: "5px",
        backgroundColor: "#027be3",
        width: "5px",
        opacity: 0.75
      },
      barStyle: {
        right: "0px",
        borderRadius: "9px",
        backgroundColor: "#027be3",
        width: "9px",
        opacity: 0.2
      }
    };
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    generateUserPictureSource,
    scrollToBottom() {
      Vue.nextTick(() => {
        /*
        
          Component is rendered independently of the chat messages, so relying on the reference is a bad idea, 
          however the scroll size of the component stays consistently the same
        
        */
        this.$refs.chat.setScrollPosition(this.messages.length * 68, 0);
      });
    },
    scrollToTop() {
      this.$refs.chat.setScrollPosition(0, 350);
    }
  },
  watch: {
    scrollTrigger() {
      this.scrollToBottom();
    }
  }
};
</script>

<style scoped lang="sass">
.bottom-left
  position: absolute
  bottom: 20px
  left: 20px
  z-index: 200
</style>
