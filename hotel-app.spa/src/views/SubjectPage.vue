<template>
  <div>
    <v-row no-gutters>
      <v-col cols="9">
        <v-row class="mt-2 mx-2" justify="center">
          <h1 class="headline font-weight-light mb-4"> {{ subject.naziv }} </h1>
           <v-col cols="12" class="px-0">
            <v-tabs grow hide-slider centered v-model="tabs" class="elevation-1 mt-n2">
              <v-tab>
                Oglasna ploča
              </v-tab>
              <v-tab>
                Informacije
              </v-tab>
              <v-tab>
                Nastavnici i suradnici
              </v-tab>
              <v-tab>
                Studenti
              </v-tab>
            </v-tabs>
            <v-tabs-items v-model="tabs">
              <!-- Oglasna ploča -->
              <v-tab-item class="elevation-1">
                <v-row class="ml-3">
                  <v-col>
                    <template v-for="item in subject.vijesti">
                      <v-card :key="item.id + item.objavio" width="98%">
                        <v-app-bar dense flat dark color="blue darken-1">
                          <v-toolbar-title class="subtitle-1"> {{ item.naslov }} </v-toolbar-title>
                          <div class="flex-grow-1"></div>
                        </v-app-bar>
                        <v-container fluid>
                          <v-row>
                            <v-col>
                              <v-list dense class="my-n5">
                                <v-list-item>
                                  <v-list-item-content>
                                    <v-list-item-title>  
                                      {{ item.objavio }} 
                                    </v-list-item-title>
                                    <v-list-item-subtitle> 
                                      {{ item.datum }} 
                                    </v-list-item-subtitle>
                                    <p class="mt-2">
                                      {{ item.opis }}
                                    </p>
                                  </v-list-item-content>
                                  <v-list-item-avatar size="60">
                                    <v-img src="../assets/TVZ/djelatnici/tin.png" />
                                  </v-list-item-avatar>
                                </v-list-item>
                              </v-list>
                            </v-col>
                          </v-row>
                        </v-container>
                      </v-card>
                    </template>
                  </v-col>
                </v-row>
              </v-tab-item>
              <!-- Informacije -->
              <v-tab-item class="elevation-1">
                <v-row class="ml-3">
                  <v-col>
                     <v-list two-line>
                      <v-list-item>
                        <v-list-item-content>
                          <v-list-item-title>
                            Ime predmeta
                          </v-list-item-title>
                          <p class="mt-2 font-weight-light"> 
                            Baze podataka 
                          </p>
                        </v-list-item-content>
                      </v-list-item>
                      <v-list-item>
                        <v-list-item-content>
                          <v-list-item-title>
                            Show your status
                          </v-list-item-title>
                          <v-list-item-subtitle>
                            Your status is visible to everyone
                          </v-list-item-subtitle>
                        </v-list-item-content>
                      </v-list-item>
                    </v-list>
                  </v-col>
                </v-row>
              </v-tab-item>
              <!-- Nastavnici i suradnici -->
              <v-tab-item class="elevation-1">
                <v-row class="ml-3">
                  <v-col>
                    <v-card width="98%">
                      <v-app-bar dense dark color="blue darken-1">
                        <v-toolbar-title class="subtitle-1">
                          Predavanja
                        </v-toolbar-title>
                        <div class="flex-grow-1" />
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">
                                    Tin Kramberger
                                  </v-list-item-title>
                                  <v-list-item-subtitle>
                                    struč. spec. tehn. inf. ing.
                                  </v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                  <v-img src="../assets/TVZ/djelatnici/tin.png" />
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                    <v-card width="98%" class="mt-3">
                      <v-app-bar dense dark color="blue darken-1">
                        <v-toolbar-title class="subtitle-1">
                          Auditorne vježbe
                        </v-toolbar-title>
                        <div class="flex-grow-1" />
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">
                                    Tin Kramberger
                                  </v-list-item-title>
                                  <v-list-item-subtitle>
                                    struč. spec. tehn. inf. ing.
                                  </v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                  <v-img src="../assets/TVZ/djelatnici/tin.png" />
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                    <v-card width="98%" class="mt-3">
                      <v-app-bar dense dark color="blue darken-1">
                        <v-toolbar-title class="subtitle-1">
                          Laboratorijske vježbe
                        </v-toolbar-title>
                        <div class="flex-grow-1" />
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">
                                    Tin Kramberger
                                  </v-list-item-title>
                                  <v-list-item-subtitle>
                                    struč. spec. tehn. inf. ing.
                                  </v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                  <v-img src="../assets/TVZ/djelatnici/tin.png" />
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">
                                    Renata Kovačević
                                  </v-list-item-title>
                                  <v-list-item-subtitle>
                                    struč. spec. tehn. inf. ing.
                                  </v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">
                                    Briggita Cafuta
                                  </v-list-item-title>
                                  <v-list-item-subtitle>
                                    asistent 
                                  </v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                  </v-col>
                </v-row>
              </v-tab-item>
              <!-- Studenti -->
              <v-tab-item class="elevation-1">
                <v-row class="ml-3">
                  <v-col>
                    <v-data-table dense
                                  :headers="headers" 
                                  :items="subject.studenti"
                                  :items-per-page="studentOptions.pages"
                                  class="elevation-1">
                      <template v-slot:item.picture="{ item }">
                        <v-avatar size="40" class="my-1" @click="showInfo(item)">
                          <v-img :src="require(`../assets/default-user.jpg`)" />
                        </v-avatar>
                      </template>
                    </v-data-table>
                  </v-col>
                </v-row>
              </v-tab-item>
            </v-tabs-items>
          </v-col>
        </v-row>
      </v-col>
      <v-col cols="3">
        <v-row justify="center">
          <v-card class="mx-auto mt-5" width="85%">
            <v-app-bar dense dark color="indigo darken-2 elevation-2">
              <v-toolbar-title class="subtitle-1">Predavanja</v-toolbar-title>
              <div class="flex-grow-1"></div>
            </v-app-bar>
            <v-container fluid>
              <v-row>
                <v-col>
                  <v-list dense class="my-n5">
                    <v-list-item v-for="item in predavanja" :key="item.title" @click="clicky()">
                      <v-list-item-icon class="mt-3">
                        <v-icon :color="item.color">
                          {{ item.icon }}
                        </v-icon>
                      </v-list-item-icon>
                      <v-list-item-content>
                        <v-list-item-title v-text="item.title"></v-list-item-title>
                        <v-list-item-subtitle v-text="item.date"></v-list-item-subtitle>
                      </v-list-item-content>
                    </v-list-item>
                  </v-list>
                </v-col>
              </v-row>
            </v-container>
          </v-card>
        </v-row>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import KolegijService from '../services/api/kolegij';

