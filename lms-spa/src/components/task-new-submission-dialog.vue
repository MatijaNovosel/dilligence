<template>
  <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="open" persistent>
    <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
      <q-toolbar class="bg-primary dialog-toolbar">
        <span>{{ mode == "edit" ? 'Edit submission' : 'New submission' }}</span>
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
      <q-card-section class="q-gutter-sm q-pb-none">
        <q-file
          ref="filePicker"
          dense
          multiple
          outlined
          v-model="submission.files"
          class="q-pr-sm"
          clearable
          label="Attachments"
          counter
        >
          <template v-slot:append>
            <q-icon name="mdi-paperclip" />
          </template>
        </q-file>
        <q-editor v-model="submission.description" min-height="5rem" class="q-mb-md" />
      </q-card-section>
      <q-card-actions class="justify-end q-pt-none">
        <q-btn
          v-if="mode == 'create'"
          @click="submitAttempt"
          class="q-mr-sm"
          color="primary"
          size="sm"
        >Submit</q-btn>
        <q-btn v-else @click="editSubmission" class="q-mr-sm" color="primary" size="sm">Save</q-btn>
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script>
import UserMixin from "../mixins/userMixin";
import CourseTaskService from "../services/api/course-task";

export default {
  name: "task-new-submission-dialog",
  props: ["mode", "activeTaskId", "open", "courseId"],
  mixins: [UserMixin],
  data() {
    return {
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      },
      submission: {
        description: "Description",
        files: null
      }
    };
  },
  methods: {
    submitAttempt() {
      let formData = new FormData();

      formData.append("userId", this.user.id);
      formData.append("courseId", this.courseId);
      formData.append("description", this.submission.description);
      formData.append("courseTaskId", this.activeTaskId);

      if (this.submission.files != null) {
        this.submission.files.forEach(file => formData.append("files", file));
      }

      CourseTaskService.addNewSubmission(formData).then(() => {
        this.reset();
      });
    },
    editSubmission() {
      let formData = new FormData();

      formData.append("userId", this.user.id);
      formData.append("courseId", this.courseId);
      formData.append("description", this.submission.description);
      formData.append("courseTaskId", this.activeTaskId);

      if (this.submission.files != null) {
        this.submission.files.forEach(file => formData.append("files", file));
      }
    },
    reset() {
      this.submission = {
        description: "Description",
        files: null
      };
      this.$emit("close");
    }
  }
};
</script>
