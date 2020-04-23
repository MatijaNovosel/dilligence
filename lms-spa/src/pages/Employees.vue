<template>
  <q-page class="q-pa-md">
    <!-- @update:pagination="optionsUpdated" - Stavi dok se popravi paginacija na SERVERU -->
    <q-table
      :pagination.sync="pagination"
      :loading="loading"
      separator="vertical"
      dense
      title="Employees"
      :rows-per-page-options="rowsPerPageOptions"
      :data="employees"
      :columns="columns"
    >
      <template v-slot:top>
        <div class="row full-width">
          <div class="col-12">
            <span class="text-weight-light text-h5">Employees</span>
          </div>
          <div class="col-12 q-py-sm">
            <q-separator />
          </div>
          <div class="col-12 q-pb-md">
            <div class="row q-gutter-sm justify-center">
              <div class="col-5">
                <q-input v-model="searchData.name" dense label="Ime" clearable />
              </div>
              <div class="col-5">
                <q-input v-model="searchData.surname" dense label="Prezime" clearable />
              </div>
              <div class="col-3">
                <div class="q-pa-sm border-box">
                  <q-btn
                    :ripple="false"
                    dense
                    size="md"
                    flat
                    round
                    class="absolute-top-right"
                    icon="mdi-close-circle"
                    color="grey"
                    v-if="searchData.odjel.length != 0"
                    @click="searchData.odjel = []"
                  />
                  <span>Odjel:</span>
                  <q-option-group
                    dense
                    :options="odjelOptions"
                    type="checkbox"
                    v-model="searchData.odjel"
                  />
                </div>
              </div>
              <div class="col-3">
                <div class="q-pa-sm border-box">
                  <q-btn
                    :ripple="false"
                    dense
                    size="md"
                    flat
                    round
                    class="absolute-top-right"
                    icon="mdi-close-circle"
                    color="grey"
                    v-if="searchData.employmentType.length != 0"
                    @click="searchData.employmentType = []"
                  />
                  <span>Vrsta zaposljenja:</span>
                  <q-option-group
                    dense
                    :options="employmentTypeOptions"
                    type="checkbox"
                    v-model="searchData.employmentType"
                  />
                </div>
              </div>
            </div>
          </div>
          <div class="col-12 text-right q-py-sm">
            <q-btn color="primary" @click="getData">Search</q-btn>
          </div>
        </div>
      </template>
      <template v-slot:body-cell-odjelId="props">
        <q-td :props="props">{{ props.value | odjelFilter }}</q-td>
      </template>
      <template v-slot:body-cell-vrstaZaposljenja="props">
        <q-td :props="props">{{ props.value | zaposljenjeFilter }}</q-td>
      </template>
    </q-table>
  </q-page>
</template>

<script>
import ODJEL from "../constants/odjel";
import EMPLOYMENT from "../constants/employment";

export default {
  name: "Employees",
  methods: {
    optionsUpdated(options) {
      this.getData();
    },
    getData() {
      this.loading = true;
    }
  },
  created() {
    this.getData();
    for (let val in ODJEL) {
      this.odjelOptions.push({ label: val, value: ODJEL[val] });
    }
    for (let val in EMPLOYMENT) {
      this.employmentTypeOptions.push({ label: val, value: EMPLOYMENT[val] });
    }
  },
  data() {
    return {
      odjelOptions: [],
      employmentTypeOptions: [],
      searchData: {
        name: null,
        surname: null,
        odjel: [],
        employmentType: []
      },
      rowsPerPageOptions: [10, 15, 20],
      loading: false,
      columns: [
        {
          name: "ime",
          label: "Ime",
          align: "center",
          field: "ime"
        },
        {
          name: "prezime",
          align: "center",
          label: "Prezime",
          field: "prezime"
        },
        {
          name: "email",
          align: "center",
          label: "Email",
          field: "email"
        },
        {
          name: "odjelId",
          align: "center",
          label: "Odjel",
          field: "odjelId"
        },
        {
          name: "titulaIspred",
          align: "center",
          label: "Titula ispred",
          field: "titulaIspred"
        },
        {
          name: "titulaIza",
          align: "center",
          label: "Titula iza",
          field: "titulaIza"
        },
        {
          name: "vrstaZaposljenja",
          align: "center",
          label: "Vrsta zaposljenja",
          field: "vrstaZaposljenjaId"
        }
      ],
      employees: [],
      pagination: {
        page: 1,
        rowsPerPage: 20
      }
    };
  }
};
</script>