export default { 
  data() {
    return {
      tabs: null,
      predavanja: [
        { title: '01 - Uvod, DBMS, PK, FK, Dizajn', date: "25/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '01 - Uvod, DBMS, PK, FK, Dizajn - primjeri', date: "25/09/2019", icon: "mdi-database", color: "grey" },
        { title: '02 - Normalizacija, operatori, tipovi', date: "25/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '02 - Normalizacija, operatori, tipovi - primjeri', date: "25/09/2019", icon: "mdi-database", color: "grey" },
        { title: '03 - DDL, DML, WHERE', date: "25/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '03 - DDL, DML, WHERE - primjeri', date: "25/09/2019", icon: "mdi-database", color: "grey" },
        { title: '04 - Funkcije', date: "25/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '04 - Funkcije - primjeri', date: "25/09/2019", icon: "mdi-database", color: "grey" }
      ],
      dialog: false,
      headers: [
        { text: 'Slika', value: 'picture', sortable: false, align: 'center' },
        { text: 'JMBAG', value: 'jmbag', sortable: false, align: 'center' },
        { text: 'Ime', value: 'ime', sortable: false, align: 'center' },
        { text: 'Prezime', value: 'prezime', sortable: false, align: 'center' },
        { text: 'Email', value: 'email', sortable: false, align: 'center' }
      ],
      information: {
        sifraISVU: "23079/85254",
        ECTS: 5,
        akademskaGodina: "2018/2019",
        naziv: "Baze podataka",
        status: "4. semestar - Organizacija i informatizacija ureda (Redovni informatika)",
        izvedbaNastave: "Predavanja + vježbe (auditorne+laboratorij+seminar+konstrukcijske), Samostalan rad - 15 + 45 (15 + 30 + 0 + 0), 90",
        izvodaci: "Predavanja: Tin Kramberger",
        ciljPredmeta: "Studenti trebaju upoznati koncept, mogućnosti i ulogu baze podataka i sustava za pretraživanje informacija u informacijskom sustavu. Praktičan rad sa sustavom za upravljanje bazom podataka omogućit će studentu da upozna i ovlada različitim metodama rukovanja s bazom.",
        ishodiUcenja: "1. Konstruirati model baze podataka"
      },
      selectedItem: { 
        Ime: null,
        JMBAG: null
      },
      loading: true,
      subject: {},
      studentOptions: {
        pages: 15
      }
    }
  },
  beforeRouteEnter(to, from, next) {
    next(vm => {
      vm.getData(to.params.id);
    });
  },
  methods: {
    showInfo: function(item) {
      this.selectedItem = item;
      this.dialog = !this.dialog;
    },
    getData(id) {
      KolegijService.getKolegijExtended(id)
      .then(({ data }) => {
        this.subject = data;
      });
    }
  }
};

</script>