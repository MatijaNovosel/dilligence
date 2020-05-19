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
      <q-card-section horizontal v-if="submissions && !$q.screen.xs && !$q.screen.sm">
        <q-card-section style="width: 40%" class="q-pa-none">
          <q-scroll-area
            :thumb-style="thumbStyle"
            :bar-style="barStyle"
            visible
            style="height: 450px"
          >
            <q-list>
              <q-item
                @click="getTaskAttemptDetails(submission.id)"
                clickable
                v-for="(submission, i) in submissions"
                :key="i"
              >
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
                  <span
                    :class="submission.gradedById == null ? 'text-red-6' : 'text-green-6'"
                    class="text-subtitle2"
                  >{{ submission.gradedById == null ? 'Ungraded' : 'Graded' }}</span>
                </q-item-section>
              </q-item>
            </q-list>
          </q-scroll-area>
        </q-card-section>
        <q-separator vertical />
        <q-card-section style="width: 60%;" class="q-pr-xs">
          <q-skeleton v-if="loading" style="width: 100%; height: 35%;" />
          <q-scroll-area
            v-else
            visible
            :thumb-style="thumbStyle"
            :bar-style="barStyle"
            style="height: 450px"
          >
            <div class="q-pr-md">
              <template v-if="details">
                <div class="q-mb-md">
                  <q-icon name="mdi-text" />
                  <span class="q-ml-sm text-subtitle1">Description</span>
                </div>
                <p
                  style="word-wrap: break-word"
                  class="text-subtitle2"
                  v-html="details.description"
                />
                <template v-if="details.attachments.length != 0">
                  <q-separator class="q-my-md" />
                  <div class="q-mb-sm">
                    <q-icon name="mdi-paperclip" />
                    <span class="q-ml-sm text-subtitle1">Attachments</span>
                  </div>
                  <q-list dense>
                    <q-item
                      @click="download(attachment.contentType, attachment.data, attachment.name)"
                      clickable
                      :key="i"
                      v-for="(attachment, i) in details.attachments"
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
                <q-separator class="q-my-md" />
                <div class="q-mb-md">
                  <q-icon name="mdi-school" />
                  <span class="q-ml-sm text-subtitle1">
                    Grade (Max
                    <span class="text-green-5">{{ details.maximumGrade }}</span>)
                  </span>
                  <div>
                    <q-input
                      :error="$v.details.grade.$invalid && $v.details.grade.$dirty"
                      @input="$v.details.grade.$touch()"
                      no-error-icon
                      hide-bottom-space
                      v-model="details.grade"
                      class="q-my-sm"
                      dense
                      outlined
                      label="Grade"
                    />
                    <q-editor class="q-my-sm" v-model="details.gradeeComment" min-height="5rem" />
                  </div>
                  <div class="text-subtitle1" v-if="details.gradedById != null">
                    <span class="text-orange-8">Graded by:</span>
                    {{ details.gradedBy }}
                  </div>
                  <div class="text-right q-mt-md">
                    <q-btn
                      label="Save"
                      :disabled="$v.details.$invalid"
                      @click="gradeAttempt"
                      size="sm"
                      color="green-8"
                    />
                  </div>
                </div>
              </template>
            </div>
          </q-scroll-area>
        </q-card-section>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script>
import UserMixin from "../mixins/userMixin";
import CourseTaskService from "../services/api/course-task";
import { format } from "date-fns";
import { fileIcon, download } from "../helpers/helpers";
import { required, integer, between } from "vuelidate/lib/validators";

export default {
  name: "task-view-submissions-dialog",
  mixins: [UserMixin],
  props: ["open", "taskId", "courseId"],
  validations() {
    if (this.details) {
      return {
        details: {
          grade: {
            required,
            integer,
            between: between(0, this.details.maximumGrade)
          }
        }
      };
    }
  },
  data() {
    return {
      loading: false,
      submissions: null,
      details: null,
      activeTaskAttemptId: null,
      addComment: false,
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
        width: "80%",
        "max-width": "90vw"
      }
    };
  },
  methods: {
    fileIcon,
    format,
    download,
    gradeAttempt() {
      let payload = {
        attemptId: this.activeTaskAttemptId,
        gradedById: this.user.id,
        grade: this.details.grade,
        comment: this.details.gradeeComment,
        courseId: this.courseId
      };
      CourseTaskService.gradeAttempt(payload).then(() => {
        this.getTaskAttempts();
        this.getTaskAttemptDetails(this.activeTaskAttemptId);
        this.$q.notify({
          type: "positive",
          message: "Submission was successfully graded!"
        });
      });
    },
    reset() {
      this.$emit("close");
    },
    getTaskAttempts() {
      CourseTaskService.getTaskAttempts(this.taskId, this.courseId).then(
        ({ data }) => {
          this.submissions = data;
        }
      );
    },
    getTaskAttemptDetails(taskAttemptId) {
      this.loading = true;
      this.activeTaskAttemptId = taskAttemptId;
      CourseTaskService.getTaskAttemptDetails(taskAttemptId, this.courseId)
        .then(({ data }) => {
          if (data.gradeeComment == null) {
            data.gradeeComment = "";
          }
          this.details = data;
        })
        .finally(() => {
          this.loading = false;
        });
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
        this.getTaskAttempts();
      }
    }
  }
};
</script>

<style lang="sass">
.absolute-bottom-right-more
  bottom: 20px
  right: 20px
  position: absolute
</style>