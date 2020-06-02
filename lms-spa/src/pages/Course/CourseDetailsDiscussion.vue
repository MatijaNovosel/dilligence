<template>
  <div>
    <div class="row q-mt-md">
      <div class="col-12">
        <div class="row q-col-gutter-sm justify-center">
          <div
            :key="i"
            v-for="(discussion, i) in discussions"
            class="col-xs-12 col-md-8 discussion-root-box q-mb-md"
            :class="[$q.dark.isActive ? 'border-dark' : 'border-light']"
          >
            <q-item>
              <q-item-section avatar top>
                <q-avatar color="primary" text-color="white">
                  <img :src="generatePictureSource(discussion.userPictureBase64String)" />
                </q-avatar>
              </q-item-section>
              <q-item-section>
                <q-item-label>{{ discussion.submittedBy }}</q-item-label>
                <q-item-label caption style="max-width: 85%;">{{discussion.content}}</q-item-label>
                <div
                  v-for="(image, i) in discussion.images"
                  :key="i"
                  style="width: 100px; height: 100px;"
                >
                  <img
                    style="border: 1px solid rgba(0, 0, 0, 0.4); border-radius: 10px; height: 100%; width: 100%;"
                    :src="generatePictureSource(image)"
                  />
                </div>
              </q-item-section>
              <q-item-section side>
                <q-item-label
                  style="font-size: 10px;"
                >{{ formatDistanceToNow(new Date(discussion.submittedAt)) }} ago</q-item-label>
              </q-item-section>
            </q-item>
            <div class="q-mt-sm">
              <q-btn flat size="xs" @click="showReplies(discussion.id)">Show replies</q-btn>
            </div>
          </div>
        </div>
      </div>
    </div>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="newDiscussionDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar
          :class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
          class="text-white dialog-toolbar"
        >
          <span>Create new discussion</span>
          <q-space />
          <q-btn
            :ripple="false"
            dense
            size="sm"
            color="white"
            flat
            round
            icon="mdi-close-circle"
            @click="resetNewDiscussionDialog"
          />
        </q-toolbar>
        <q-card-section class="q-gutter-sm">
          <q-editor
            ref="editor_ref"
            :toolbar="[]"
            :key="rerenderEditorKey"
            :content-style="{ 
              [$v.newDiscussion.content.$invalid && $v.newDiscussion.content.$dirty && 'border']: '1px solid #C10015', 
              'background-color': newDiscussion.backgroundColor,
              'color': newDiscussion.textColor
            }"
            v-model="newDiscussion.content"
            min-height="5rem"
            @input="$v.newDiscussion.content.$touch()"
            @paste.native="evt => pasteCapture(evt)"
          />
          <div
            v-if="$v.newDiscussion.content.$invalid && $v.newDiscussion.content.$dirty"
            class="error-text q-pl-sm"
          >This field is required!</div>
          <div
            v-else
            class="q-pl-sm"
            :class="[$q.dark.isActive ? 'hint-text-dark' : 'hint-text']"
          >Discussion contents</div>
          <q-input
            label="Background color"
            dense
            class="q-py-sm"
            outlined
            v-model="newDiscussion.backgroundColor"
            readonly
          >
            <template v-slot:append>
              <q-icon :style="{ color: newDiscussion.backgroundColor }" name="colorize">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-color
                    no-header
                    class="container-border"
                    v-model="newDiscussion.backgroundColor"
                  />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-input label="Text color" dense outlined v-model="newDiscussion.textColor" readonly>
            <template v-slot:append>
              <q-icon :style="{ color: newDiscussion.textColor }" name="colorize">
                <q-popup-proxy transition-show="scale" transition-hide="scale">
                  <q-color no-header class="container-border" v-model="newDiscussion.textColor" />
                </q-popup-proxy>
              </q-icon>
            </template>
          </q-input>
          <q-file
            ref="filePicker"
            dense
            multiple
            outlined
            v-model="newDiscussion.attachments"
            class="q-pr-sm q-pt-sm"
            clearable
            label="Attachments"
          >
            <template v-slot:append>
              <q-icon name="mdi-paperclip" />
            </template>
          </q-file>
          <q-file
            ref="filePicker"
            dense
            multiple
            outlined
            v-model="newDiscussion.images"
            class="q-pr-sm q-pt-sm"
            clearable
            accept="image/*"
            :max-files="5"
            label="Images (Maximum 5)"
          >
            <template v-slot:append>
              <q-icon name="mdi-image" />
            </template>
          </q-file>
        </q-card-section>
        <q-card-actions class="q-pl-md q-gutter-sm">
          <div
            v-for="(image, i) in imageBase64Strings"
            :key="i"
            style="width: 100px; height: 100px;"
          >
            <img
              style="border: 1px solid rgba(0, 0, 0, 0.4); border-radius: 10px; height: 100%; width: 100%;"
              :src="image"
            />
          </div>
        </q-card-actions>
        <q-card-actions class="justify-end q-pt-none q-mb-sm">
          <q-btn
            :disabled="$v.newDiscussion.$invalid"
            class="q-mr-sm"
            color="primary"
            size="sm"
            @click="createNewDiscussion"
          >Post</q-btn>
        </q-card-actions>
      </q-card>
    </q-dialog>
    <q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="repliesDialog" persistent>
      <q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
        <q-toolbar
          :style="`border-bottom: 1px solid ${$q.dark.isActive ? 'rgba(255, 255, 255, 0.6)' : 'rgba(0, 0, 0, 0.12)'};`"
        >
          <span style="font-size: 18px;">Replies</span>
          <q-space />
          <q-btn
            @click="closeDialog"
            :ripple="false"
            dense
            size="sm"
            flat
            round
            icon="mdi-close-thick"
          />
        </q-toolbar>
        <q-card-section>
          <q-list
            v-if="true"
            separator
            class="list-border"
            :class="$q.dark.isActive ? 'border-dark' : 'border-light'"
          >
            <q-item class="no-select" :key="i" v-for="(comment, i) in 5">{{ comment }}</q-item>
          </q-list>
          <q-list
            dense
            v-else
            class="list-border"
            :class="$q.dark.isActive ? 'border-dark' : 'border-light'"
          >
            <q-item class="no-select text-center">
              <q-item-section>No replies!</q-item-section>
            </q-item>
          </q-list>
        </q-card-section>
      </q-card>
    </q-dialog>
    <q-page-sticky
      position="bottom-right"
      :offset="[18, 18]"
      v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageDiscussion, Privileges.CanCreateNewDiscussion) 
      && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
    >
      <q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
        <q-fab-action
          @click="newDiscussionDialog = true"
          icon="mdi-message-plus"
          :color="!$q.dark.isActive ? 'primary' : 'grey-8'"
          label="New discussion"
        />
      </q-fab>
    </q-page-sticky>
  </div>
