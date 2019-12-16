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
    <v-row no-gutters class="mt-5">
      <v-col>
        <v-data-table :loading="loading" 
                      :headers="headers" 
                      :items="employees" 
                      class="elevation-1">
        </v-data-table>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import KolegijService from '../services/api/zaposlenik';

export default { 
  data() {
    return {
      skip: 0,
      take: null,
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
        { text: 'ID', value: 'ID', sortable: false },
        { text: 'Titula ispred', value: 'TitulaIspred', sortable: false },
        { text: 'Ime', value: 'Ime', sortable: false },
        { text: 'Prezime', value: 'Prezime', sortable: false },
        { text: 'Titula iza', value: 'TitulaIza', sortable: false },
        { text: 'Email', value: 'Email', sortable: false },
        { text: 'Vrsta zaposljenja', value: 'VrstaZaposljenja', sortable: false },
        { text: 'Odjel', value: 'Odjel', sortable: false },
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