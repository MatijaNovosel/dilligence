<template>
  <q-page class="q-pa-md">
    <div class="row full-width">
      <div class="col-12">
        <span class="text-weight-light text-h5">Available subjects</span>
      </div>
      <div class="col-12 q-py-sm">
        <q-separator />
      </div>
      <div class="col-12 q-pb-md">
        <div class="row q-gutter-sm justify-start">
          <div class="col-4">
            <q-input outlined v-model="searchData.name" dense label="Name" clearable />
          </div>
          <div class="col-4">
            <q-select
              outlined
              dense
              v-model="searchData.smjer"
              :options="smjerOptions"
              label="Smjer"
              multiple
              emit-value
              map-options
            >
              <template v-slot:option="scope">
                <q-item v-bind="scope.itemProps" v-on="scope.itemEvents">
                  <q-item-section>
                    <q-item-label v-html="scope.opt.label"></q-item-label>
                  </q-item-section>
                  <q-item-section side>
                    <q-toggle v-model="searchData.smjer" :val="scope.opt.value" />
                  </q-item-section>
                </q-item>
              </template>
            </q-select>
          </div>
          <div class="col-12">
            <q-checkbox
              size="sm"
              v-model="searchData.showSubscribed"
              val="xs"
              label="Show subscribed"
            />
            <q-checkbox
              size="sm"
              v-model="searchData.showNonSubscribed"
              val="xs"
              label="Show non subscribed subjects"
            />
          </div>
        </div>
      </div>
      <div class="col-12 text-right q-py-sm">
        <q-btn color="primary" @click="getData">Search</q-btn>
      </div>
      <div class="col-12 q-mt-sm">
        <q-table
          v-if="subscriptions != null"
          :pagination.sync="pagination"
          :rows-per-page-options="rowsPerPageOptions"
          grid
          :data="subjects"
          :columns="columns"
          row-key="naziv"
          hide-header
        >
          <template v-slot:item="props">
            <div class="q-pa-xs col-xs-12 col-sm-6 col-md-4 col-lg-2 grid-style-transition">
              <q-card flat bordered>
                <q-card-section
                  :class="(subscriptions.includes(props.row.id) ? 'bg-green-2' : 'bg-grey-2') + ' q-py-md'"
                >
                  <span class="ellipsis">{{ props.row.naziv }}</span>
                  <q-icon
                    dense
                    size="20px"
                    class="aside"
                    :name="subscriptions.includes(props.row.id) ? 'mdi-lock-open-variant' : 'mdi-lock-question'"
                    :color="subscriptions.includes(props.row.id) ? 'green-5' : 'red-4'"
                  />
                </q-card-section>
                <q-separator />
                <q-card-section class="q-py-none">
                  <q-list dense>
                    <q-item
                      v-for="col in props.cols.filter(col => !['naziv', 'id'].includes(col.name))"
                      :key="col.name"
                    >
                      <q-item-section>
                        <q-item-label>{{ col.label }}</q-item-label>
                      </q-item-section>
                      <q-item-section side>
                        <q-item-label
                          caption
                        >{{ col.name != 'smjerId' ? col.value : col.value | smjerFilter }}</q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-list>
                </q-card-section>
                <q-separator />
                <q-card-actions>
                  <q-space />
                  <q-btn
                    v-if="subscriptions.includes(props.row.id)"
                    flat
                    size="sm"
                    class="bg-red-4 text-white"
                    @click="unsubscribe(props.row.id)"
                  >Unsubscribe</q-btn>
                  <q-btn
                    v-else
                    flat
                    size="sm"
                    class="bg-primary text-white"
                    @click="subscribe(props.row.id)"
                  >Subscribe</q-btn>
                  <q-space />
                </q-card-actions>
              </q-card>
            </div>
          </template>
        </q-table>
      </div>
    </div>
  </q-page>
</template>

<script>
import SubjectService from "../services/api/kolegij";
import StudentService from "../services/api/student";
import SMJER from "../constants/smjer";
import { mapGetters } from "vuex";

export default {
  name: "Subjects",
  methods: {
    subscribe(subjectId) {
      StudentService.subscribe(this.password, this.user.id, subjectId)
        .catch(error => {
          this.$q.notify({
            type: "negative",
            message: "Incorrect password!"
          });
        })
        .then(() => {
          this.getSubscriptions();
          this.getData();
        });
    },
    unsubscribe(subjectId) {
      StudentService.unsubscribe(this.user.id, subjectId).then(() => {
        this.getSubscriptions();
        this.getData();
      });
    },
    optionsUpdated(options) {
      this.getData();
    },
    getSubscriptions() {
      StudentService.getSubscriptions(this.user.id).then(({ data }) => {
        this.subscriptions = data;
      });
    },
    getData() {
      this.loading = true;
      SubjectService.get(this.searchData.smjer, this.searchData.name)
        .then(({ data }) => {
          this.subjects = data.results;
        })
        .finally(() => {
          this.loading = false;
        });
    }
  },
  computed: {
    ...mapGetters(["user"])
  },
  created() {
    this.getData();
    this.getSubscriptions();
    for (let val in SMJER) {
      this.smjerOptions.push({ label: val, value: SMJER[val] });
    }
  },
  data() {
    return {
      password: null,
      smjerOptions: [],
      subscriptions: null,
      searchData: {
        name: null,
        smjer: [],
        showSubscribed: true,
        showNonSubscribed: true
      },
      rowsPerPageOptions: [10, 15, 20],
      loading: false,
      columns: [
        {
          name: "id",
          label: "Id",
          align: "center",
          field: "id"
        },
        {
          name: "naziv",
          label: "Name",
          align: "center",
          field: "naziv"
        },
        {
          name: "smjerId",
          align: "center",
          label: "Smjer",
          field: "smjerId"
        }
      ],
      subjects: [],
      pagination: {
        page: 1,
        rowsPerPage: 20
      }
    };
  }
};
</script>

<style lang="sass" scoped>
.border-box
  position: relative
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 10px
.top-right
  position: absolute
  right: 8px
  top: 8px
.aside
  position: absolute
  right: 15px
  bottom: 15px
</style>
