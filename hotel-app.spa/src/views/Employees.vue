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
        <employee-card :key="item.ime + item.prezime + item.id" :employeeData='item' />
      </template>
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
      searchData: {
        name: null,
        surname: null,
        vrstaZaposljenja: null,
        odjel: null
      },
      employees: [],
      totalEmployees: null,
      loading: true
    }
  },
  created() {
    this.getData();
  },
  methods: {
    getData() {
      this.loading = true;
      ZaposlenikService.getZaposlenici(this.searchData.name, this.searchData.surname, 1, this.searchData.vrstaZaposljenja, this.skip, this.take).then(({ data }) => {
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