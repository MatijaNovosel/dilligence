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
      <q-card-section v-if="taskInfo">
        <p class="text-subtitle1">{{ taskInfo.title }}</p>
        <p
          :class="[$q.dark.isActive ? 'border-box-dark' : 'border-box-light']"
          v-html="taskInfo.description"
        />
        <div>
          <div class="q-mb-sm">
            <q-icon name="mdi-paperclip" />
            <span class="q-ml-sm">Attachments:</span>
          </div>
          <q-list dense style="max-width: 50%">
            <q-item
              @click="download(attachment.contentType, attachment.data, attachment.name)"
              clickable
              :key="i"
              v-for="(attachment, i) in taskInfo.attachments"
            >
              <q-item-section avatar>
                <q-icon
                  size="xs"
                  :name="fileIcon(attachment.name.slice(attachment.name.lastIndexOf('.') + 1))"
                />
              </q-item-section>
              <q-item-section class="text-subtitle2">{{ attachment.name }}</q-item-section>
              <q-item-section
                class="text-subtitle2"
                side
              >{{ attachment.size | byteCountToReadableFormat }}</q-item-section>
            </q-item>
          </q-list>
        </div>
      </q-card-section>
      <q-separator />
      <q-card-section class="q-gutter-sm q-pb-none">
        <div class="q-ml-sm q-mb-md text-subtitle1">Your submission:</div>
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
import { download, fileIcon } from "../helpers/helpers";
import { base64StringToBlob } from "blob-util";

export default {
  name: "task-new-submission-dialog",
  props: ["mode", "activeTaskId", "open", "courseId", "attemptId"],
  mixins: [UserMixin],
  data() {
    return {
      taskInfo: null,
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
    fileIcon,
    download,
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
        this.$q.notify({
          type: "positive",
          message: "Submission has been successfully sent!"
        });
      });
    },
    editSubmission() {
      let formData = new FormData();

      formData.append("userId", this.user.id);
      formData.append("id", this.attemptId);
      formData.append("courseId", this.courseId);
      formData.append("description", this.submission.description);
      formData.append("courseTaskId", this.activeTaskId);

      if (this.submission.files != null) {
        this.submission.files.forEach(file => formData.append("files", file));
      }

      CourseTaskService.editSubmission(formData).then(() => {
        this.$q.notify({
          type: "positive",
          message: "Attempt successfully updated!"
        });
      });
    },
    reset() {
      this.submission = {
        description: "Description",
        files: null
      };
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
        CourseTaskService.getTask(this.activeTaskId).then(({ data }) => {
          this.taskInfo = data;
          if (this.mode == "edit") {
            CourseTaskService.getTaskAttemptDetails(
              this.attemptId,
              this.courseId
            ).then(({ data }) => {
              this.submission = data;
              let files = [];
              data.attachments.forEach(attachment => {
                files.push(
                  new File(
                    [
                      base64StringToBlob(
                        attachment.data,
                        attachment.contentType
                      )
                    ],
                    attachment.name
                  )
                );
              });
              this.$refs.filePicker.addFiles(files);
            });
          }
        });
      }
    }
  }
};
</script>
