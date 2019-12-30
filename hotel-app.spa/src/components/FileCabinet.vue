<template>
  <div>
    <v-card class="mx-auto my-6" max-width="40%" tile>
      <v-list dense v-if="content != null" class="no-padding">
        <div class="text-center">
          <v-sheet tile :color="headerColor" class="py-3">
            <span class="white--text">
              {{ content.naslov }}
            </span>
          </v-sheet>
        </div>
        <v-divider />
        <v-list-item two-line v-for="file in content.files" :key="file.id + file.naziv">
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
            <v-btn icon text :loading="file.downloading" @click="handleDownload(file)">
              <v-icon color="primary">
                mdi-download
              </v-icon>
            </v-btn>
          </v-list-item-action>
        </v-list-item>
      </v-list>
    </v-card>
  </div>
</template>

<script>

  import { download } from '../helpers/helpers';
  import { fileIcon } from '../helpers/helpers';

  export default {
    props: [ 'content', 'headerColor' ], 
    data() {
      return {
        
      }
    },
    methods: {
      download,
      fileIcon,
      handleDownload(item) {
        item.downloading = true;
        this.download(item.contentType, item.data, item.naziv);
        setTimeout(() => item.downloading = false, 500)
      }  
    }
  }
  
</script>

<style>
  .no-padding {
    padding: 0px !important;  
  }
</style>