</template>

<script>
import UserMixin from "../../mixins/userMixin";
import CourseService from "../../services/api/course";
import { required, minLength, numeric } from "vuelidate/lib/validators";
import { generatePictureSource } from "../../helpers/helpers";
import { formatDistanceToNow } from "date-fns";

export default {
  name: "CourseDetailsDiscussion",
  mixins: [UserMixin],
  data() {
    return {
      repliesDialog: false,
      discussions: [],
      creatingDiscussion: false,
      rerenderEditorKey: 0,
      newDiscussionDialog: false,
      imageBase64Strings: [],
      dialogStyle: { width: "55%", "max-width": "90vw" },
      newDiscussion: {
        content: "",
        attachments: null,
        images: null,
        backgroundColor: null,
        textColor: null
      },
      dialogStyle: {
        width: "70%",
        "max-width": "90vw"
      }
    };
  },
  validations: {
    newDiscussion: {
      content: {
        required,
        minLength: minLength(4)
      }
    }
  },
  created() {
    this.courseId = this.$route.params.id;
    this.getDiscussions();
  },
  watch: {
    "newDiscussion.backgroundColor": {
      immediate: false,
      deep: false,
      handler(val) {
        this.rerenderEditorKey++;
      }
    },
    "newDiscussion.images": {
      immediate: false,
      deep: true,
      handler(val) {
        if (val == null) {
          this.imageBase64Strings = [];
          return;
        }
        this.imageBase64Strings = [];
        val.forEach(x => {
          let reader = new FileReader();
          reader.readAsDataURL(x);
          reader.onload = () => {
            this.imageBase64Strings.push(reader.result);
          };
        });
      }
    }
  },
  methods: {
    formatDistanceToNow,
    generatePictureSource,
    showReplies(discussionId) {
      this.repliesDialog = true;
		},
		closeDialog() {
			this.repliesDialog = false;
		},
    getDiscussions() {
      CourseService.getDiscussions(this.courseId, true).then(({ data }) => {
        this.discussions = data;
      });
    },
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
    resetNewDiscussionDialog() {
      this.newDiscussionDialog = false;

      this.newDiscussion = {
        content: "",
        attachments: null,
        images: null,
        backgroundColor: null,
        textColor: null
      };

      this.$v.$reset();
    },
    createNewDiscussion() {
      let formData = new FormData();
      let newDiscussion = this.newDiscussion;
      this.creatingDiscussion = true;

      formData.append("submittedById", this.user.id);
      formData.append("courseId", this.courseId);
      formData.append("backgroundColor", newDiscussion.backgroundColor);
      formData.append("textColor", newDiscussion.textColor);
      formData.append("body", newDiscussion.content);

      if (newDiscussion.attachments != null) {
        newDiscussion.attachments.forEach(file =>
          formData.append("attachments", file)
        );
      }

      if (newDiscussion.images != null) {
        newDiscussion.images.forEach(file => formData.append("images", file));
      }

      CourseService.createNewDiscussion(formData, this.courseId).finally(() => {
        this.creatingDiscussion = false;
        this.resetNewDiscussionDialog();
      });
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
.discussion-root-box
	position: relative
	border-radius: 8px
	padding: 15px
</style>
