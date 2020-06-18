<template>
  <q-page class="q-pa-md">
    <q-btn
      size="sm"
      class="absolute-top-right q-mr-sm bg-green-8 text-white"
      @click="newCourseDialog = true"
    >Create course</q-btn>
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
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="subscribeDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar
          :class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
          class="text-white dialog-toolbar"
        >
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
        <q-card-section class="text-center">
          <q-input
            dense
            outlined
            no-error-icon
            v-model="password"
            :label="$i18n.t('enterPassword')"
            :type="!showPassword ? 'password' : 'text'"
            @input="$v.password.$touch"
            :error="$v.password.$invalid && $v.password.$dirty"
          >
            <template v-slot:append>
              <q-btn
                flat
                round
                size="sm"
                :icon="!showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                @click="showPassword = !showPassword"
              />
            </template>
          </q-input>
          <q-btn
            :ripple="false"
            dense
            size="sm"
            color="primary"
            class="q-px-md"
            @click="subscribe"
            :disabled="$v.password.$invalid"
          >{{ $i18n.t('confirm') }}</q-btn>
        </q-card-section>
      </q-card>
    </q-dialog>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="newCourseDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || newDialogStyle">
        <q-toolbar
          :style="`border-bottom: 1px solid ${$q.dark.isActive ? 'rgba(255, 255, 255, 0.6)' : 'rgba(0, 0, 0, 0.12)'};`"
        >
          <span style="font-size: 18px;">New course</span>
          <q-space />
          <q-btn
            @click="resetNewCourseDialog"
            :ripple="false"
            dense
            size="sm"
            flat
            round
            icon="mdi-close-thick"
          />
        </q-toolbar>
        <q-card-section class="q-gutter-sm q-pb-none">
          <div class="row">
            <div class="col-12">
              <q-input
                :error="$v.newCourse.name.$invalid && $v.newCourse.name.$dirty"
                error-message="This field is required!"
                dense
                outlined
                v-model="newCourse.name"
                label="Title"
                @input="$v.newCourse.name.$touch()"
              />
            </div>
            <div class="col-12">
              <q-input
                :error="$v.newCourse.password.$invalid && $v.newCourse.password.$dirty"
                error-message="This field is required!"
                dense
                outlined
                v-model="newCourse.password"
                label="Password"
                @input="$v.newCourse.password.$touch()"
              />
            </div>
            <div class="col-12">
              <q-select
                outlined
                dense
                v-model="newCourse.specializationId"
                :options="smjerOptions"
                :label="$i18n.t('specialization')"
                emit-value
                map-options
              />
            </div>
          </div>
        </q-card-section>
        <q-card-actions class="justify-end q-pt-md">
          <q-btn
            :disabled="$v.newCourse.$invalid"
            @click="createNewCourse"
            class="q-mr-sm q-mb-sm"
            color="primary"
            size="sm"
          >Create</q-btn>
        </q-card-actions>
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
import { mapActions } from "vuex";

export default {
  name: "Subjects",
  mixins: [UserMixin, saveState],
  methods: {
    ...mapActions(["addNewPrivelege", "removePrivelege"]),
    getSaveStateConfig() {
      return {
        cacheKey: "course-list",
        saveProperties: ["searchData"]
      };
    },
    resetNewCourseDialog() {
      this.newCourseDialog = false;
    },
    createNewCourse() {
      let payload = {
        name: this.newCourse.name,
        password: this.newCourse.password,
        specializationId: this.newCourse.specializationId,
        createdById: this.user.id
      };
      CourseService.createNewCourse(payload)
        .then(({ data }) => {
          let id = data.payload;
          this.addNewPrivelege({
            courseId: id,
            privileges: [
              this.Privileges.CanManageCourse,
              this.Privileges.IsInvolvedWithCourse
            ]
          });
          this.getData();
          this.resetNewCourseDialog();
        })
        .catch(error => {
          NotificationService.showError(error.message);
        });
    },
    subscribe() {
      UserService.subscribe(this.password, this.user.id, this.activeSubjectId)
        .then(() => {
          this.getData();
          this.addNewPrivelege({
            courseId: this.activeSubjectId,
            privileges: [this.Privileges.CanCreateNewDiscussion]
          });
          this.resetSubscribeDialog();
        })
        .catch(error => {
          NotificationService.showError("Invalid password!");
        });
    },
    unsubscribe(subjectId) {
      UserService.unsubscribe(this.user.id, subjectId).then(() => {
        this.getData();
        this.removePrivelege(subjectId);
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
    newCourse: {
      name: {
        required,
        minLength: minLength(4)
      },
      password: {
        required,
        minLength: minLength(4)
      }
    },
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
      newCourseDialog: false,
      dialogStyle: { width: "35%", "max-width": "90vw" },
      showPassword: false,
      activeSubjectId: null,
      subscribeDialog: null,
      password: null,
      smjerOptions: [],
      newCourse: {
        name: null,
        password: null,
        specializationId: null
      },
      newDialogStyle: {
        width: "70%",
        "max-width": "90vw"
      },
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
