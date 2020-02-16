<template>
  <div>
    <v-row no-gutters>
      <v-switch class="gore-desno-switch" color="primary" label="Table view" v-model="tableView" />
      <v-col>
        <v-row class="mt-10" justify="center">
          <h1 class="display-1"> Zaposlenici </h1>
        </v-row>
      </v-col>
    </v-row>
    <v-row v-if="!tableView" no-gutters class="mt-5" justify="center">
      <template v-for="item in employees">
        <v-skeleton-loader type="card" :loading="employeesLoading" :key="item.ime + item.prezime + item.id">
          <employee-card :employeeData="item" />
        </v-skeleton-loader>
      </template>
    </v-row>
    <v-row v-else no-gutters class="mt-5" justify="center">
      <v-col>
        <v-data-table dense
                      class="elevation-2"
                      :headers="headers"
                      fixed-header
                      :items="employees"
                      :items-per-page="options.itemsPerPage">
        </v-data-table>
      </v-col>
    </v-row>
  </div>
</template>

<script>

import ZaposlenikService from '../services/api/zaposlenik';
import EmployeeCard from '../components/EmployeeCard.vue';

export default { 
  components: {
    EmployeeCard
  },
  data() {
    return {
      skip: 0,
      take: 15,
      tableView: false,
      employeesLoading: false,
      headers: [
        { text: 'Ime', value: 'ime', sortable: false, align: 'center' },
        { text: 'Prezime', value: 'prezime', sortable: false, align: 'center' },
        { text: 'Email', value: 'email', sortable: false, align: 'center' }
      ],
      searchData: {
        name: null,
        surname: null,
        vrstaZaposljenja: null,
        odjel: null
      },
      employees: [],
      totalEmployees: null,
      options: {
        itemsPerPage: 10
      }
    }
  },
  created() {
    this.getData();
  },
  methods: {
    getData() {
      this.employeesLoading = true;
      ZaposlenikService.getZaposlenici(this.searchData.name, this.searchData.surname, 1, this.searchData.vrstaZaposljenja, this.skip, this.take)
      .then(({ data }) => {
        this.employees = data.results;
        this.totalEmployees = data.total;
      }).finally(() => {
        this.employeesLoading = false;
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
  .gore-desno-switch {
    position: absolute;
    right: 15px;
    top: 15px;  
  }
</style>