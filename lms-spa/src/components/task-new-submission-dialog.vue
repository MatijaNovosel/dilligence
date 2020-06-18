<template>
  <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="open" persistent>
    <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
      <q-toolbar
        :class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
        class="text-white dialog-toolbar"
      >
        <span>{{ mode == "edit" ? 'View submission' : 'New submission' }}</span>
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
        <div class="q-mb-md text-subtitle1">
          <q-icon size="xs" class="q-mr-sm" name="mdi-calendar-check" />Task details
        </div>
        <p class="text-subtitle1">
          <span class="text-grey-6" style="font-size: 12px;">Title</span>
          {{ taskInfo.title }}
        </p>
        <html-description-box :html="taskInfo.description" />
        <div>
          <div class="q-mb-sm">
            <q-icon name="mdi-paperclip" />
            <span class="q-ml-sm text-subtitle1">Attachments</span>
          </div>
          <q-list
            dense
            :style="$q.screen.xs || $q.screen.sm ? fileListStyleSmall : fileListStyleNormal"
          >
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
      <q-card-section class="q-gutter-sm q-pb-none" v-if="!expired">
        <q-separator />
        <div class="q-mb-md text-subtitle1">
          <q-icon size="xs" class="q-mr-sm" name="mdi-file-multiple" />Your submission
        </div>
        <q-editor
          ref="editor_ref"
          v-if="submission.gradedById == null"
          v-model="submission.description"
          min-height="5rem"
          class="q-mb-md"
          @paste.native="evt => pasteCapture(evt)"
        />
        <html-description-box v-else :html="submission.description" />
        <q-file
          v-if="submission.gradedById == null"
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
        <div v-else class="q-mb-md">
          <template v-if="submission.files">
            <div class="q-mb-sm">
              <q-icon name="mdi-paperclip" />
              <span class="q-ml-sm text-subtitle1">Attachments</span>
            </div>
            <q-list dense style="max-width: 50%">
              <q-item
                @click="download(attachment.contentType, attachment.data, attachment.name)"
                clickable
                :key="i"
                v-for="(attachment, i) in submission.files"
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
          </template>
        </div>
      </q-card-section>
      <q-card-section class="q-pt-none text-right" v-if="submission.gradedById == null">
        <template v-if="!expired">
          <q-btn
            v-if="mode == 'create'"
            @click="submitAttempt"
            class="q-mt-md"
            color="primary"
            size="sm"
          >Submit</q-btn>
          <q-btn class="q-mt-md" v-else @click="editSubmission" color="primary" size="sm">Save</q-btn>
        </template>
      </q-card-section>
      <template v-else>
        <q-separator />
        <q-card-section class="q-gutter-sm">
          <div class="q-mb-md text-subtitle1">
            <q-icon size="xs" class="q-mr-md" name="fas fa-chalkboard-teacher" />Gradee details
          </div>
          <html-description-box :html="submission.gradeeComment" />
          <div>
            <q-icon name="mdi-account" class="q-mr-xs" />
            {{ submission.gradedBy }}
          </div>
          <div>
            <q-icon name="mdi-school" class="q-mr-sm" />
            <span class="text-green-4">{{ submission.grade }}</span>
            / {{ submission.maximumGrade }}
          </div>
        </q-card-section>
      </template>
    </q-card>
  </q-dialog>
</template>

<script>
import UserMixin from "../mixins/userMixin";
import CourseTaskService from "../services/api/course-task";
import HtmlDescriptionBox from "../components/html-description-box";
import NotificationService from "../services/notification/notifications";
import { download, fileIcon } from "../helpers/helpers";
import { base64StringToBlob } from "blob-util";
import { isPast } from "date-fns";

export default {
  name: "task-new-submission-dialog",
  props: ["mode", "activeTaskId", "open", "courseId", "attemptId"],
  mixins: [UserMixin],
  components: {
    "html-description-box": HtmlDescriptionBox
  },
  data() {
    return {
      taskInfo: null,
      fileListStyleSmall: {
        "max-width": "100%"
      },
      fileListStyleNormal: {
        "max-width": "50%"
      },
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
  computed: {
    expired() {
      if (this.taskInfo != null) {
        return isPast(new Date(this.taskInfo.dueDate));
      }
      return false;
    }
  },
  methods: {
    fileIcon,
    download,
    pasteCapture(evt) {
      let text, onPasteStripFormattingIEPaste;
      evt.preventDefault();
      if (evt.originalEvent && evt.originalEvent.clipboardData.getData) {
        text = evt.originalEvent.clipboardData.getData("text/plain");
        this.$refs.editor_ref.runCmd("insertText", text);
      } else if (evt.clipboardData && evt.clipboardData.getData) {
        text = evt.clipboardData.getData("text/plain");
        this.$refs.editor_ref.runCmd("insertText", text);
      } else if (window.clipboardData && window.clipboardData.getData) {
        if (!onPasteStripFormattingIEPaste) {
          onPasteStripFormattingIEPaste = true;
          this.$refs.editor_ref.runCmd("ms-pasteTextOnly", text);
        }
        onPasteStripFormattingIEPaste = false;
      }
    },
    submitAttempt() {
      let formData = new FormData();

      formData.append("userId", this.user.id);
      formData.append("courseId", this.courseId);
      formData.append("description", this.submission.description);
      formData.append("courseTaskId", this.activeTaskId);
      formData.append("submittedById", this.user.id);

      if (this.submission.files != null) {
        this.submission.files.forEach(file => formData.append("files", file));
      }

      CourseTaskService.addNewSubmission(formData, this.courseId).then(() => {
        this.reset();
        NotificationService.showSuccess("Submission sent!");
        this.$emit("newSubmission");
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

      CourseTaskService.editSubmission(formData, this.courseId).then(() => {
        NotificationService.showSuccess("Submission updated!");
        this.reset();
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
        CourseTaskService.getTask(this.activeTaskId, this.courseId).then(
          ({ data }) => {
            this.taskInfo = data;
            if (this.mode == "edit") {
              CourseTaskService.getTaskAttemptDetails(
                this.attemptId,
                this.courseId
              ).then(({ data }) => {
                this.submission = data;
                if (this.submission.gradedById == null) {
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
                }
              });
            }
          }
        );
      }
    }
  }
};
</script>
