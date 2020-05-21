<template>
  <q-page class="q-pa-md">
    <div class="row">
      <div class="col-12">
        <span class="text-weight-light text-h5">{{ $i18n.t('availableCourses') }}</span>
      </div>
      <div class="col-12 q-py-sm">
        <q-separator />
      </div>
      <div class="col-12 q-pb-md">
        <div class="row justify-start q-col-gutter-sm">
          <div class="col-xs-12 col-md-8">
            <q-input
              @input="inputChanged"
              outlined
              v-model="searchData.name"
              dense
              :label="$i18n.t('name')"
              clearable
            />
          </div>
          <div class="col-xs-12 col-md-4">
            <q-select
              @input="inputChanged"
              outlined
              dense
              v-model="searchData.smjer"
              :options="smjerOptions"
              :label="$i18n.t('specialization')"
              multiple
              emit-value
              map-options
            >
              <template v-slot:option="scope">
                <q-item dense v-bind="scope.itemProps" v-on="scope.itemEvents">
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
            <q-option-group
              @input="inputChanged"
              size="sm"
              v-model="searchData.showSubscriptions"
              :options="showSubscriptionsOptions"
              type="checkbox"
              color="primary"
              inline
            />
          </div>
        </div>
      </div>
      <div class="col-12 q-py-sm">
        <q-separator />
      </div>
      <div class="col-12 q-mt-sm">
        <q-table
          :pagination.sync="pagination"
          :rows-per-page-options="rowsPerPageOptions"
          :visible-columns="['specializationId']"
          grid
          :loading="loading"
          :data="subjects"
          :columns="columns"
          row-key="name"
          :no-data-label="$i18n.t('noData')"
          :loading-label="$i18n.t('loading')"
          hide-header
        >
          <template v-slot:item="props">
            <div class="q-pa-xs col-xs-12 col-sm-6 col-md-4 col-lg-3 grid-style-transition">
              <q-card flat bordered>
                <q-card-section class="q-py-md">
                  <span class="ellipsis-text">{{ props.row.name }}</span>
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
                        >{{ col.name != 'specializationId' ? col.value : col.value | smjerFilter }}</q-item-label>
                      </q-item-section>
                    </q-item>
                  </q-list>
                </q-card-section>
                <q-separator />
                <div>
                  <q-btn-group unelevated spread>
                    <q-btn
                      v-if="props.row.subscribed"
                      size="sm"
                      icon="mdi-eye"
                      class="q-py-xs"
                      :class="$q.dark.isActive ? 'bg-grey-8' : 'bg-grey-2'"
                      @click="$router.push({ name: 'course-details-home', params: { id: props.row.id } })"
                    >
                      <q-tooltip
                        anchor="top middle"
                        self="bottom middle"
                        :offset="[10, 10]"
                      >{{ $i18n.t('viewCourseDetails') }}</q-tooltip>
                    </q-btn>
                    <q-btn
                      size="sm"
                      @click="!props.row.subscribed ? openSubscribeDialog(props.row.id) : unsubscribe(props.row.id)"
                      :icon="props.row.subscribed ? 'mdi-close-circle' : 'mdi-door-open'"
                      class="q-py-xs"
                      :class="$q.dark.isActive ? 'bg-grey-9' : 'bg-grey-3'"
                    >
                      <q-tooltip
                        anchor="top middle"
                        self="bottom middle"
                        :offset="[10, 10]"
                      >{{ props.row.subscribed ? $i18n.t('unsubscribeFromCourse') : $i18n.t('subscribeToCourse') }}</q-tooltip>
                    </q-btn>
                  </q-btn-group>
                </div>
              </q-card>
            </div>
          </template>
          <template v-slot:no-data="{ message }">
            <div
              :class="loading ? 'bg-green-8' : 'bg-red-8'"
              class="rounded text-white full-width row flex-center q-py-sm"
            >
              <q-icon v-if="!loading" size="2.5em" name="mdi-database-remove" />
              <q-spinner-hourglass size="2.5em" v-else />
              <span class="text-h6 q-pl-sm">{{ message }}</span>
            </div>
          </template>
        </q-table>
      </div>
    </div>
    <q-dialog v-model="subscribeDialog" persistent>
      <q-card style="width: 50vw;">
        <q-toolbar class="bg-primary text-white dialog-toolbar">
          <span>{{ $i18n.t('subscribeToCourse') }}</span>
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
          <q-input
            dense
            outlined
            no-error-icon
            v-model="password"
            :label="$i18n.t('enterPassword')"
            @input="$v.password.$touch"
            :error="$v.password.$invalid && $v.password.$dirty"
          >
            <template v-slot:append>
              <q-btn
                :ripple="false"
                dense
                size="sm"
                color="primary"
                class="q-mr-md"
                @click="subscribe"
                :disabled="$v.password.$invalid"
              >{{ $i18n.t('confirm') }}</q-btn>
            </template>
          </q-input>
        </q-card-section>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script>
import CourseService from "../../services/api/course";
import UserService from "../../services/api/user";
import NotificationService from "../../services/notification/notifications";
import SMJER from "../../constants/smjer";
import UserMixin from "../../mixins/userMixin";
import { debounce } from "debounce";
import { required, minLength } from "vuelidate/lib/validators";
import saveState from "vue-save-state";

export default {
  name: "Subjects",
  mixins: [UserMixin, saveState],
  methods: {
    getSaveStateConfig() {
      return {
        cacheKey: "course-list",
        saveProperties: ["searchData"]
      };
    },
    subscribe() {
      UserService.subscribe(this.password, this.user.id, this.activeSubjectId)
        .then(() => {
          this.getData();
          this.resetSubscribeDialog();
        })
        .catch(error => {
          NotificationService.showError("Invalid password!");
        });
    },
    unsubscribe(subjectId) {
      UserService.unsubscribe(this.user.id, subjectId).then(() => {
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
      this.$v.$reset();
    },
    optionsUpdated(options) {
      this.getData();
    },
    getData() {
      this.loading = true;
      CourseService.get(
        this.user.id,
        this.searchData.smjer,
        this.searchData.name,
        this.searchData.showSubscriptions.includes(0),
        this.searchData.showSubscriptions.includes(1)
      )
        .then(({ data }) => {
          this.subjects = data.results;
        })
        .finally(() => {
          this.loading = false;
        });
    },
    inputChanged: debounce(function() {
      this.getData();
    }, 1000)
  },
  validations: {
    password: {
      required,
      minLength: minLength(4)
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
      showSubscriptionsOptions: [
        { label: "Show subscribed", value: 0 },
        { label: "Show non subscribed", value: 1 }
      ],
      searchData: {
        name: null,
        smjer: [],
        showSubscriptions: [0]
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
          name: "name",
          label: this.$i18n.t("name"),
          align: "center",
          field: "name"
        },
        {
          name: "specializationId",
          align: "center",
          label: this.$i18n.t("specialization"),
          field: "specializationId"
        },
        {
          name: "subscribed",
          align: "center",
          label: this.$i18n.t("subscribed"),
          field: "subscribed"
        }
      ],
      subjects: [],
      pagination: {
        page: 1,
        rowsPerPage: 20
      }
    };
  },
  watch: {
    "searchData.showSubscriptions": {
      immediate: false,
      handler(newVal, oldVal) {
        if (newVal.length == 0) {
          this.searchData.showSubscriptions = oldVal;
        }
      }
    }
  }
};
</script>

<style lang="sass" scoped>
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
.rounded
  border-radius: 10px
</style>
