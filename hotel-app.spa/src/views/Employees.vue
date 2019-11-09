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
        <v-data-table :loading="loading" :headers="headers" :items="employees" class="elevation-1">
        </v-data-table>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import axios from 'axios';

export default { 
  data() {
    return {
      employees: [],
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
  mounted() {
    axios.get("http://localhost/tvz2/api/Zaposlenik/").then(({ data }) => {
      this.employees = data;
    }).finally(() => {
      this.loading = false;
    });
  },
  methods: { }
};

</script>