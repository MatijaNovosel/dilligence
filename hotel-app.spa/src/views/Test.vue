<template>
  <div>
    <v-row no-gutters justify="center">
      <v-col cols="12" class="text-center">
        <h1> Upload test </h1>
      </v-col>
      <v-col cols="6" class="mt-6">
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
      <v-col cols="1" class="ml-3 mt-6">
        <v-btn icon text class="mt-2" @click="upload">
          <v-icon>
            mdi-upload
          </v-icon>
        </v-btn>
      </v-col>
      <v-col cols="12">
        <v-card class="mx-auto my-6" max-width="40%" tile>
          <v-list dense v-if="sidebarContent != null">
            <v-row justify="center" class="mb-4 mt-3">
              {{ sidebarContent.naslov }}
            </v-row>
            <v-divider />
            <v-list-item two-line v-for="file in sidebarContent.files" :key="file.id + file.naziv">
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
                <v-btn icon text :loading="file.downloading" @click="download(file)">
                  <v-icon color="primary">
                    mdi-download
                  </v-icon>
                </v-btn>
              </v-list-item-action>
            </v-list-item>
          </v-list>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import FileService from '../services/api/file';
import KolegijService from '../services/api/kolegij';
import { fileIcon } from '../helpers/helpers.js';

export default { 
  data() {
    return {
      file: null,
      sidebarContent: null
    }
  },
  methods: {
    fileIcon,
    getData() {
      KolegijService.getKolegijSidebarContent(147)
      .then((response) => {
        response.data.results[0].files.forEach(x => x.downloading = false);
        this.sidebarContent = response.data.results[0];
      });
    },
    upload() {
      var formData = new FormData();
      formData.append("file", this.file);
      FileService.upload(formData)
      .then(() => {
        this.getData();
      });
    },
    download(item) {
      item.downloading = true;
      
      var element = document.createElement('a');
      element.setAttribute('href', `data:${item.contentType};base64, ${item.data}`);
      element.setAttribute('download', item.naziv);
      element.style.display = 'none';
      document.body.appendChild(element);
      element.click();
      document.body.removeChild(element);
      
      setTimeout(() => item.downloading = false, 500)
    }
  },
  created() {
    this.getData();
  }
};

</script>

<style>

.v-list-item--dense.v-list-item--two-line, .v-list--dense .v-list-item.v-list-item--two-line:hover {
  background-color: #f6f6f6 !important;  
}

</style>