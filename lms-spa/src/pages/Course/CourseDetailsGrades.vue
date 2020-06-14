<template>
  <div class="row q-col-gutter-sm">
    <div class="col-xs-12 col-md-6">
      <q-table
        hide-pagination
        v-if="tasks"
        dense
        :data="tasks"
        :columns="taskColumns"
        row-key="name"
      >
        <template v-slot:top>
          <div class="row full-width">
            <div class="col-12">
              <span class="text-weight-light text-h5 q-pl-md">Tasks</span>
            </div>
            <div class="col-12 q-py-sm">
              <q-separator />
            </div>
          </div>
        </template>
      </q-table>
    </div>
    <div class="col-xs-12 col-md-6">
      <q-table
        hide-pagination
        v-if="exams"
        dense
        :data="exams"
        :columns="taskColumns"
        row-key="name"
      >
        <template v-slot:top>
          <div class="row full-width">
            <div class="col-12">
              <span class="text-weight-light text-h5 q-pl-md">Exams</span>
            </div>
            <div class="col-12 q-py-sm">
              <q-separator />
            </div>
          </div>
        </template>
      </q-table>
    </div>
  </div>
</template>

<script>
import UserMixin from "../../mixins/userMixin";
import CourseService from "../../services/api/course";

export default {
  name: "CourseDetailsGrades",
  mixins: [UserMixin],
  created() {
    this.courseId = this.$route.params.id;
    CourseService.getUserGrades(this.courseId, this.user.id).then(
      ({ data }) => {
        this.tasks = data.tasks;
        this.exams = data.exams;
      }
    );
  },
  methods: {},
  data() {
    return {
      taskColumns: [
        {
          name: "name",
          label: "Name",
          field: "name",
          align: "left"
        },
        {
          name: "grade",
          label: "Grade",
          field: "grade",
          align: "center"
        },
        {
          name: "maximumGrade",
          label: "Maximum grade",
          field: "maximumGrade",
          align: "center"
        }
      ],
      tasks: null,
      exams: null
    };
  }
};
</script>

<style lang="sass">
.q-table--dense .q-table__top
  padding: 6px 0px !important
</style>