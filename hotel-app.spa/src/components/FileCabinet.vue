<template>
  <div>
    <v-card class="mx-auto my-6" max-width="40%" tile>
      <v-list dense v-if="content != null" class="no-padding">
        <div class="text-center">
          <v-sheet tile :color="headerColor" class="py-3">
            <v-row justify="center">
              <span class="white--text">
                {{ content.naslov }}
              </span>
              <v-btn icon text class="left-abs" @click="addFileDialog = true">
                <v-icon color="white">
                  mdi-plus-circle
                </v-icon>
              </v-btn>
            </v-row>
          </v-sheet>
        </div>
        <v-divider />
        <v-list-item two-line v-for="(file, index) in content.files" :key="file.id + file.naziv" :class="{ shaded: index % 2, nonShaded: !(index % 2) }">
          <v-list-item-icon class="mt-4">
            <v-icon size="25">
              {{ fileIcon(file.naziv.slice(file.naziv.lastIndexOf(".") + 1)) }}
            </v-icon>
          </v-list-item-icon>
          <v-divider class="ml-n4 mr-4" vertical />
          <v-list-item-content>
            <v-list-item-title>
              {{ file.naziv }} 
            </v-list-item-title>
            <v-list-item-subtitle> 
              {{ file.contentType }} 
            </v-list-item-subtitle>
          </v-list-item-content>
          <v-list-item-action>
            <v-row>
              <v-btn icon text :loading="file.downloading" @click="handleDownload(file)">
                <v-icon color="primary">
                  mdi-download
                </v-icon>
              </v-btn>
              <v-btn icon text>
                <v-icon color="primary">
                  mdi-delete
                </v-icon>
              </v-btn>
            </v-row>
          </v-list-item-action>
        </v-list-item>
        <v-list-item v-show="itemUploading">
          <v-list-item-icon class="my-4 skeleton-icon">
            <v-skeleton-loader type="avatar">
            </v-skeleton-loader>
          </v-list-item-icon>
          <v-divider class="ml-n4 mr-4" vertical />
          <v-list-item-content>
            <v-list-item-title>
              <v-skeleton-loader type="sentences">
              </v-skeleton-loader>
            </v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-card>
    <v-dialog v-model="addFileDialog" max-width="600">
      <v-toolbar dense dark flat color="primary">
        <v-spacer></v-spacer>
        <v-btn icon @click="addFileDialog = false">
          <v-icon dark>
            mdi-close
          </v-icon>
        </v-btn>
      </v-toolbar>
      <v-card>
        <v-card-text>
          <v-row class="pt-2">
            <v-col cols="11">
              <v-file-input v-model="file"
                            color="primary"
                            counter
                            label="Single file upload"
                            placeholder="Select your file"
                            prepend-icon="mdi-paperclip"
                            outlined
                            :show-size="1000">
              </v-file-input>
            </v-col>
            <v-col cols="1">
              <v-btn icon text class="mt-3" @click="upload">
                <v-icon>
                  mdi-upload
                </v-icon>
              </v-btn>
            </v-col>
          </v-row>
          <v-divider class="my-3" />
          <v-row class="pt-2">
            <v-col cols="11">
              <v-file-input v-model="files"
                            color="primary"
                            counter
                            label="Multiple file upload"
                            placeholder="Select your files"
                            prepend-icon="mdi-paperclip"
                            outlined
                            multiple
                            :show-size="1000">
              </v-file-input>
            </v-col>
            <v-col cols="1">
              <v-btn icon text class="mt-3" @click="uploadMultiple">
                <v-icon>
                  mdi-upload
                </v-icon>
              </v-btn>
            </v-col>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>

import { download, fileIcon } from '../helpers/helpers'
import FileService from '../services/api/file';
import KolegijService from '../services/api/kolegij';

export default {
  props: [ 'content', 'headerColor' ], 
  data() {
    return {
      addFileDialog: false,
      file: null,
      files: null,
      itemUploading: false
    }
  },
  methods: {
    download,
    fileIcon,
    handleDownload(item) {
      item.downloading = true;
      this.download(item.contentType, item.data, item.naziv);
      setTimeout(() => item.downloading = false, 500)
    },
    upload() {
      var formData = new FormData();
      formData.append("file", this.file);
      FileService.upload(formData);
    },
    uploadMultiple() {
      this.itemUploading = true;
      var formData = new FormData();
      formData.set("files", null);
      this.files.forEach(x => formData.append("files", x))
      FileService.uploadMultiple(formData)
      .then(({ data }) => {
        KolegijService.connectSidebarFile(1, data);
      })
      .finally(() => {
        this.$emit("doneUploading");
        this.itemUploading = false;
      });
    }
  }
}
  
</script>

<style>
  .no-padding {
    padding: 0px !important;  
  }
  .left-abs {
    position: absolute;
    right: 20px;
    top: 5px;
  }
  .nonShaded:hover {
    background-color: #ebebeb;  
  }
  .shaded {
    background-color: #f6f6f6;  
  }
  .shaded:hover {
    background-color: #e6e6e6;  
  }
  .skeleton-icon {
    width: 20px;
  }
  .v-skeleton-loader__text {
    height: 16px !important;
  }
</style>