<template>
  <q-page class="q-pa-md">
    <div class="row" v-if="course">
      <div class="col-12 q-pb-md text-center">
        <span class="text-weight-light text-h5">{{ course.name }}</span>
      </div>
      <div class="col-12">
        <q-card flat bordered>
          <q-tabs
            v-model="tab"
            dense
            class="text-grey"
            active-color="primary"
            indicator-color="primary"
            align="justify"
            narrow-indicator
          >
            <q-tab name="home" :label="$t('home')" />
            <q-tab name="notifications" :label="$t('notifications')" />
            <q-tab name="participants" :label="$t('participants')" />
            <q-tab name="exams" :label="$t('exams')" />
          </q-tabs>
          <q-separator />
          <q-tab-panels v-model="tab">
            <q-tab-panel name="home">
              {{ tab }}
            </q-tab-panel>
            <q-tab-panel name="notifications">
              
            </q-tab-panel>
            <q-tab-panel name="participants">
              {{ tab }}
            </q-tab-panel>
            <q-tab-panel name="exams">
              {{ tab }}
            </q-tab-panel>
          </q-tab-panels>
        </q-card>
      </div>
    </div>
  </q-page>
</template>

<script>
import CourseService from "../services/api/course";

export default {
  name: "CourseDetails",
  created() {
    this.getData();
  },
  methods: {
    getData() {
      CourseService.getCourse(this.$route.params.id).then(({ data }) => {
        this.course = data;
      });
    }
  },
  data() {
    return {
      course: null,
      tab: "home"
    };
  }
};
</script>
