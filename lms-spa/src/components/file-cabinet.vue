<template>
  <div>
    <q-card class="q-mx-auto q-my-lg cabinet-card" flat bordered>
      <div class="text-center justify-center q-py-sm bg-primary">
        <span class="caption text-white">{{ content.naslov }}</span>
        <div class="right-abs">
          <q-btn
            :ripple="false"
            dense
            size="sm"
            color="white"
            flat
            round
            icon="mdi-plus-box-multiple"
            @click="addFileDialog = !addFileDialog"
          >
            <q-tooltip>{{ $i18n.t('uploadFiles') }}</q-tooltip>
          </q-btn>
          <q-btn
            :ripple="false"
            dense
            size="sm"
            color="white"
            flat
            round
            class="q-ml-sm"
            :icon="downloadMultiple ? 'mdi-lock-open-variant' : 'mdi-lock'"
            @click="changeDownloadToMultiple"
          >
            <q-tooltip>{{ $i18n.t('downloadMultiple') }}</q-tooltip>
          </q-btn>
        </div>
      </div>
      <q-list separator v-if="content != null">
        <q-separator />
        <q-item
          :class="{ shaded: index % 2, nonShaded: !(index % 2) }"
          v-for="(file, index) in content.files"
          :key="file.id + file.name"
        >
          <q-item-section avatar>
            <q-icon
              class="q-pl-md"
              color="primary"
              :name="fileIcon(file.name.slice(file.name.lastIndexOf('.') + 1))"
            />
          </q-item-section>
          <q-separator vertical />
          <q-item-section class="q-pl-md">
            <q-item-label>{{ file.name }}</q-item-label>
            <q-item-label caption>{{ file.contentType }}</q-item-label>
          </q-item-section>
          <q-item-section side>
            <q-btn
              :ripple="false"
              dense
              size="sm"
              flat
              round
              v-if="!downloadMultiple"
              icon="mdi-download"
              @click="addFileDialog = !addFileDialog"
            />
            <q-checkbox
              class="q-pr-xs"
              v-else
              v-model="downloadMultipleSelection"
              dense
              :val="file.id"
              color="primary"
              size="sm"
            ></q-checkbox>
          </q-item-section>
        </q-item>
        <q-item class="justify-center" v-show="downloadMultiple">
          <q-space />
          <q-btn size="sm" dense color="primary" @click="downloadMultipleFiles">{{ $i18n.t('download') }}</q-btn>
          <q-space />
          <q-btn
            :ripple="false"
            dense
            size="sm"
            flat
            round
            class="select-all"
            v-if="downloadMultiple"
            icon="mdi-check-box-multiple-outline"
          >
            <q-tooltip>{{ $i18n.t('selectAll') }}</q-tooltip>
          </q-btn>
        </q-item>
      </q-list>
    </q-card>
    <q-dialog v-model="addFileDialog" persistent>
      <q-card class="upload-dialog">
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
            @click="resetDialog"
          />
        </q-toolbar>
        <q-card-section>
          <q-file
            dense
            multiple
            outlined
            v-model="files"
            clearable
            :label="$i18n.t('uploadMultipleFiles')"
          >
            <template v-slot:prepend>
              <q-icon name="mdi-paperclip" />
            </template>
          </q-file>
        </q-card-section>
        <q-card-actions class="q-pt-none">
          <q-space />
          <q-btn
            :ripple="false"
            dense
            :loading="itemUploading"
            :disabled="itemUploading"
            size="sm"
            color="primary"
            @click="upload"
          >{{ $i18n.t('upload') }}</q-btn>
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import { download, fileIcon } from "../helpers/helpers";
import FileService from "../services/api/file";

export default {
  name: "file-cabinet",
  props: ["content", "headerColor"],
  data() {
    return {
      downloadMultipleSelection: [],
      downloadMultiple: false,
      addFileDialog: false,
      files: null,
      itemUploading: false,
      closeAfterCompletion: false
    };
  },
  methods: {
    download,
    fileIcon,
    downloadMultipleFiles() {
      console.log("he hee");
    },
    changeDownloadToMultiple() {
      [this.downloadMultiple, this.downloadMultipleSelection] = [
        !this.downloadMultiple,
        []
      ];
    },
    resetDialog() {
      this.addFileDialog = false;
      this.files = null;
    },
    handleDownload(item) {
      item.downloading = true;
      this.download(item.contentType, item.data, item.name);
      setTimeout(() => (item.downloading = false), 500);
    },
    upload() {
      this.itemUploading = true;
      var formData = new FormData();
      this.files.forEach(x => formData.append("files", x));
      FileService.upload(formData).finally(() => {
        this.$emit("doneUploading");
        this.itemUploading = false;
      });
    },
    uploadMultipleWithSidebar() {
      this.itemUploading = true;
      var formData = new FormData();
      this.files.forEach(x => formData.append("files", x));
      FileService.uploadSidebar(formData, this.content.id).finally(() => {
        this.$emit("doneUploading");
        this.itemUploading = false;
        if (this.closeAfterCompletion) {
          this.addFileDialog = false;
        }
        this.files = null;
      });
    },
    deleteFile(file) {
      file.deleting = true;
      FileService.deleteFile(file.id).finally(() => {
        this.content.files = this.content.files.filter(x => x.id != file.id);
        file.deleting = false;
      });
    }
  }
};
</script>

<style lang="sass" scoped>
.right-abs
	position: absolute
	right: 8px
	top: 8px
.nonShaded:hover
	background-color: #ebebeb
.shaded
	background-color: #f6f6f6
.shaded:hover
	background-color: #e6e6e6
.cabinet-card
	max-width: 45%
.dialog-toolbar
	min-height: 30px
.upload-dialog
	width: 80vh
	max-width: 80vw
.q-item__section--main ~ .q-item__section--side
	padding-left: 0px
.select-all
	position: absolute
	right: 15px
	top: 12px
</style>