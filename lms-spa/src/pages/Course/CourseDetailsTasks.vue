<template>
  <div>
    <div class="row">
      <div class="col-12">
        <q-option-group
          size="sm"
          v-model="showNotifications"
          :options="showNotificationsOptions"
          type="checkbox"
          color="primary"
          inline
          @input="showNotificationsValueChanged"
        />
        <div class="q-ml-md q-mb-md" :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']">
          *
          <q-icon size="xs" class="q-mr-xs" name="mdi-mouse" />Right click on tasks (or long tap on phones) for more options
        </div>
      </div>
      <q-skeleton v-show="tasksLoading" class="q-mx-sm" width="100%" height="150px" square />
      <template v-if="courseTasks && courseTasks.length != 0">
        <div class="col-xs-12 col-md-4" :key="i" v-for="(courseTask, i) in courseTasks">
          <task-card
            class="q-ma-sm"
            @edit="editTask"
            @view="viewTask"
            @delete="deleteTask"
            @submit="submitTask"
            :value="courseTask"
            :courseId="courseId"
          />
        </div>
      </template>
      <div v-show="!tasksLoading" v-else class="col-12 q-my-sm">
        <div>{{ $i18n.t('noData') }}</div>
      </div>
    </div>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="taskDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar class="bg-primary dialog-toolbar">
          <span>{{ dialogMode == "edit" ? 'Edit task' : 'Create task' }}</span>
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
          <div
            v-if="$v.task.description.$invalid"
            class="error-text q-pl-sm"
          >This field is required!</div>
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
            v-if="dialogMode == 'create'"
            @click="createCourseTask"
            class="q-mr-sm"
            color="primary"
            size="sm"
          >Create</q-btn>
          <q-btn v-else @click="editCourseTask" class="q-mr-sm" color="primary" size="sm">Save</q-btn>
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-page-sticky
      position="bottom-right"
      :offset="[18, 18]"
      v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageTasks, Privileges.CanCreateTasks) 
      && hasCoursePrivileges(courseId, Privileges.IsInvolvedToCourse)"
    >
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          icon="mdi-newspaper-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New task"
          @click="openTaskDialog('create')"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
import CourseTaskService from "../../services/api/course-task";
import { required, minLength, numeric } from "vuelidate/lib/validators";
import UserMixin from "../../mixins/userMixin";
import TaskCard from "../../components/task-card";
import { debounce } from "debounce";
import { base64StringToBlob } from "blob-util";

export default {
  name: "CourseDetailsTasks",
  components: {
    "task-card": TaskCard
  },
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
  created() {
    this.courseId = this.$route.params.id;
    this.getCourseTasks();
  },
  data() {
    return {
      activeTaskId: null,
      tasksLoading: false,
      courseId: null,
      taskDialog: false,
      dialogMode: "create",
      task: {
        title: "Task title",
        sendEmail: false,
        description: "Task description",
        dueDate: "2020-07-20 12:40",
        files: null,
        maximumGrade: 100
      },
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      },
      courseTasks: null,
      showNotifications: [0],
      showNotificationsOptions: [
        {
          label: "Show graded",
          value: 0
        },
        {
          label: "Show ungraded",
          value: 1
        }
      ]
    };
  },
  methods: {
    openTaskDialog(mode) {
      this.dialogMode = mode;

      if (this.dialogMode == "edit") {
        CourseTaskService.getTask(this.activeTaskId).then(({ data }) => {
          this.task = data;
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

      this.taskDialog = true;
    },
    showNotificationsValueChanged: debounce(function() {
      this.getCourseTasks();
    }, 1500),
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
        this.getCourseTasks();
        this.resetNewTaskDialog();
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
        this.getCourseTasks();
        this.resetNewTaskDialog();
      });
    },
    getCourseTasks() {
      this.tasksLoading = true;
      this.courseTasks = null;
      CourseTaskService.getCourseTasks(this.courseId)
        .then(({ data }) => {
          this.courseTasks = data;
        })
        .finally(() => {
          this.tasksLoading = false;
        });
    },
    resetNewTaskDialog() {
      this.task = {
        title: "Task title",
        sendEmail: false,
        description: "Task description",
        dueDate: "2020-07-20 12:40",
        files: null,
        maximumGrade: 100
      };
      this.taskDialog = false;
    },
    deleteTask(taskId) {
      console.log(taskId);
    },
    viewTask(taskId) {
      console.log(taskId);
    },
    editTask(taskId) {
      this.activeTaskId = taskId;
      this.openTaskDialog("edit");
    },
    submitTask(taskId) {
      console.log(taskId);
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
