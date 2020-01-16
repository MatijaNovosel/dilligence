<template>
  <div>
    <v-row no-gutters justify="center">
      <v-col cols="12" class="text-center">
        <h1> Upload test </h1>
      </v-col>
      <v-col cols="12">
        <v-skeleton-loader class="mx-auto" type="card" max-width="40%" :loading="sidebarContent == null">
          <file-cabinet :content="sidebarContent" headerColor="primary" />
        </v-skeleton-loader>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import KolegijService from '../services/api/kolegij';
import FileCabinet from '../components/FileCabinet';

export default { 
  components: { FileCabinet },
  data() {
    return {
      sidebarContent: null
    }
  },
  methods: {
    getData() {
      KolegijService.getKolegijSidebarContent(147)
      .then(({ data }) => {
        data.results[0].files.forEach(x => x.downloading = false);
        this.sidebarContent = data.results[0];
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