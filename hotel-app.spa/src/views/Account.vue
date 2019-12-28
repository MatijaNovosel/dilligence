<template>
  <div>
    <v-dialog v-model="showExpandedPicture" max-width="300">
      <v-card class="mx-auto">
        <v-card-text>
          <v-row>
            <v-avatar size="250" class="mx-auto mt-5">
              <img src="../assets/default-user.jpg">
            </v-avatar>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
    <v-row class="mt-5">
      <v-col>
        <v-row justify="center">
          <v-avatar @mouseenter="hoverOverPicture = true" @mouseleave="hoverOverPicture = false" :class="hoverObj" size="130" @click="expandPicture">
            <img src="../assets/default-user.jpg">
          </v-avatar>
          <v-btn class="ml-n6" color="blue" fab dark x-small>
            <v-icon>
              mdi-pencil
            </v-icon>
          </v-btn>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-2" justify="center">
          <h1 class="display-1"> 
            Account settings 
          </h1>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-2" justify="center">
          <h1 class="subtitle-1 font-weight-light"> 
            Manage your settings and personal information 
          </h1>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters class="mt-5">
      <v-col>
        <v-row class="mt-2" justify="center">
          <v-card class="mx-auto" width="55%" color="#F9F9F9">
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title class="headline">
                  {{ `${accountInfo.name} ${accountInfo.surname}` }}
                </v-list-item-title>
                <v-list-item-subtitle>
                  Registered on {{ date }}
                </v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
            <v-list dense class="py-8">
              <v-list-item> 
                  <v-text-field outlined type="text" v-model="organisation" label="Course" :rules="[rules.required]" prepend-icon="mdi-school" readonly />
              </v-list-item>
              <v-list-item>
                  <v-select prepend-icon="mdi-web" :items="languages" label="Localization" outlined v-model="localization" />
              </v-list-item>
              <v-list-item class="my-n5">
                <v-switch prepend-icon="mdi-moon-waning-crescent" color="primary" inset v-model="darkMode" :label="'Dark mode'" />
              </v-list-item>
            </v-list>
            <v-divider />
            <v-card-actions>
              <v-row justify="center">
                <v-btn class="ma-2 primary">
                  Save
                </v-btn>
              </v-row>
            </v-card-actions>
          </v-card>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import store from '../store/store';

export default { 
  data() {
    return {
      modal: false,
      menu2: false,
      darkMode: false,
      showExpandedPicture: false,
      hoverOverPicture: false,
      languages: [ 'English', 'Croatian' ],
      localization: "English",
      organisation: "Information technologies",
      date: new Date().toISOString().substr(0, 10),
      accountInfo: {
        name: null,
        surname: null  
      },
      rules: {
        required: value => !!value || "Required"
      }
    }
  },
  methods: {
    expandPicture: function() {
      this.showExpandedPicture = true;
    }
  },
  created() {
    this.accountInfo.name = store.getters.user.name;  
    this.accountInfo.surname = store.getters.user.surname;  
  },
  computed: {
    hoverObj: function() {
      return {
        'elevation-8': this.hoverOverPicture,
        'elevation-2': !this.hoverOverPicture
      }
    }
  }
};

</script>

<style>
  .v-text-field.v-text-field--enclosed .v-input__prepend-outer, .v-text-field.v-text-field--enclosed .v-input__prepend-inner, .v-text-field.v-text-field--enclosed .v-input__append-inner, .v-text-field.v-text-field--enclosed .v-input__append-outer {
    margin-right: 20px;
  }
  .v-input--selection-controls .v-input__append-outer, .v-input--selection-controls .v-input__prepend-outer {
    margin-right: 20px;  
  }
</style>