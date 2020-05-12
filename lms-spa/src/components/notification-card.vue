<template>
  <q-card>
    <q-menu touch-position context-menu>
      <q-list
        :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
        dense
        separator
        style="min-width: 100px; border-radius: 6px;"
      >
        <q-item clickable v-close-popup @click="$deleteNotification">
          <q-item-section>Delete notification</q-item-section>
        </q-item>
        <q-item clickable v-close-popup>
          <q-item-section>Archive notification</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
    <q-bar dense :style="`background-color: ${value.color}; height: 10px;`" />
    <q-card-section class="q-py-sm">
      <div class="text-h6">{{ value.title }}</div>
      <div
        class="text-subtitle2"
      >{{ `${value.submittedBy}, ${$options.filters.timeStampFilter(value.submittedAt)}` }}</div>
    </q-card-section>
    <q-separator />
    <q-card-section class="q-py-sm">
      <span v-html="value.description" />
    </q-card-section>
    <template v-if="value.attachments && value.attachments.length != 0">
      <q-separator />
      <q-card-section class="q-py-sm">
        <div style="font-size: 14px" :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']">
          Attachments
          <q-icon name="mdi-paperclip" />
        </div>
        <q-list dense>
          <q-item dense v-for="(attachment, i) in value.attachments" :key="i">
            <q-item-section avatar>
              <q-icon size="xs" :name="fileIcon(attachment.contentType)" />
            </q-item-section>
            <q-item-section>{{ attachment.name }}</q-item-section>
            <q-item-section side>{{ attachment.size | byteCountToReadableFormat }}</q-item-section>
            <q-item-section side>
              <q-btn
                @click="downloadFile(attachment)"
                :loading="attachment.downloading"
                size="sm"
                flat
                round
                icon="mdi-download"
              />
            </q-item-section>
          </q-item>
        </q-list>
      </q-card-section>
    </template>
  </q-card>
</template>

<script>
import { download, fileIcon } from "../helpers/helpers";

export default {
  name: "notification-card",
  props: ["value"],
  created() {
    this.value.attachments.forEach(x => (x.downloading = false));
  },
  data() {
    return {};
  },
  methods: {
    fileIcon,
    downloadFile(attachment) {
      download(attachment.contentType, attachment.data, attachment.name);
    },
    $deleteNotification() {
      this.$emit("deleteNotification", this.value.id);
    }
  }
};
</script>
