<template>
  <div>
    <v-row no-gutters>
      <v-col>
        <v-row class="mt-10" justify="center">
          <h1 class="display-1"> Zaposlenici </h1>
        </v-row>
      </v-col>
    </v-row>
    <v-row no-gutters class="mt-5" justify="center">
      <template v-for="item in employees">
        <v-card :key="item.ime + item.prezime + item.id" width="225" class="ma-3">
          <v-avatar size="135" class="ml-11">
            <v-img v-if="item.ime + item.prezime == 'Željko Kovačević'" src="../assets/TVZ/djelatnici/kova.png" />
            <v-img v-else src="../assets/default-user.jpg" />
          </v-avatar>
          <v-btn icon class="gore-desno">
            <v-icon>mdi-dots-vertical</v-icon>
          </v-btn>
          <v-row justify="center" class="mt-3">
            <h4 class="caption font-weight-light"> {{ item.titulaIspred || '-' }} </h4>
          </v-row>
          <v-row justify="center">
            <h4 class="subtitle-1 font-weight-light"> {{ `${item.ime} ${item.prezime}` }} </h4>
          </v-row>
          <v-row justify="center" class="mb-3">
            <h4 class="caption font-weight-light"> {{ item.titulaIza || '-' }} </h4>
          </v-row>
          <v-divider></v-divider>
          <v-list two-line>
            <v-list-item class="my-n4">
              <v-list-item-icon>
                <v-icon color="indigo" class="pt-3">mdi-email</v-icon>
              </v-list-item-icon>
              <v-list-item-content class="ml-n3">
                <v-list-item-title> {{ item.email }}</v-list-item-title>
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

<style scoped>
  .gore-desno {
    position: absolute;
    right: 5px;
    top: 5px;
  }
</style>