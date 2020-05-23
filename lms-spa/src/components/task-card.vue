<template>
  <q-card>
    <q-menu touch-position context-menu>
      <q-list
        :class="`${$q.dark.isActive ? 'border-dark' : 'border-light'}`"
        dense
        separator
        style="min-width: 100px; border-radius: 6px;"
      >
        <q-item
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageTasks, Privileges.CanDeleteTasks) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
          clickable
          v-close-popup
          @click="$deleteTask"
        >
          <q-item-section>Delete task</q-item-section>
        </q-item>
        <q-item
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageTasks, Privileges.CanCreateTasks) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse) && !isExpired"
          clickable
          v-close-popup
          @click="$editTask"
        >
          <q-item-section>Edit task</q-item-section>
        </q-item>
        <q-item
          v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageTasks, Privileges.CanGradeTasks) 
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
          clickable
          v-close-popup
          @click="$viewSubmissions"
        >
          <q-item-section>View submissions</q-item-section>
        </q-item>
        <q-item
          v-if="!hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
          clickable
          v-close-popup
          @click="$submitAttempt"
        >
          <q-item-section>{{ userHasSubmittedAttempt ? 'Edit attempt' : 'Submit attempt' }}</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
    <q-bar style="height: 10px;" :class="isExpired ? 'bar-expired' : ''" />
    <q-card-section class="q-py-sm" :class="isExpired ? 'header-expired' : ''">
      <span :class="isExpired ? 'text-white' : ''" class="text-h6">{{ value.title }}</span>
    </q-card-section>
    <q-separator />
    <q-card-section horizontal>
      <q-card-actions vertical class="justify-around text-subtitle1">
        <span class="q-mr-sm">
          <q-icon
            size="xs"
            :class="!$q.screen.xs && !$q.screen.sm ? 'q-mr-sm' : 'q-ml-sm'"
            color="indigo"
            name="mdi-account"
          />
          <span v-if="!$q.screen.xs && !$q.screen.sm">Created by</span>
        </span>
        <span class="q-mr-sm">
          <q-icon
            size="xs"
            :class="!$q.screen.xs && !$q.screen.sm ? 'q-mr-sm' : 'q-ml-sm'"
            color="blue"
            name="mdi-calendar-check"
          />
          <span v-if="!$q.screen.xs && !$q.screen.sm">Created at</span>
        </span>
        <span class="q-mr-sm">
          <q-icon
            size="xs"
            :class="!$q.screen.xs && !$q.screen.sm ? 'q-mr-sm' : 'q-ml-sm'"
            color="red-7"
            name="mdi-clock-out"
          />
          <span v-if="!$q.screen.xs && !$q.screen.sm">Due date</span>
        </span>
      </q-card-actions>
      <q-separator vertical />
      <q-card-section
        class="text-subtitle1"
        :class="[ $q.dark.isActive ? 'text-grey-6' : 'text-black' ]"
      >
        <div>{{ value.createdBy }}</div>
        <div class="q-my-md">{{ format(new Date(value.submittedAt), 'yyyy-MM-dd HH:mm') }}</div>
        <div>{{ format(new Date(value.dueDate), 'yyyy-MM-dd HH:mm') }}</div>
      </q-card-section>
    </q-card-section>
    <template v-if="hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)">
      <q-separator />
      <q-card-actions class="text-subtitle1 q-ml-sm">
        Number of submissions:
        <span class="text-green-4 q-mx-xs">{{ value.numberOfSubmissions }}</span>
      </q-card-actions>
    </template>
  </q-card>
</template>

<script>
import { download, fileIcon } from "../helpers/helpers";
import { format, compareAsc } from "date-fns";
import UserMixin from "../mixins/userMixin";
import CourseTaskService from "../services/api/course-task";

export default {
  name: "task-card",
  props: ["value", "courseId"],
  mixins: [UserMixin],
  created() {
    this.value.attachments.forEach(x => (x.downloading = false));
    CourseTaskService.getTaskAttempts(this.value.id, this.courseId).then(
      ({ data }) => {
        let attempt = data.find(x => x.userId == this.user.id);
        if (attempt != undefined) {
          this.userHasSubmittedAttempt = true;
          this.attemptId = attempt.id;
        }
      }
    );
  },
  data() {
    return {
      userHasSubmittedAttempt: false,
      attemptId: null
    };
  },
  computed: {
    isExpired() {
      return compareAsc(new Date(this.value.dueDate), new Date()) == -1;
    }
  },
  methods: {
    format,
    fileIcon,
    downloadFile(attachment) {
      download(attachment.contentType, attachment.data, attachment.name);
    },
    $deleteTask() {
      this.$emit("delete", this.value.id);
    },
    $editTask() {
      this.$emit("edit", this.value.id);
    },
    $viewSubmissions() {
      this.$emit("view", this.value.id);
    },
    $submitAttempt() {
      this.$emit(
        "submit",
        this.value.id,
        this.userHasSubmittedAttempt,
        this.attemptId
      );
    }
  }
};
</script>

<style lang="sass">
.bar-expired
  background-color: rgb(138, 19, 19)
.header-expired
  background-color: rgb(181, 24, 24)
</style>
