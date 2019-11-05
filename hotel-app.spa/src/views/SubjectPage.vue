<template>
  <div>
    <v-dialog v-model="dialog" max-width="400">
      <v-card class="mx-auto">
        <v-img class="white--text" :src="selectedItem.JMBAG !== null ? require(`../assets/TVZ/studenti/${selectedItem.JMBAG}.jpg`) : '' ">
          <v-card-title class="align-end fill-height display-1">{{ selectedItem.Naziv }}</v-card-title>
        </v-img>
      </v-card>
    </v-dialog>
    <v-row no-gutters>
      <v-col cols="9">
        <v-row class="mt-2 mx-2" justify="center">
          <h1 class="display-1 mb-4"> <v-chip color="primary">Baze podataka</v-chip> </h1>
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
              <v-tab>
                Arhiva
              </v-tab>
              <v-tab>
                Raspored
              </v-tab>
              <v-tab>
                Rezervacija labosa
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
                        <v-avatar size="40" class="my-1" @click="showInfo(item)">
                          <v-img :src="require(`../assets/TVZ/studenti/${item.JMBAG}.jpg`)" />
                        </v-avatar>
                      </template>
                    </v-data-table>
                  </v-col>
                </v-row>
              </v-tab-item>
              <!-- Arhiva  -->
              <v-tab-item class="elevation-1">
                <v-row class="ml-3">
                  <v-col>
                    <v-card width="98%">
                      <v-app-bar dense dark color="blue darken-1">
                        <v-toolbar-title class="subtitle-1">Stara vijest</v-toolbar-title>
                        <div class="flex-grow-1"></div>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col>
                            <v-list dense class="my-n5">
                              <v-list-item>
                                <v-list-item-content>
                                  <v-list-item-title>Tin Kramberger</v-list-item-title>
                                  <v-list-item-subtitle>02/09/2019</v-list-item-subtitle>
                                  <p class="mt-2">
                                    Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus a elit eu massa convallis sagittis elementum eget purus. Quisque eros nisi, feugiat sed vehicula id, placerat et leo. Duis tempus a neque nec condimentum. Quisque non mauris nec orci fringilla ultrices in auctor felis. Aliquam felis dui, maximus a placerat eget, egestas eu sapien. Ut ac ligula turpis. Fusce eros felis, varius quis urna sed, iaculis tincidunt nunc. Vivamus quis feugiat arcu, vitae tincidunt justo. Nulla bibendum rhoncus ex sollicitudin dictum. Vivamus in accumsan justo. Duis eu nunc vel dui aliquam pharetra. Vivamus sed aliquam arcu, a placerat nibh. Quisque nisl felis, feugiat at congue et, lacinia sed augue. Mauris a accumsan eros, sed dignissim dui.
                                    Suspendisse sed condimentum ligula, quis sollicitudin ex. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Sed lacinia velit ac sapien consectetur finibus. Pellentesque vel mi eu augue scelerisque vestibulum. Vestibulum hendrerit ullamcorper orci, a congue ante. Duis rhoncus, turpis et pellentesque tristique, ante ligula faucibus nisi, ut porttitor ipsum tortor id arcu. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin tempus erat et feugiat facilisis. Vestibulum elementum lobortis purus ac condimentum.
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
                  </v-col>
                </v-row>
              </v-tab-item>
              <!-- Raspored -->
              <v-tab-item class="elevation-1">
                <v-row class="ml-3">
                  <v-col>
                  </v-col>
                </v-row>
              </v-tab-item>
              <!-- Rezervacija labosa -->
              <v-tab-item class="elevation-1">
                <v-row class="ml-3">
                  <v-col>
                    <v-card width="98%">
                      <v-app-bar dense dark color="blue darken-2">
                        <v-toolbar-title class="subtitle-1">
                          <v-icon class="mt-n1 mr-3">mdi-clock-alert-outline</v-icon>
                          [Ponedjeljak] 7:00 - 8:00
                        </v-toolbar-title>
                        <div class="flex-grow-1"></div>
                        <v-menu>
                          <template v-slot:activator="{ on }">
                            <v-btn class="info" v-on="on">
                              Join
                            </v-btn>
                          </template>
                        </v-menu>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col>
                            <v-list dense class="my-n5">
                              <v-list-item>
                                <v-list-item-content>
                                  <v-data-table :headers="headers" :items="studenti" class="elevation-1">
                                    <template v-slot:item.picture="{ item }">
                                      <v-avatar size="40">
                                        <v-img :src="require(`../assets/TVZ/studenti/${item.JMBAG}.jpg`)"></v-img>
                                      </v-avatar>
                                    </template>
                                    <template v-slot:no-data>
                                      <v-btn color="primary" @click="initialize">Reset</v-btn>
                                    </template>
                                  </v-data-table>
                                </v-list-item-content>
                              </v-list-item>
                            </v-list>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                    <v-card class="mt-5" width="98%">
                      <v-app-bar dense dark color="blue darken-2">
                        <v-toolbar-title class="subtitle-1">
                          <v-icon class="mt-n1 mr-3">mdi-clock-alert-outline</v-icon>
                          [Srijeda] 14:00 - 15:00
                        </v-toolbar-title>
                        <div class="flex-grow-1"></div>
                        <v-menu>
                          <template v-slot:activator="{ on }">
                            <v-btn class="info" v-on="on">
                              Join
                            </v-btn>
                          </template>
                        </v-menu>
                      </v-app-bar>
                      <v-container fluid>
                        <v-row>
                          <v-col>
                            <v-list dense class="my-n5">
                              <v-list-item>
                                <v-list-item-content>
                                  <v-data-table :headers="headers" :items="studenti" class="elevation-1">
                                    <template v-slot:item.picture="{ item }">
                                      <v-avatar size="40">
                                        <v-img :src="require(`../assets/TVZ/studenti/${item.JMBAG}.jpg`)"></v-img>
                                      </v-avatar>
                                    </template>
                                    <template v-slot:no-data>
                                      <v-btn color="primary" @click="initialize">Reset</v-btn>
                                    </template>
                                  </v-data-table>
                                </v-list-item-content>
                              </v-list-item>
                            </v-list>
                          </v-col>
                        </v-row>
                      </v-container>
                    </v-card>
                  </v-col>
                </v-row>
              </v-tab-item>
            </v-tabs-items>
          </v-col>
        </v-row>
      </v-col>
      <v-col cols="3">
        <v-row class="mt-4" justify="center">
          <v-card class="mx-auto" width="85%">
            <v-app-bar dense dark color="pink elevation-2">
              <v-toolbar-title class="subtitle-1">Pravila kolegija</v-toolbar-title>
              <div class="flex-grow-1"></div>
            </v-app-bar>
            <v-container fluid>
              <v-row>
                <v-col>
                  <v-list dense class="my-n5">
                    <v-list-item v-for="item in pravila" :key="item.title" @click="clicky()">
                      <v-list-item-icon class="mt-3">
                        <v-icon color="red">{{ item.icon }}</v-icon>
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
          <v-card class="mx-auto mt-5" width="85%">
            <v-app-bar dense dark color="blue darken-2 elevation-2">
              <v-toolbar-title class="subtitle-1">Knjiga iz kolegija</v-toolbar-title>
              <div class="flex-grow-1"></div>
            </v-app-bar>
            <v-container fluid>
              <v-row>
                <v-col>
                  <v-list dense class="my-n5">
                    <v-list-item v-for="item in knjige" :key="item.title" @click="clicky()">
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
          <v-card class="mx-auto mt-5" width="85%">
            <v-app-bar dense dark color="amber darken-2 elevation-2">
              <v-toolbar-title class="subtitle-1">Neobavezne pripreme</v-toolbar-title>
              <div class="flex-grow-1"></div>
            </v-app-bar>
            <v-container fluid>
              <v-row>
                <v-col>
                  <v-list dense class="my-n5">
                    <v-list-item v-for="item in pripreme" :key="item.title" @click="clicky()">
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
import axios from 'axios';

