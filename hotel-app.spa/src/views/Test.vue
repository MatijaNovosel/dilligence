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
                      label="File input"
                      placeholder="Select your files"
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
    </v-row>
  </div>
</template>

<script>

import FileService from '../services/api/file';

export default { 
  data() {
    return {
      file: null
    }
  },
  methods: {
    upload() {
      FileService.upload(this.file);
    }
  }
};

</script>