<template>
  <div>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-2" justify="center">
          <v-chip-group v-model="chipSelection" mandatory multiple column active-class="white--text blue darken-2">
            <v-chip v-for="item in tags" :key="item.value">
              {{ item.name }}
            </v-chip>
          </v-chip-group>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-5">
          <v-card width="75%" class="mx-auto" :loading="loading">
            <v-toolbar dark flat color="primary">
              <v-icon dark class="mr-2">
                mdi-text-subject
              </v-icon>
              <v-toolbar-title> Kolegiji </v-toolbar-title>
              <v-spacer> </v-spacer>
              <v-btn icon @click="searchEnabled = !searchEnabled">
                <v-icon> mdi-filter-menu </v-icon>
              </v-btn>
              <v-btn icon class="mr-4">
                <v-icon> mdi-filter-remove </v-icon>
              </v-btn>
            </v-toolbar>
            <v-expand-transition>
              <v-row class="mx-3 my-2" justify="center" v-show="searchEnabled">
                <v-col>
                  <v-text-field label="Ime kolegija" 
                                v-model="subjectName"> 
                  </v-text-field>
                </v-col>
                <v-col>
                  <v-text-field label="ISVU" 
                                v-model="ISVU"> 
                  </v-text-field>
                </v-col>
                <v-col>              
                  <v-range-slider v-model="ECTS"
                            label="ECTS"
                            min="1"
                            max="6"
                            class="mt-6"
                            thumb-label="always">
                  </v-range-slider>
                </v-col>
              </v-row>
             </v-expand-transition>
            <v-list three-line subheader>
              <v-divider></v-divider>
              <template v-for="(item, i) in subjects">
                <v-list-item :key="item.naziv + item.id">
                  <v-list-item-avatar @click="redirectToKolegijDetails(item)" size="50" color="primary" class="justify-center">
                    <span class="white--text headline"> {{ Helper.acronym(item.naziv) }} </span>
                  </v-list-item-avatar>
                  <v-list-item-content>
                    <v-list-item-title v-html="item.naziv"></v-list-item-title>
                    <v-list-item-subtitle><b>ECTS: </b>{{ item.ects }}</v-list-item-subtitle>
                    <v-list-item-subtitle><b>ISVU: </b>{{ item.isvu }}</v-list-item-subtitle>
                  </v-list-item-content>
                  <v-list-item-action class="mt-8 mr-5">
                    <v-checkbox color="primary" class="mt-1"> </v-checkbox>
                  </v-list-item-action>
                </v-list-item>
                <v-divider :key="i" v-if="i < subjects.length - 1" />
              </template>
            </v-list>
          </v-card>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import KolegijService from '../services/api/kolegij'; 
import { SmjerInformation } from '../constants/smjerInformation';
import { Helper } from '../helpers/helpers.js';

export default { 
  data() {
    return {
      subjects: [],
      totalSubjects: 0,
      loading: true,
      chipSelection: [ SmjerInformation.inf ],
      replacementSubjects: [],
      tags: [],
      searchEnabled: false,
      Helper: null,
      ECTS: [1, 6],
      ISVU: null,
      subjectName: null
    }
  },
  created() {
    this.Helper = Helper;
    for(let prop in SmjerInformation) {
      this.tags.push({
        "name": prop,
        "value": SmjerInformation[prop]
      });
    }
    this.getData();
  },
  methods: {
    getData() {
      this.loading = true;
      KolegijService.getKolegijBySmjerID(this.chipSelection.map(x => x + 1), 0, null)
      .then(({ data }) => {
        this.subjects = data.results;
        this.replacementSubjects = data.results;
        this.totalSubjects = data.total;
      }).finally(() => {
        this.loading = false;
      });
    },
    buildPath(name) {
      return require(`../assets/TVZ/subjects/${name}.png`);
    },
    showInfo(item) {
      this.selectedItem = item;
      this.dialog = !this.dialog;
    },
    redirectToKolegijDetails(item) {
      this.$router.push({ name: 'subject-details', params: { id: item.id } });
    }
  },
  watch: {
    chipSelection: {
      handler: function(val) {
        if(val) {
          this.getData();
        }
      },
      deep: true
    }
  }
};

</script>

<style scoped>
  .v-avatar:hover {
    cursor: pointer;
    background-color: #292826 !important;
  }
</style>