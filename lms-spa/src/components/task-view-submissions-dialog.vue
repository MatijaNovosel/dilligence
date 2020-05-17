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
      <q-card-section class="q-gutter-sm" v-if="submissions">
        <q-list dense bordered class="rounded-borders">
          <template v-if="submissions.length != 0">
            <q-expansion-item
              :key="i"
              v-for="(submission, i) in submissions"
              expand-icon-toggle
              expand-separator
              icon="mdi-account"
              :label="submission.submittedBy"
              :caption="format(new Date(submission.submittedAt), 'yyyy-MM-dd HH:mm')"
            >
              <q-card>
                <q-separator />
                <q-card-section>{{ submission.description }}</q-card-section>
                <template v-if="submission.attachments.length != 0">
                  <q-separator />
                  <q-card-section>
                    <div class="q-mb-sm">
                      <q-icon name="mdi-paperclip" />
                      <span class="q-ml-sm">Attachments:</span>
                    </div>
                    <q-list dense style="max-width: 50%">
                      <q-item
                        @click="download(attachment.contentType, attachment.data, attachment.name)"
                        clickable
                        :key="i"
                        v-for="(attachment, i) in submission.attachments"
                      >
                        <q-item-section class="text-subtitle2">{{ attachment.name }}</q-item-section>
                        <q-item-section
                          class="text-subtitle2"
                          side
                        >{{ attachment.size | byteCountToReadableFormat }}</q-item-section>
                      </q-item>
                    </q-list>
                  </q-card-section>
                </template>
              </q-card>
            </q-expansion-item>
          </template>
          <q-item v-else>
            <q-item-section>No submissions found!</q-item-section>
          </q-item>
        </q-list>
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
