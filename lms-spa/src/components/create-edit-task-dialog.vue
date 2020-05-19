<template>
  <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="open" persistent>
    <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
      <q-toolbar
        :class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
        class="text-white dialog-toolbar"
      >
        <span>{{ mode == "edit" ? 'Edit task' : 'Create task' }}</span>
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
        <q-input
          hint="The title of the task"
          :error="$v.task.title.$invalid"
          error-message="This field is required!"
          dense
          outlined
          v-model="task.title"
          label="Title"
        />
        <q-input
          dense
          outlined
          v-model="task.dueDate"
          label="Due date"
          readonly
          hint="The date at which the task can no longer be submitted"
        >
          <template v-slot:prepend>
            <q-icon name="mdi-calendar-month" class="cursor-pointer">
              <q-popup-proxy transition-show="scale" transition-hide="scale">
                <q-date
                  :class="$q.dark.isActive ? 'border-dark' : 'border-light'"
                  minimal
                  v-model="task.dueDate"
                  mask="YYYY-MM-DD HH:mm"
                />
              </q-popup-proxy>
            </q-icon>
          </template>
          <template v-slot:append>
            <q-icon name="mdi-alarm" class="cursor-pointer">
              <q-popup-proxy transition-show="scale" transition-hide="scale">
                <q-time
                  :class="$q.dark.isActive ? 'border-dark' : 'border-light'"
                  v-model="task.dueDate"
                  mask="YYYY-MM-DD HH:mm"
                  format24h
                />
              </q-popup-proxy>
            </q-icon>
          </template>
        </q-input>
        <q-input
          hint="The maximum attainable number of points"
          :error="$v.task.maximumGrade.$invalid"
          error-message="This field is required!"
          dense
          outlined
          v-model="task.maximumGrade"
          label="Maximum grade"
        />
        <q-file
          ref="filePicker"
          dense
          multiple
          outlined
          v-model="task.files"
          class="q-pr-sm"
          clearable
          label="Attachments"
          counter
        >
          <template v-slot:append>
            <q-icon name="mdi-paperclip" />
          </template>
        </q-file>
        <q-editor
          :content-style="{ [$v.task.description.$invalid && 'border']: '1px solid #C10015' }"
          v-model="task.description"
          min-height="5rem"
        />
        <div v-if="$v.task.description.$invalid" class="error-text q-pl-sm">This field is required!</div>
        <div
          v-else
          class="q-pl-sm"
          :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
        >The description of what the task should be</div>
        <q-checkbox
          v-model="task.sendEmail"
          size="xs"
          class="q-mt-none"
          label="Send email to subscribed users"
        />
      </q-card-section>
      <q-card-actions class="justify-end q-pt-none">
        <q-btn
          v-if="mode == 'create'"
          @click="createCourseTask"
          class="q-mr-sm"
          color="primary"
          size="sm"
        >Create</q-btn>
        <q-btn v-else @click="editCourseTask" class="q-mr-sm" color="primary" size="sm">Save</q-btn>
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script>
import UserMixin from "../mixins/userMixin";
import { base64StringToBlob } from "blob-util";
import { format, add } from "date-fns";
import { required, minLength, numeric } from "vuelidate/lib/validators";
import CourseTaskService from "../services/api/course-task";

export default {
  name: "create-edit-task-dialog",
  props: ["mode", "activeTaskId", "open", "courseId"],
  mixins: [UserMixin],
  validations: {
    task: {
      description: {
        required,
        minLength: minLength(4)
      },
      title: {
        required,
        minLength: minLength(4)
      },
      maximumGrade: {
        required,
        numeric,
        minLength: minLength(2)
      }
    }
  },
  data() {
    return {
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      },
      task: {
        title: "Task title",
        sendEmail: false,
        description: "Task description",
        dueDate: format(add(new Date(), { days: 1 }), "yyyy-MM-dd HH:mm"),
        files: null,
        maximumGrade: 100
      }
    };
  },
  methods: {
    createCourseTask() {
      let formData = new FormData();
      let task = this.task;

      formData.append("createdById", this.user.id);
      formData.append("courseId", this.courseId);

      for (let [key, value] of Object.entries(task)) {
        if (key != "files") {
          formData.append(key, value);
        }
      }

      if (task.files != null) {
        task.files.forEach(file => formData.append("files", file));
      }

      CourseTaskService.createCourseTask(formData).then(() => {
        this.$emit("refresh");
        this.reset();
        this.$q.notify({
          type: "positive",
          message: "Task successfully created!"
        });
      });
    },
    editCourseTask() {
      let formData = new FormData();

      formData.append("id", this.activeTaskId);
      formData.append("title", this.task.title);
      formData.append("description", this.task.description);
      formData.append("dueDate", this.task.dueDate);
      formData.append("sendEmail", this.task.sendEmail);
      formData.append("maximumGrade", this.task.maximumGrade);
      formData.append("courseId", this.courseId);

      if (this.task.files != null) {
        this.task.files.forEach(file => formData.append("files", file));
      }

      CourseTaskService.updateTask(formData).then(() => {
        this.$emit("refresh");
        this.reset();
        this.$q.notify({
          type: "positive",
          message: "Task successfully updated!"
        });
      });
    },
    reset() {
      this.task = {
        title: "Task title",
        sendEmail: false,
        description: "Task description",
        dueDate: format(add(new Date(), { days: 1 }), "yyyy-MM-dd HH:mm"),
        files: null,
        maximumGrade: 100
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
        if (this.mode == "edit") {
          CourseTaskService.getTask(this.activeTaskId).then(({ data }) => {
            this.task = data;
            this.task.dueDate = format(
              add(new Date(this.task.dueDate), { days: 1 }),
              "yyyy-MM-dd HH:mm"
            );
            this.task.sendEmail = false;
            let files = [];
            data.attachments.forEach(attachment => {
              files.push(
                new File(
                  [base64StringToBlob(attachment.data, attachment.contentType)],
                  attachment.name
                )
              );
            });
            this.$refs.filePicker.addFiles(files);
          });
        }
      }
    }
  }
};
</script>
