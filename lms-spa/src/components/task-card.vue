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
          && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
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
          <q-item-section>Submit attempt</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
    <q-bar dense style="height: 10px;" />
    <q-card-section class="q-py-sm">
      <span class="text-h6">{{ value.title }}</span>
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
    <q-separator />
    <q-card-actions
      class="text-subtitle1 q-ml-sm"
      v-if="!hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
    >
      Current grade:
      <span class="text-green-4 q-mx-xs">50</span> out of
      <span class="text-green-5 q-ml-xs">100</span>
    </q-card-actions>
    <q-card-actions class="text-subtitle1 q-ml-sm" v-else>
      Number of submissions:
      <span class="text-green-4 q-mx-xs">5</span>
    </q-card-actions>
  </q-card>
</template>

<script>
import { download, fileIcon } from "../helpers/helpers";
import UserMixin from "../mixins/userMixin";
import { format } from "date-fns";

export default {
  name: "task-card",
  props: ["value", "courseId"],
  mixins: [UserMixin],
  created() {
    this.value.attachments.forEach(x => (x.downloading = false));
  },
  data() {
    return {};
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
      this.$emit("submit", this.value.id);
    }
  }
};
</script>
