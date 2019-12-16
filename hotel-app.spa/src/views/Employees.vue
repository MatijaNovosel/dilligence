<template>
  <div>
    <!--<v-parallax src="../assets/banner.jpg" class="mt-n5" height="300" />-->
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-10" justify="center">
          <h1 class="display-1"> Employees </h1>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters class="mt-5" justify="center">
      <template v-for="item in employees">
        <v-card :key="item.ime + item.prezime + item.id" max-width="225" class="ma-3">
          <v-avatar size="135" class="ml-12">
            <v-img src="../assets/TVZ/djelatnici/kova.png"> </v-img>
          </v-avatar>
          <v-row justify="center" class="my-3">
            <h4 class="subtitle-1 font-weight-light">Željko Kovačević</h4>
            <h4 class="caption font-weight-light">struč.spec.ing.techn.inf.</h4>
          </v-row>
          <v-divider></v-divider>
          <v-list two-line>
            <v-list-item class="mb-n6 mt-n3">
              <v-list-item-icon>
                <v-icon color="indigo" class="pt-3">mdi-phone</v-icon>
              </v-list-item-icon>
              <v-list-item-content class="ml-n3">
                <v-list-item-title>099/6542365</v-list-item-title>
                <v-list-item-subtitle>Mobile</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
            <v-list-item class="my-n4">
              <v-list-item-icon>
                <v-icon color="indigo" class="pt-3">mdi-email</v-icon>
              </v-list-item-icon>
              <v-list-item-content class="ml-n3">
                <v-list-item-title>zkovacevic@tvz.hr</v-list-item-title>
                <v-list-item-subtitle>Email</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-card>
      </template>
    </v-row>
  </div>
</template>

<script>

import KolegijService from '../services/api/zaposlenik';

export default { 
  data() {
    return {
      skip: 0,
      take: 25,
      searchData: {
        name: null,
        surname: null,
        vrstaZaposljenja: null,
        odjel: null
      },
      employees: [],
      totalEmployees: null,
      loading: true,
      headers: [
        { text: 'Titula ispred', value: 'titulaIspred', sortable: false },
        { text: 'Ime', value: 'ime', sortable: false },
        { text: 'Prezime', value: 'prezime', sortable: false },
        { text: 'Titula iza', value: 'titulaIza', sortable: false },
        { text: 'Email', value: 'email', sortable: false },
        { text: 'Vrsta zaposljenja', value: 'vrstaZaposljenja', sortable: false },
        { text: 'Odjel', value: 'odjel', sortable: false },
      ]
    }
  },
  created() {
    this.getData();
  },
  methods: {
    getData() {
      this.loading = true;
      KolegijService.getZaposlenici(this.searchData.name, this.searchData.surname, this.searchData.odjel, this.searchData.vrstaZaposljenja, this.skip, this.take).then(({ data }) => {
        this.employees = data.results;
        this.totalEmployees = data.total;
      }).finally(() => {
        this.loading = false;
      });
    }
  }
};

</script>