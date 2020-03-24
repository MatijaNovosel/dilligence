<template>
  <div>
    <q-btn
      @click="scrollChatToBottom"
      v-show="$refs.chat && $refs.chat.scrollSize > $refs.chat.containerHeight && $refs.chat.scrollPercentage < 0.8"
      size="sm"
      round
      color="primary"
      icon="mdi-menu-down"
      class="bottom-left"
    />
    <q-scroll-area
      v-if="messages"
      ref="chat"
      :thumb-style="thumbStyle"
      :bar-style="barStyle"
      class="border-box q-px-lg q-py-md"
      style="height: 600px;"
    >
      <template v-for="message in messages">
        <q-chat-message
          :key="message.id"
          :name="message.userId == user.id ? 'You' : (activeChat.firstParticipant.id == user.id ? activeChat.secondParticipant.username : activeChat.firstParticipant.username)"
          avatar="../assets/default-user.jpg"
          :text="[message.content]"
          :style="message.userId == user.id ? 'text-align: right;' : 'text-align: left;'"
          size="5"
          :stamp="message.sentAt | timeStampFilter"
          :sent="message.userId == user.id"
          :bg-color="message.userId == user.id ? 'blue-5' : 'blue-2'"
        />
      </template>
    </q-scroll-area>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "ChatPanel",
  props: ["messages", "scrollTrigger"],
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
        right: "2px",
        borderRadius: "9px",
        backgroundColor: "#027be3",
        width: "9px",
        opacity: 0.2
      }
    };
  },
  updated() {
    this.scrollChatToBottom();
  },
	computed: {
    ...mapGetters(["user"])
  },
  methods: {
    scrollChatToBottom() {
      this.$refs.chat.setScrollPosition(this.$refs.chat.scrollSize, 300);
    }
  },
  watch: {
    scrollTrigger() {
      this.scrollChatToBottom();
    }
  }
};
</script>

<style scoped lang="sass">
.border-box
	position: relative
	border: 1px solid rgba(0, 0, 0, 0.12)
	border-radius: 10px
	width: 100%
.bottom-left
	position: absolute
	bottom: 20px
	left: 20px
	z-index: 200
</style>
