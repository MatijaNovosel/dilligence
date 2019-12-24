<template>
  <div>
    <v-row no-gutters justify="center">
      <v-col cols="5">
        <v-row class="mt-2" justify="center">
          <v-chip-group v-model="searchData.smjerIDs" 
                        mandatory 
                        multiple 
                        column 
                        active-class="white--text blue darken-2">
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
            <v-toolbar dark flat dense color="primary">
              <v-icon dark class="mr-2">
                mdi-text-subject
              </v-icon>
              <v-toolbar-title> Kolegiji </v-toolbar-title>
              <v-spacer> </v-spacer>
              <v-btn icon @click="searchEnabled = !searchEnabled">
                <v-icon> mdi-filter-menu </v-icon>
              </v-btn>
              <v-btn icon @click="resetForm">
                <v-icon> mdi-filter-remove </v-icon>
              </v-btn>
              <v-btn icon @click="getData" class="mr-4">
                <v-icon> mdi-magnify </v-icon>
              </v-btn>
            </v-toolbar>
            <v-row class="mx-3 mt-4" justify="center" v-show="searchEnabled">
              <v-col>
                <v-text-field label="Ime kolegija" 
                              v-model="searchData.name"> 
                </v-text-field>
              </v-col>
              <v-col>
                <v-text-field label="ISVU" 
                              v-model="searchData.ISVU"> 
                </v-text-field>
              </v-col>
              <v-col>              
                <v-range-slider v-model="searchData.ECTS"
                          label="ECTS"
                          min="1"
                          max="6"
                          class="mt-6"
                          thumb-label="always">
                </v-range-slider>
              </v-col>
            </v-row>
            <v-list three-line subheader>
              <v-divider />
              <template v-for="(item, i) in subjects">
                <v-list-item :key="item.naziv + item.id">
                  <v-list-item-avatar @click="redirectToKolegijDetails(item)" size="50" color="primary" class="justify-center">
                    <span class="white--text headline"> {{ Helper.acronym(item.naziv) }} </span>
                  </v-list-item-avatar>
                  <v-list-item-content>
                    <v-list-item-title v-html="item.naziv"></v-list-item-title>
                    <v-list-item-subtitle>
                      <b>ECTS: </b>{{ item.ects }}
                    </v-list-item-subtitle>
                    <v-list-item-subtitle>
                      <b>Smjer: </b>{{ getSubjectNameFromID(item.smjer) }}
                    </v-list-item-subtitle>
                  </v-list-item-content>
                  <v-list-item-action class="mt-8 mr-5">
                    <v-checkbox v-model="item.subscribed" 
                                color="primary"
                                @change="handleChange($event, item.id)" />
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
  import PretplataService from '../services/api/pretplata';
  import NotificationService from '../services/notification';
  import { Smjer } from '../constants/Smjer';
  import { Helper } from '../helpers/helpers.js';
  import { mapGetters } from 'vuex';

  export default { 
    data() {
      return {
        subscriptions: [],
        subjects: [],
        totalSubjects: 0,
        loading: null,
        tags: [],
        searchEnabled: true,
        Helper: null,
        searchData: {
          smjerIDs: [ Smjer["Informatika"] - 1 ],
          name: null,
          ECTS: [ 1, 6 ],
          ISVU: null 
        }
      }
    },
    created() {
      this.Helper = Helper;
      for(let prop in Smjer) {
        this.tags.push({
          "name": prop,
          "value": Smjer[prop]
        });
      }
      this.getData();
    },
    methods: {
      getData() {
        this.loading = true;
        PretplataService.getPretplata(this.user.id).then((response) => {
          let subscriptions = response.data;
          this.subscriptions = subscriptions;
          KolegijService.getKolegiji(this.searchData.smjerIDs.map(x => x + 1), this.searchData.name, this.searchData.ECTS[0], this.searchData.ECTS[1], this.searchData.ISVU, 0, null)
          .then(({ data }) => {
            data.results.forEach(x => {
              x.subscribed = subscriptions.includes(x.id) ? true : false;
            });
            this.subjects = data.results;
            this.totalSubjects = data.total;
          }).finally(() => {
            this.loading = false;
          });
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
        this.$router.push({ 
          name: 'subject-details', 
          params: { id: item.id } 
        });
      },
      resetForm() {
        this.searchData = {
          smjerIDs: [ Smjer["Informatika"] - 1 ],
          name: null,
          ECTS: [ 1, 6 ],
          ISVU: null 
        };
        this.getData();
      },
      getSubjectNameFromID(id) {
        return Object.keys(Smjer).find(key => Smjer[key] === id);
      },
      handleChange(e, id) {
        if(e) {
          this.subscriptions.push(id);
        } else {
          this.subscriptions = this.subscriptions.filter(x => x != id);
        }
        PretplataService.postPreplata(this.user.id, this.subscriptions)
        .then(() => {
          NotificationService.success("Pretplata", "Pretplata uspjesno promijenjena!");
        });
      }
    },
    computed: {
      ...mapGetters([
        'user'
      ])
    }
  };

</script>

<style scoped>
  .v-avatar:hover {
    cursor: pointer;
    background-color: #292826 !important;
  }
</style>