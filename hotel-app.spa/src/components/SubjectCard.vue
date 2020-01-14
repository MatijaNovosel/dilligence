<template>
  <div>
    <v-card outlined>
      <v-menu bottom left allow-overflow>
        <template v-slot:activator="{ on }">
          <v-btn icon v-on="on" class="gore-desno">
            <v-icon color="white">
              mdi-dots-vertical
            </v-icon>
          </v-btn>
        </template>
        <v-list dense>
          <v-list-item class="my-n2">
            <v-list-item-title>
              Vidi detalje
            </v-list-item-title>
          </v-list-item>
          <v-divider class="my-2" />
          <v-list-item class="my-n2">
            <v-list-item-title>
              Odjavi predmet
            </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
      <div class="bg-header">
        <v-list-item>
          <v-list-item-avatar color="white" />
          <v-list-item-content class="ml-n2">
            <v-list-item-title class="subtitle-1 white--text">
              {{ subject.naziv }}
            </v-list-item-title>
            <v-list-item-subtitle>
              Smjer: {{ subject.smjer }}
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </div>
      <v-divider />
      <v-card-text>
        Text
      </v-card-text>
    </v-card>
  </div>
</template>

<script>

import { Smjer } from '../constants/Smjer';

export default { 
  props: [ 'subject' ],
  data() {
    return {
      Smjer
    }
  },
  methods: {
    
  }
};

</script>

<style scoped>
  .bg-header {
    background-image: url("../assets/nav-bg.svg");
    background-position: bottom;
    background-size: cover;
    z-index: -1;
  }
  .gore-desno {
    position: absolute;
    right: 10px;
    top: 10px;
    z-index: 1;
  }
</style>