export default { 
  data() {
    return {
      tabs: null,
      studentLoading: true,
      totalStudents: 0,
      pravila: [
        { title: 'Pravila kolegija', date: "25/09/2019", icon: "mdi-file-pdf-box", color: "red" },
      ],
      knjige: [
        { title: 'Knjiga - pdf', date: "25/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: 'Knjiga - epub', date: "25/09/2019", icon: "mdi-link-variant", color: "grey" },
        { title: 'Knjiga - mobi', date: "25/09/2019", icon: "mdi-link-variant", color: "grey" },
      ],
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
      pripreme: [
        { title: '01 - Laboratorijska vježba', date: "22/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '01 - Laboratorijska vježba - Rješenja', date: "21/09/2019", icon: "mdi-zip-box", color: "purple" },
        { title: '02 - Laboratorijska vježba', date: "22/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '02 - Laboratorijska vježba - Rješenja', date: "21/09/2019", icon: "mdi-zip-box", color: "purple" },
        { title: '03 - Laboratorijska vježba', date: "22/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '03 - Laboratorijska vježba - Rješenja', date: "21/09/2019", icon: "mdi-zip-box", color: "purple" },
        { title: '04 - Laboratorijska vježba', date: "22/09/2019", icon: "mdi-file-pdf-box", color: "red" },
        { title: '04 - Laboratorijska vježba - Rješenja', date: "21/09/2019", icon: "mdi-zip-box", color: "purple" },
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
      subjects: [],
      loading: true,
      studentSearch: "",
      placeholder: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer elit nulla, fermentum rutrum mi suscipit, tempus ornare est. Quisque rutrum convallis metus, a lobortis ipsum placerat quis. Proin elementum lectus eu ex elementum, in viverra eros semper. Nam dignissim augue justo, nec tincidunt risus viverra ut. Proin quis lacinia justo. Nullam volutpat, urna vitae pharetra mollis, leo velit feugiat sapien, quis elementum sem enim at nisl. Proin in ultricies nulla.Cras et pulvinar eros, quis pretium elit. Donec non ligula nec libero luctus suscipit malesuada vitae purus. Duis facilisis egestas ligula ac tincidunt. Fusce ex urna, venenatis non justo et, congue tempus nisi. In rutrum at leo sit amet hendrerit. Ut viverra ultrices maximus. Vestibulum quis gravida justo, eu fringilla dolor. Fusce a diam in eros porta volutpat et eget leo. Phasellus ante erat, venenatis sit amet tempus eu, finibus vel neque. Vestibulum est nibh, molestie eu venenatis in, aliquam sit amet nunc. Suspendisse potenti. Aliquam iaculis eleifend malesuada. Vestibulum magna diam, interdum sed condimentum eu, maximus ac ante. Aenean tempor non risus nec egestas. Nam ante diam, tristique quis mauris non, lobortis semper tortor.",
    }
  },
  created () {
    this.getData();
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
      this.studentLoading = true;
      axios.get("http://localhost/tvz2/api/Kolegij/" + id).then(({ data }) => {
        this.studenti = data.Studenti;
        this.studenti.sort((a, b) => a.Prezime.localeCompare(b.Prezime));
        this.originalStudenti = this.studenti;
        this.totalStudents = this.studenti.length;
      }).finally(() => {
        this.studentLoading = false;
      });
    },
    clicky: function() {
      console.log("Click!");
    }
  },
  watch: {
    studentSearch: function (value) {
      this.studenti = this.originalStudenti;
      if(!value) {
        this.studenti = this.originalStudenti;
        return;
      }
      setTimeout(() => {
        this.studenti = this.studenti.filter(x => {
          if(x.Ime.includes(value)) {
            return x;
          }
        });
      }, 300);
    }
  }
};

</script>