<template>
  <div>
    <v-dialog v-model="showExpandedPicture" max-width="300">
      <v-card class="mx-auto">
        <v-card-text>
          <v-row>
            <v-avatar size="250" class="mx-auto mt-5">
              <img src="../assets/matija.png">
            </v-avatar>
          </v-row>
        </v-card-text>
      </v-card>
    </v-dialog>
    <v-row class="mt-5">
      <v-col>
        <v-row justify="center">
          <v-avatar @mouseenter="hoverOverPicture = true" @mouseleave="hoverOverPicture = false" :class="hoverObj" size="130" @click="expandPicture()">
            <img src="../assets/matija.png">
          </v-avatar>
          <v-btn class="ml-n6" color="blue" fab dark x-small>
            <v-icon>mdi-pencil</v-icon>
          </v-btn>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-2" justify="center">
          <h1 class="display-1"> Account settings </h1>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-2" justify="center">
          <h1 class="subtitle-1"> Manage your settings and personal information </h1>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters class="mt-5">
      <v-col>
        <v-row class="mt-2" justify="center">
          <v-card class="mx-auto" color="#F9F9F9">
            <v-list-item two-line>
              <v-list-item-content>
                <v-list-item-title class="headline">Matija Novosel</v-list-item-title>
                <v-list-item-subtitle>Registered on 25th of September, 2019</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
            <v-list dense>
              <v-list-item class="mt-5">
                <v-text-field outlined clearable type="text" v-model="name" label="Name" :rules="[rules.required]" prepend-icon="mdi-alpha-n-box"></v-text-field>
              </v-list-item>
              <v-list-item>
                  <v-text-field outlined clearable type="text" v-model="surname" label="Surname" :rules="[rules.required]" prepend-icon="mdi-alpha-s-box"></v-text-field>
              </v-list-item>
              <v-list-item>
                  <v-text-field outlined type="text" v-model="organisation" label="Course" :rules="[rules.required]" prepend-icon="mdi-school" readonly></v-text-field>
              </v-list-item>
              <v-list-item>
                  <v-select prepend-icon="mdi-web" :items="languages" label="Localization" outlined v-model="localization"></v-select>
              </v-list-item>
              <v-list-item>
                <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition" offset-y full-width min-width="290px">
                  <template v-slot:activator="{ on }">
                    <v-text-field :rules="[rules.required]" v-model="date" outlined label="Birth date" clearable prepend-icon="mdi-cake" v-on="on"></v-text-field>
                  </template>
                  <v-date-picker v-model="date" @input="menu2 = false"></v-date-picker>
                </v-menu>
              </v-list-item>
              <v-list-item class="my-n5">
                <v-col cols="1" class="mt-n2">
                  <v-row justify="center">
                    <v-icon>mdi-moon-waning-crescent</v-icon>
                  </v-row>
                </v-col>
                <v-col cols="11" class="ml-2">
                  <v-row justify="left">
                    <v-switch color="primary" inset v-model="darkMode" :label="'Dark mode'" />
                  </v-row>
                </v-col>
              </v-list-item>
            </v-list>
            <v-divider></v-divider>
            <v-card-actions>
              <v-row justify="center">
                <v-btn class="ma-2 primary">Save</v-btn>
              </v-row>
            </v-card-actions>
          </v-card>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>

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
      name: "Matija",
      surname: "Novosel",
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
  computed: {
    hoverObj: function() {
      return {
        'elevation-12': this.hoverOverPicture,
        'elevation-2': !this.hoverOverPicture
      }
    }
  }
};

</script>