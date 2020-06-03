<template>
  <div>
    <div class="row">
      <div class="col-12">
        <div class="row q-col-gutter-sm">
          <q-input
            outlined
            dense
            label="Name"
            clearable
            class="full-width"
            v-model="searchData.name"
            @input="searchValuesChanged"
          />
          <q-option-group
            size="sm"
            v-model="showTasks"
            :options="showTasksOptions"
            v-if="!hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
            type="checkbox"
            color="primary"
            inline
            @input="searchValuesChanged"
          />
          <q-option-group
            size="sm"
            v-model="showOverdue"
            :options="showOverdueOptions"
            type="checkbox"
            color="primary"
            inline
            @input="searchValuesChanged"
          />
        </div>
        <div class="q-ml-md q-my-sm" :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']">
          *
          <q-icon size="xs" class="q-mr-xs" name="mdi-mouse" />Right click on tasks (or long tap on phones) for more options
        </div>
      </div>
      <q-skeleton v-show="tasksLoading" class="q-mx-sm" width="100%" height="150px" square />
      <template v-if="courseTasks && courseTasks.length != 0">
        <div class="col-xs-12 col-sm-4" :key="i" v-for="(courseTask, i) in courseTasks">
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
      <div v-show="!tasksLoading" v-else class="col-12">
        <div>{{ $i18n.t('noData') }}</div>
      </div>
    </div>
    <create-edit-task-dialog
      :mode="dialogMode"
      :open="editCreateDialog"
      :activeTaskId="activeTaskId"
      :courseId="courseId"
      @close="editCreateDialog = false"
      @refresh="getCourseTasks"
    />
    <task-new-submission-dialog
      :mode="dialogMode"
      :open="createSubmissionDialog"
      :activeTaskId="activeTaskId"
      :courseId="courseId"
      :attemptId="activeAttemptId"
      @close="createSubmissionDialog = false"
      @newSubmission="getCourseTasks"
    />
    <task-view-submissions-dialog
      :open="viewSubmissionsDialog"
      :taskId="activeTaskId"
      :courseId="courseId"
      @close="viewSubmissionsDialog = false"
    />
    <q-page-sticky
      position="bottom-right"
      :offset="[18, 18]"
      v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageTasks, Privileges.CanCreateTasks) 
      && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
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
import UserMixin from "../../mixins/userMixin";
import TaskCard from "../../components/task-card";
import saveState from "vue-save-state";
import CreateEditTaskDialog from "../../components/create-edit-task-dialog";
import TaskNewSubmissionDialog from "../../components/task-new-submission-dialog";
import taskViewSubmissionsDialogVue from "../../components/task-view-submissions-dialog.vue";
import { debounce } from "debounce";

export default {
  name: "CourseDetailsTasks",
  components: {
    "task-card": TaskCard,
    "create-edit-task-dialog": CreateEditTaskDialog,
    "task-new-submission-dialog": TaskNewSubmissionDialog,
    "task-view-submissions-dialog": taskViewSubmissionsDialogVue
  },
  mixins: [UserMixin, saveState],
  created() {
    this.courseId = this.$route.params.id;
    this.getCourseTasks();
  },
  watch: {
    showOverdue: {
      deep: true,
      immediate: false,
      handler(newVal, oldVal) {
        // Never allow the value to be empty!!
        if (newVal.length == 0) {
          this.showOverdue = oldVal;
        }
      }
    }
  },
  data() {
    return {
      searchData: {
        name: null
      },
      activeAttemptId: null,
      activeTaskId: null,
      tasksLoading: false,
      courseId: null,
      editCreateDialog: false,
      createSubmissionDialog: false,
      viewSubmissionsDialog: false,
      dialogMode: "create",
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      },
      courseTasks: null,
      showTasks: [0],
      showOverdue: [0, 1],
      showTasksOptions: [
        {
          label: "Show graded",
          value: 0
        },
        {
          label: "Show ungraded",
          value: 1
        }
      ],
      showOverdueOptions: [
        {
          label: "Show overdue",
          value: 0
        },
        {
          label: "Show active tasks",
          value: 1
        }
      ]
    };
  },
  methods: {
    getSaveStateConfig() {
      return {
        cacheKey: "course-details-tasks",
        saveProperties: ["searchData", "showTasksOptions", "showOverdueOptions"]
      };
    },
    openSubmissionsDialog() {
      this.viewSubmissionsDialog = true;
    },
    openNewSubmissionDialog(mode) {
      this.dialogMode = mode;
      this.createSubmissionDialog = true;
    },
    openTaskDialog(mode) {
      this.dialogMode = mode;
      this.editCreateDialog = true;
    },
    searchValuesChanged: debounce(function() {
      this.getCourseTasks();
    }, 1500),
    getCourseTasks() {
      this.tasksLoading = true;
      this.courseTasks = null;
      CourseTaskService.getCourseTasks(
        this.courseId,
        this.searchData.name,
        this.showOverdue.includes(0),
        this.showOverdue.includes(1)
      )
        .then(({ data }) => {
          this.courseTasks = data.results;
        })
        .finally(() => {
          this.tasksLoading = false;
        });
    },
    deleteTask(taskId) {
      CourseTaskService.deleteTask(taskId, this.courseId).then(() => {
        this.getCourseTasks();
      });
    },
    viewTask(taskId) {
      this.activeTaskId = taskId;
      this.openSubmissionsDialog();
    },
    editTask(taskId) {
      this.activeTaskId = taskId;
      this.openTaskDialog("edit");
    },
    submitTask(taskId, userHasSubmission, attemptId) {
      this.activeTaskId = taskId;
      if (userHasSubmission) {
        this.activeAttemptId = attemptId;
      }
      this.openNewSubmissionDialog(userHasSubmission ? "edit" : "create");
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
