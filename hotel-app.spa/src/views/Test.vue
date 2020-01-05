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
      <v-col cols="12" class="my-4">
        <v-skeleton-loader class="mx-auto" type="card" max-width="40%" :loading="sidebarContent == null">
          <file-cabinet :content="sidebarContent" headerColor="primary" />
        </v-skeleton-loader>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import FileService from '../services/api/file';
import KolegijService from '../services/api/kolegij';
import FileCabinet from '../components/FileCabinet';

export default { 
  components: { FileCabinet },
  data() {
    return {
      file: null,
      sidebarContent: null
    }
  },
  methods: {
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