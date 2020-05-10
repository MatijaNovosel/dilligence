<template>
  <div>
    <div class="row">
      <div class="col-4" :key="i" v-for="(courseTask, i) in courseTasks">
        {{ courseTask.title }}
      </div>
    </div>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="newTaskDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar class="bg-primary dialog-toolbar">
          <span>Create new task</span>
          <q-space />
          <q-btn
            :ripple="false"
            dense
            size="sm"
            color="white"
            flat
            round
            icon="mdi-close-circle"
            @click="resetNewTaskDialog"
          />
        </q-toolbar>
        <q-card-section class="q-gutter-sm q-pb-none">
          <q-input
            hint="The title of the task"
            :error="$v.newTask.title.$invalid"
            error-message="This field is required!"
            dense
            outlined
            v-model="newTask.title"
            label="Title"
          />
          <q-input
            dense
            outlined
            v-model="newTask.dueDate"
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
                    v-model="newTask.dueDate"
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
                    v-model="newTask.dueDate"
                    mask="YYYY-MM-DD HH:mm"
                    format24h
                  />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-file
            dense
            multiple
            outlined
            v-model="newTask.files"
            class="q-pr-sm"
            clearable
            label="Attachments"
          >
            <template v-slot:append>
              <q-icon name="mdi-paperclip" />
            </template>
          </q-file>
          <q-editor
            :content-style="{ [$v.newTask.description.$invalid && 'border']: '1px solid #C10015' }"
            v-model="newTask.description"
            min-height="5rem"
          />
          <div
            v-if="$v.newTask.description.$invalid"
            class="error-text q-pl-sm"
          >This field is required!</div>
          <div
            v-else
            class="q-pl-sm"
            :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
          >The description of what the task should be</div>
          <q-checkbox
            v-model="newTask.sendEmail"
            size="xs"
            class="q-mt-none"
            label="Send email to subscribed users"
          />
        </q-card-section>
        <q-card-actions class="justify-end q-pt-none">
          <q-btn @click="createCourseTask" class="q-mr-sm" color="primary" size="sm">Create</q-btn>
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-page-sticky position="bottom-right" :offset="[18, 18]">
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          icon="mdi-newspaper-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New task"
          @click="newTaskDialog = true"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
import CourseTaskService from "../../services/api/course-task";
import { required, minLength } from "vuelidate/lib/validators";
import UserMixin from "../../mixins/userMixin";

export default {
  name: "CourseDetailsTasks",
  mixins: [UserMixin],
  validations: {
    newTask: {
      description: {
        required,
        minLength: minLength(4)
      },
      title: {
        required,
        minLength: minLength(4)
      }
    }
  },
  created() {
    this.courseId = this.$route.params.id;
    this.getCourseTasks();
  },
  data() {
    return {
      courseId: null,
      newTaskDialog: false,
      newTask: {
        title: "Task title",
        sendEmail: false,
        description: "Task description",
        dueDate: "2020-07-20 12:40",
        files: null
      },
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      },
      courseTasks: null
    };
  },
  methods: {
    createCourseTask() {
      let formData = new FormData();
      let task = this.newTask;

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
        this.getCourseTasks();
        this.resetNewTaskDialog();
      });
    },
    getCourseTasks() {
      CourseTaskService.getCourseTasks(this.courseId).then(({ data }) => {
        this.courseTasks = data;
      });
    },
    resetNewTaskDialog() {
      this.newTask = {
        title: "Task title",
        sendEmail: false,
        description: "Task description",
        dueDate: "2020-07-20 12:40",
        files: null
      };
      this.newTaskDialog = false;
    }
  }
};
</script>

<style lang="sass">
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
.dialog-toolbar
  min-height: 30px
</style>
