<template>
  <div>
    <div class="row justify-center">
      <div class="col-12 q-pa-md">
        <q-card>
          <q-card-section class="bg-grey-1">
            <div class="row">
              <div class="col-12 q-pb-md">
                <span class="text-weight-light text-h5">{{ $t("exam.info") }}</span>
              </div>
              <div class="col-6 q-pr-sm">
                <q-input v-model="exam.name" outlined dense :label="$t('exam.name')" />
              </div>
              <div class="col-6">
                <q-input
                  outlined
                  dense
                  :label="$t('timeNeeded')"
                  v-model="exam.timeNeeded"
                  mask="##:##"
                  :hint="$t('error.timeNeededFormat')"
                />
              </div>
              <div class="col-12 q-pt-sm">
                <q-input dense outlined v-model="exam.dueDate" :label="$('dueDate')" readonly>
                  <template v-slot:prepend>
                    <q-icon name="mdi-calendar-month" class="cursor-pointer">
                      <q-popup-proxy transition-show="scale" transition-hide="scale">
                        <q-date v-model="exam.dueDate" mask="YYYY-MM-DD HH:mm" />
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                  <template v-slot:append>
                    <q-icon name="mdi-alarm" class="cursor-pointer">
                      <q-popup-proxy transition-show="scale" transition-hide="scale">
                        <q-time v-model="exam.dueDate" mask="YYYY-MM-DD HH:mm" format24h />
                      </q-popup-proxy>
                    </q-icon>
                  </template>
                </q-input>
              </div>
            </div>
          </q-card-section>
          <q-separator />
          <q-card-section class="q-px-md">
            <div class="row">
              <div class="col-12 q-pb-sm">
                <span class="text-weight-light text-h5">{{ $t('questions') }}</span>
              </div>
              <div class="col-12">
                <q-chip
                  clickable
                  class="bg-green-6 text-white"
                  square
                  style="cursor: pointer; user-select: none;"
                  @click="addNewQuestion"
                >
                  <q-icon name="mdi-plus" />
                </q-chip>
                <template v-for="n in exam.questions.length">
                  <q-chip
                    clickable
                    square
                    :key="n"
                    style="cursor: pointer; user-select: none;"
                    @click="selectedQuestion = n - 1"
                  >{{ n }}</q-chip>
                </template>
              </div>
            </div>
          </q-card-section>
          <q-separator />
          <template v-for="(question, i) in exam.questions">
            <div :key="i" v-if="selectedQuestion === i">
              <q-card-section class="bg-grey-1">
                <div class="row items-center text-center">
                  <div class="col-6 text-center q-pr-sm">
                    <q-input v-model="question.title" outlined dense :label="$t('questionName')" />
                  </div>
                  <div class="col-5 text-center q-pr-sm">
                    <q-select
                      @input="questionTypeChanged(question)"
                      dense
                      outlined
                      v-model="question.typeId"
                      :label="$t('questionType')"
                      :options="questionTypeOptions"
                      behavior="menu"
                    />
                  </div>
                  <div class="col-1">
                    <q-btn
                      size="sm"
                      class="bg-red-5 text-white"
                      @click="exam.questions.splice(i, 1)"
                    >{{ $t('remove') }}</q-btn>
                  </div>
                  <div class="col-12 q-my-md">
                    <q-editor
                      v-model="question.content"
                      min-height="5rem"
                      :toolbar="toolbarOptions"
                      :fonts="fonts"
                    />
                  </div>
                  <div class="col-12 border-box">
                    <div class="text-h6 gore-desno">
                      <q-badge color="primary">{{ $t('preview') }}</q-badge>
                    </div>
                    <p class="q-pa-md" v-html="question.content"></p>
                  </div>
                </div>
              </q-card-section>
              <q-separator />
              <q-card-section>
                <div class="row">
                  <div class="col-12 q-pb-sm">
                    <span class="text-weight-light text-h5">{{ $t('answers') }}</span>
                    <q-btn
                      dense
                      size="sm"
                      class="bg-green-6 text-white q-ml-md q-mb-xs"
                      @click="addNewAnswer(question)"
                    >
                      <q-icon name="mdi-plus" />
                    </q-btn>
                  </div>
                  <div class="col-12">
                    <template v-for="(answer, i) in question.answers">
                      <q-input v-model="answer.content" outlined dense :key="i" class="q-py-xs">
                        <template v-slot:append>
                          <q-btn
                            @click="question.answers.splice(i, 1)"
                            dense
                            size="sm"
                            class="bg-red-6 text-white"
                          >{{ $t('remove') }}</q-btn>
                          <q-checkbox
                            :disable="!answer.correct && question.typeId.value == 1 && question.answers.reduce((sum, x) => sum += x.correct ? 1 : 0, 0) >= 1"
                            size="xs"
                            color="green-5"
                            v-model="answer.correct"
                          />
                        </template>
                      </q-input>
                    </template>
                  </div>
                  <div class="col-12 q-pl-sm">
                    <span class="hint-text">* {{ $t('tickIfCorrect') }}</span>
                  </div>
                </div>
              </q-card-section>
            </div>
          </template>
        </q-card>
      </div>
      <div class="col-12 text-right q-pr-md">
        <q-btn @click="createExam" size="sm" class="bg-green-5 text-white">{{ $t('save') }}</q-btn>
      </div>
    </div>
  </div>
