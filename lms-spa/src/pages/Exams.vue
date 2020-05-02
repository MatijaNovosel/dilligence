<template>
  <q-page class="q-pa-md">
    <div class="row">
      <div class="col-12">
        <span class="text-weight-light text-h5">{{ $i18n.t('availableExams') }}</span>
      </div>
      <div class="col-12 q-py-sm">
        <q-separator />
      </div>
      <div class="col-12">
        <div class="row">
          <template v-if="attempts">
            <div
              v-for="(attempt, i) in attempts"
              :key="i"
              class="q-pa-xs col-xs-12 col-sm-6 col-md-4 col-lg-3"
            >
              <exam-card :value="attempt" />
            </div>
          </template>
        </div>
      </div>
    </div>
  </q-page>
</template>

<script>
import ExamCard from "../components/exam-card";
import ExamService from "../services/api/exam";
import { mapGetters } from "vuex";
import UserMixin from "../mixins/userMixin";

export default {
  name: "Exams",
  components: { "exam-card": ExamCard },
  mixins: [UserMixin],
  methods: {
    getAttempts() {
      ExamService.getAttempts(this.user.id).then(({ data }) => {
        this.attempts = data;
      });
    }
  },
  created() {
    this.getAttempts();
  },
  data() {
    return {
      attempts: null
    };
  }
};
</script>

<style lang="sass" scoped>

</style>
