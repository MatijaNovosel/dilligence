<template>
  <q-page class="q-pa-md">
    <div class="row">
      <template v-for="n in 10">
        <div :key="n" class="q-pa-xs col-xs-12 col-sm-6 col-md-4 col-lg-3">
          <ExamCard />
        </div>
      </template>
    </div>
    <router-view></router-view>
  </q-page>
</template>

<script>
import ExamCard from "../components/ExamCard";

export default {
  name: "Exams",
  components: { ExamCard },
  methods: {},
  data() {
    return {};
  }
};
</script>

<style lang="sass" scoped>

</style>
