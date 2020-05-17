<template>
  <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="open" persistent>
    <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
      <q-toolbar class="bg-primary dialog-toolbar">
        <span>Submissions</span>
        <q-space />
        <q-btn
          :ripple="false"
          dense
          size="sm"
          color="white"
          flat
          round
          icon="mdi-close-circle"
          @click="reset"
        />
      </q-toolbar>
      <q-card-section horizontal v-if="submissions">
        <q-card-section style="width: 40%" class="q-pl-none">
          <q-scroll-area
            :thumb-style="thumbStyle"
            :bar-style="barStyle"
            visible
            style="height: 450px"
          >
            <q-list class="q-px-md">
              <q-item clickable v-for="(submission, i) in submissions" :key="i">
                <q-item-section avatar>
                  <q-icon name="mdi-account" />
                </q-item-section>
                <q-item-section>
                  <q-item-label>{{ submission.submittedBy }}</q-item-label>
                  <q-item-label
                    caption
                  >Submitted at: {{ format(new Date(submission.submittedAt), 'yyyy-MM-dd HH:mm') }}</q-item-label>
                </q-item-section>
                <q-item-section side>
                  <span class="text-red-6 text-subtitle2">Ungraded</span>
                </q-item-section>
              </q-item>
            </q-list>
          </q-scroll-area>
        </q-card-section>
        <q-separator vertical />
        <q-card-section style="width: 60%;"></q-card-section>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script>
import UserMixin from "../mixins/userMixin";
import CourseTaskService from "../services/api/course-task";
import { format } from "date-fns";

export default {
  name: "task-view-submissions-dialog",
  mixins: [UserMixin],
  props: ["open", "taskId", "courseId"],
  data() {
    return {
      submissions: null,
      thumbStyle: {
        right: "2px",
        borderRadius: "5px",
        backgroundColor: "#027be3",
        width: "5px",
        opacity: 0.75
      },
      barStyle: {
        right: "0px",
        borderRadius: "9px",
        backgroundColor: "#027be3",
        width: "9px",
        opacity: 0.2
      },
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      }
    };
  },
  methods: {
    format,
    reset() {
      this.$emit("close");
    }
  },
  watch: {
    open: {
      immediate: false,
      deep: true,
      handler(val) {
        if (!val) {
          return;
        }
        CourseTaskService.getTaskAttempts(this.taskId, this.courseId).then(
          ({ data }) => {
            this.submissions = data;
          }
        );
      }
    }
  }
};
</script>
