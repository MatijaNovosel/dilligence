<template>
  <q-page class="q-pa-md">
    <div class="row full-width">
      <div class="col-12">
        <span class="text-weight-light text-h5">{{ $t('availableCourses') }}</span>
      </div>
      <div class="col-12 q-py-sm">
        <q-separator />
      </div>
      <div class="col-12 q-pb-md">
        <div class="row justify-start">
          <div class="col-8">
            <q-input outlined v-model="searchData.name" dense :label="$t('name')" clearable />
          </div>
          <div class="col-4 q-pl-sm">
            <q-select
              outlined
              dense
              v-model="searchData.smjer"
              :options="smjerOptions"
              :label="$t('specialization')"
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
          <div class="col-12 q-pt-sm">
            <q-checkbox
              size="xs"
              v-model="searchData.showSubscribed"
              val="xs"
              :label="$t('showSubscribed')"
            />
            <q-checkbox
              size="xs"
              v-model="searchData.showNonSubscribed"
              val="xs"
              :label="$t('showNonSubscribed')"
            />
          </div>
        </div>
      </div>
      <div class="col-12 text-right q-py-sm">
        <q-btn dense class="q-px-sm" color="primary" @click="getData">{{ $t('search') }}</q-btn>
      </div>
      <div class="col-12 q-mt-sm">
        <q-table
          :pagination.sync="pagination"
          :rows-per-page-options="rowsPerPageOptions"
          :visible-columns="['smjerId']"
          grid
          :loading="loading"
          :data="subjects"
          :columns="columns"
          row-key="naziv"
          hide-header
        >
          <template v-slot:item="props">
            <div class="q-pa-xs col-xs-12 col-sm-6 col-md-4 col-lg-3 grid-style-transition">
              <q-card flat bordered>
                <q-card-section class="q-py-md">
                  <span class="ellipsis-text">{{ props.row.naziv }}</span>
                  <q-icon
                    dense
                    size="20px"
                    class="aside"
                    :name="props.row.subscribed ? 'mdi-lock-open-variant' : 'mdi-lock-question'"
                    :color="props.row.subscribed ? 'green-5' : 'red-4'"
                  />
                </q-card-section>
                <q-separator />
                <q-card-section class="q-py-none">
                  <q-list dense>
                    <q-item class="q-my-sm" v-for="col in props.cols" :key="col.name">
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
                    v-if="props.row.subscribed"
                    flat
                    size="sm"
                    class="bg-red-4 text-white"
                    @click="unsubscribe(props.row.id)"
                  >{{ $t('unsubscribe') }}</q-btn>
                  <q-btn
                    v-else
                    flat
                    size="sm"
                    class="bg-primary text-white"
                    @click="openSubscribeDialog(props.row.id)"
                  >{{ $t('subscribe') }}</q-btn>
                  <q-space />
                </q-card-actions>
              </q-card>
            </div>
          </template>
        </q-table>
      </div>
    </div>
    <q-dialog v-model="subscribeDialog" persistent no-esc-dismiss>
      <q-card style="width: 50vw;">
        <q-toolbar class="bg-primary text-white dialog-toolbar">
          <q-space />
          <q-btn
            :ripple="false"
            dense
            size="sm"
            color="white"
            flat
            round
            icon="mdi-close-circle"
            @click="resetSubscribeDialog"
          />
        </q-toolbar>
        <q-card-section>
          <q-input type="password" dense outlined v-model="password" :label="$t('enterPassword')">
            <template v-slot:append>
              <q-btn
                :ripple="false"
                dense
                size="sm"
                color="primary"
                @click="subscribe"
              >{{ $t('confirm') }}</q-btn>
            </template>
          </q-input>
        </q-card-section>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
import SubjectService from "../services/api/kolegij";
import KorisnikService from "../services/api/korisnik";
import SMJER from "../constants/smjer";
import UserMixin from "../mixins/userMixin";

export default {
  name: "Subjects",
  mixins: [UserMixin],
  methods: {
    subscribe() {
      KorisnikService.subscribe(
        this.password,
        this.user.id,
        this.activeSubjectId
      )
        .then(() => {
          this.getData();
          this.resetSubscribeDialog();
        })
        .catch(error => {
          this.$q.notify({
            type: "negative",
            message: this.$t("error.incorrectPassword")
          });
        });
    },
    unsubscribe(subjectId) {
      KorisnikService.unsubscribe(this.user.id, subjectId).then(() => {
        this.getData();
      });
    },
    openSubscribeDialog(subjectId) {
      this.subscribeDialog = true;
      this.activeSubjectId = subjectId;
    },
    resetSubscribeDialog() {
      this.subscribeDialog = false;
      this.password = this.activeSubjectId = null;
    },
    optionsUpdated(options) {
      this.getData();
    },
    getData() {
      this.loading = true;
      SubjectService.get(
        this.user.id,
        this.searchData.smjer,
        this.searchData.name,
        this.searchData.showSubscribed,
        this.searchData.showNonSubscribed
      )
        .then(({ data }) => {
          let subjects = data.results;
          KorisnikService.getSubscriptions(this.user.id).then(({ data }) => {
            let subscriptions = data;
            subjects.forEach(
              x => (x.subscribed = subscriptions.includes(x.id))
            );
            this.subjects = subjects;
          });
        })
        .finally(() => {
          this.loading = false;
        });
    }
  },
  created() {
    this.getData();
    for (let val in SMJER) {
      this.smjerOptions.push({ label: val, value: SMJER[val] });
    }
  },
  data() {
    return {
      activeSubjectId: null,
      subscribeDialog: null,
      password: null,
      smjerOptions: [],
      searchData: {
        name: null,
        smjer: [],
        showSubscribed: true,
        showNonSubscribed: false
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
          label: this.$t("name"),
          align: "center",
          field: "naziv"
        },
        {
          name: "smjerId",
          align: "center",
          label: this.$t("specialization"),
          field: "smjerId"
        },
        {
          name: "subscribed",
          align: "center",
          label: this.$t("subscribed"),
          field: "subscribed"
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
.dialog-toolbar
  min-height: 30px
.ellipsis-text
  white-space: nowrap
  overflow: hidden
  display: block
</style>
