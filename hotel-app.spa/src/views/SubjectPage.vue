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
                    <v-card width="98%">
                      <v-app-bar dense dark color="blue darken-1">
                        <v-toolbar-title class="subtitle-1">Gost predavač</v-toolbar-title>
                        <div class="flex-grow-1"></div>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col>
                            <v-list dense class="my-n5">
                              <v-list-item>
                                <v-list-item-content>
                                  <v-list-item-title>Tin Kramberger</v-list-item-title>
                                  <v-list-item-subtitle>16/09/2019</v-list-item-subtitle>
                                  <p class="mt-2">
                                    U srijedu 05.06. nam kao gost predavač dolazi Vladimir Manzin koji je freelance istraživač, a glavno područje istraživanja mu je umjetna inteligencija i neuronske mreže.
                                    Održati će nam predavanje na temu umjetne inteligencije s naglaskom na posljednje aktualnosti u tom području.
                                    Dolazak na predavanje je obvezan, te će se popisivati studenti.
                                    Nitko od vas nije bez grijeha, sve što radite je naprotiv Bogu.
                                  </p>
                                </v-list-item-content>
                                <v-list-item-avatar size="60">
                                  <v-img src="../assets/TVZ/djelatnici/tin.png"></v-img>
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-list>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                    <v-card width="98%" class="mt-5">
                      <v-app-bar dense dark color="red">
                        <v-toolbar-title class="subtitle-1">Uvodna riječ</v-toolbar-title>
                        <div class="flex-grow-1"></div>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col>
                            <v-list dense class="my-n5">
                              <v-list-item>
                                <v-list-item-content>
                                  <v-list-item-title>Matija Novosel</v-list-item-title>
                                  <v-list-item-subtitle>21/09/2019</v-list-item-subtitle>
                                  <p class="mt-2">
                                    U srednjoj sam počeo trčati jer mi je išlo na živce kako glupi trener iz tjelesnog kaže "1 krug zagrijavanja, pa onda 5 krugova (200 m)), a ja realno mogu istrčati 1,5 krugova". I zbog pičaka. Long story short, nabrijo sam se na nogomet i sportove i teretanu i one debilne motivacijske spike, i "grajndao" sam ko crnac neki i smršavio 30+ kg u jednoj godini. To me katapultiralo iz F ranga u B rang kod pičaka, pa su me počele oblijetavati i štipati za guzu (omg, sexual assault, zovite centar za prevenciju silovanja. xD). I tako sam se malo zafrkavao s njima, ali nikad nisam niti jednu poljubio jer sam u duši još bio beta, i nikad nisam neku pofukao jer sam uvijek pričao o igricama i glupostima. Jedna je bila jako zainteresirana i vjerojatno bi dala i bili smo na par "dejtova", i totalno mi se sviđala, ali sva sreća da sam uzeo korak unatrag i sagledao situaciju objektivno - da nemamo ništa zajedničkog, da je objektivno ružna iznutra i izvana, i da je jedini razlog zbog kojeg mi se sviđa to što moje tjelo proizvodi hormone koji služe da me izmanipuliraju da napravim loš potez. Tog dana sam postao vocel/incel i ne želim imati posla sa djevojkama. Tim više nakon one epidemije lažnih prijava silovanja i američkih feministkinja. Svu izgubljenu težinu sam brzo dobio natrag jer sam pao u depresiju jer me lik iz biologije jebo i nije mi htio 2 dati pa je bilo biti ili ne biti, i počeo sam igrati igrice po 12 sati na dan i uopće ne izlaziti iz sobe.
                                    Sad dajem savjete ljudima na internetu u slobodno vrijeme.
                                  </p>
                                </v-list-item-content>
                                <v-list-item-avatar size="60">
                                  <v-img src="../assets/matija.png"></v-img>
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-list>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
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
                          <v-list-item-title>Ime predmeta</v-list-item-title>
                          <p class="mt-2 font-weight-light"> Baze podataka </p>
                        </v-list-item-content>
                      </v-list-item>
                      <v-list-item>
                        <v-list-item-content>
                          <v-list-item-title>Show your status</v-list-item-title>
                          <v-list-item-subtitle>Your status is visible to everyone</v-list-item-subtitle>
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
                        <v-toolbar-title class="subtitle-1">Predavanja</v-toolbar-title>
                        <div class="flex-grow-1"></div>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">Tin Kramberger</v-list-item-title>
                                  <v-list-item-subtitle>struč. spec. tehn. inf. ing.</v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                  <v-img src="../assets/TVZ/djelatnici/tin.png"></v-img>
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                    <v-card width="98%" class="mt-3">
                      <v-app-bar dense dark color="blue darken-1">
                        <v-toolbar-title class="subtitle-1">Auditorne vježbe</v-toolbar-title>
                        <div class="flex-grow-1"></div>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">Tin Kramberger</v-list-item-title>
                                  <v-list-item-subtitle>struč. spec. tehn. inf. ing.</v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                  <v-img src="../assets/TVZ/djelatnici/tin.png"></v-img>
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                    <v-card width="98%" class="mt-3">
                      <v-app-bar dense dark color="blue darken-1">
                        <v-toolbar-title class="subtitle-1">Laboratorijske vježbe</v-toolbar-title>
                        <div class="flex-grow-1"></div>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">Tin Kramberger</v-list-item-title>
                                  <v-list-item-subtitle>struč. spec. tehn. inf. ing.</v-list-item-subtitle>
                                </v-list-item-content>
                                <v-list-item-avatar tile size="80" color="grey">
                                  <v-img src="../assets/TVZ/djelatnici/tin.png"></v-img>
                                </v-list-item-avatar>
                              </v-list-item>
                            </v-card>
                          </v-col>
                          <v-col cols="4">
                            <v-card outlined>
                              <v-list-item three-line>
                                <v-list-item-content>
                                  <v-list-item-title class="headline mb-1">Renata Kovačević</v-list-item-title>
                                  <v-list-item-subtitle>struč. spec. tehn. inf. ing.</v-list-item-subtitle>
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
                                  <v-list-item-title class="headline mb-1">Briggita Cafuta</v-list-item-title>
                                  <v-list-item-subtitle>asistent </v-list-item-subtitle>
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
                      :loading="studentLoading" 
                      :items="studenti" 
                      class="elevation-1">
                      <template v-slot:top>
                        <v-col cols="9">
                          <v-text-field
                            v-model.lazy="studentSearch"
                            class="mt-2 mb-n7 pa-3"
                            outlined
                            label="Search"
                            append-outer-icon="mdi-magnify"
                          ></v-text-field>
                        </v-col>
                      </template>
                      <template v-slot:item.picture="{ item }">
                        <!--<v-avatar size="40" class="my-1" @click="showInfo(item)">
                          <v-img :src="require(`../assets/TVZ/studenti/${item.JMBAG}.jpg`)" />
                        </v-avatar>-->
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
                        <v-icon :color="item.color">{{ item.icon }}</v-icon>
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
      studentLoading: true,
      totalStudents: 0,
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
        { text: 'JMBAG', value: 'JMBAG', sortable: false },
        { text: 'Ime', value: 'Ime', sortable: false },
        { text: 'Prezime', value: 'Prezime', sortable: false },
        { text: 'Email', value: 'Email', sortable: false }
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
      studenti: null,
      originalStudenti: null,
      selectedItem: { 
        Ime: null,
        JMBAG: null
      },
      loading: true,
      subject: {}
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
      /*

        ects: 5
        id: 147
        isvu: "25066/185288"
        naziv: "3D modeliranje"
        smjer: 1
        url: "185288"

      */
      KolegijService.getKolegij(id).then(({ data }) => {
        this.subject = data;
      });
    }
  }
};

</script>