</template>

<script>
import ExamService from "../services/api/exam";
import UserMixin from "../mixins/userMixin";

export default {
  name: "ExamDetails",
  mixins: [UserMixin],
  data() {
    return {
      selectedQuestion: 0,
      exam: null,
      toolbarOptions: [
        [
          {
            label: this.$q.lang.editor.align,
            icon: this.$q.iconSet.editor.align,
            fixedLabel: true,
            list: "only-icons",
            options: ["left", "center", "right", "justify"]
          }
        ],
        ["bold", "italic", "strike", "underline", "subscript", "superscript"],
        ["token", "hr", "link"],
        [
          {
            label: this.$q.lang.editor.formatting,
            icon: this.$q.iconSet.editor.formatting,
            list: "no-icons",
            options: ["p", "h1", "h2", "h3", "h4", "h5", "h6", "code"]
          },
          {
            label: this.$q.lang.editor.fontSize,
            icon: this.$q.iconSet.editor.fontSize,
            fixedLabel: true,
            fixedIcon: true,
            list: "no-icons",
            options: [
              "size-1",
              "size-2",
              "size-3",
              "size-4",
              "size-5",
              "size-6",
              "size-7"
            ]
          },
          {
            label: this.$q.lang.editor.defaultFont,
            icon: this.$q.iconSet.editor.font,
            fixedIcon: true,
            list: "no-icons",
            options: [
              "default_font",
              "arial",
              "arial_black",
              "comic_sans",
              "courier_new",
              "impact",
              "lucida_grande",
              "times_new_roman",
              "verdana"
            ]
          },
          "removeFormat"
        ],
        ["quote", "unordered", "ordered", "outdent", "indent"],
        ["undo", "redo"],
        ["viewsource"]
      ],
      fonts: {
        arial: "Arial",
        arial_black: "Arial Black",
        comic_sans: "Comic Sans MS",
        courier_new: "Courier New",
        impact: "Impact",
        lucida_grande: "Lucida Grande",
        times_new_roman: "Times New Roman",
        verdana: "Verdana"
      }
    };
  },
  methods: {
    questionTypeChanged(question) {
      question.answers.forEach(x => (x.correct = false));
    },
    createExam() {
      let newExam = JSON.parse(JSON.stringify(this.exam));
      newExam.questions.forEach(x => (x.typeId = x.typeId.value));
      newExam.timeNeeded = this.$options.filters.hoursMinutesToSecondsFilter(
        newExam.timeNeeded
      );
      newExam.dueDate = new Date(newExam.dueDate);
      newExam.createdById = this.user.id;
      newExam.subjectId = 147;
      ExamService.createExam(newExam);
    },
    addNewAnswer(question) {
      question.answers.push({
        id: 1,
        content: this.$t("newAnswer"),
        correct: false
      });
    },
    addNewQuestion() {
      this.exam.questions.push({
        title: null,
        content: "",
        typeId: this.questionTypeOptions[0],
        answers: []
      });
    }
  },
  created() {
    this.questionTypes = { RADIO: 1, CHECKBOX: 2 };
    this.questionTypeOptions = [
      {
        label: this.$t("questionTypes.radio"),
        value: this.questionTypes.RADIO
      },
      {
        label: this.$t("questionTypes.checkbox"),
        value: this.questionTypes.CHECKBOX
      }
    ];
    this.exam = {
      name: "Exam name",
      timeNeeded: "01:00",
      dueDate: "2019-02-01 12:44",
      questions: [
        {
          id: 1,
          title: "Question 1",
          content: "<b> Question contents </b>",
          typeId: questionTypeOptions[0],
          answers: [
            {
              content: "Answer 1",
              correct: true
            }
          ]
        }
      ]
    };
  }
};
</script>

<style scoped lang="sass">
.border-box
  position: relative
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 6px
.gore-desno
  position: absolute
  top: 5px
  right: 10px
.hint-text
  font-size: 11px
  color: rgb(141, 141, 141)
</style>