<template>
	<div>
		<div v-if="loading" class="row justify-center q-mt-md">
			<div class="col-8">
				<q-skeleton class="q-mx-sm" width="100%" height="150px" square />
			</div>
		</div>
		<div v-else class="row q-col-gutter-sm justify-center q-mt-md">
			<template v-if="discussions.length != 0">
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
							<div v-for="(image, i) in discussion.images" :key="i" style="width: 100px; height: 100px;">
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
						<q-btn
							flat
							size="xs"
							v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageDiscussion, Privileges.CanDeleteDiscussion) || discussion.submittedById == user.id"
							@click="deleteDiscussion(discussion.id)"
						>Delete</q-btn>
					</div>
				</div>
			</template>
			<div v-else class="col-12 text-center">
				<h6>
					<q-icon class="q-mr-sm" name="mdi-comment-question" size="md" />No discussions found!
				</h6>
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
						:content-style="{ [$v.newDiscussion.content.$invalid && $v.newDiscussion.content.$dirty && 'border']: '1px solid #C10015' }"
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
				</q-card-section>
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
					<q-btn @click="closeDialog" :ripple="false" dense size="sm" flat round icon="mdi-close-thick" />
				</q-toolbar>
				<q-card-section>
					<div
						v-if="replies.length != 0"
						:class="$q.dark.isActive ? 'border-box-dark' : 'border-box-light'"
						:style="{ 'background-color': $q.dark.isActive ? '#2b2b2b' : '#f9f8f8' }"
					>
						<q-btn
							@click="scrollToBottom"
							style="z-index: 999;"
							size="sm"
							round
							color="primary"
							icon="mdi-menu-down"
							class="absolute-bottom-left"
							v-show="$refs.replies != undefined && $refs.replies.scrollSize > $refs.replies.containerHeight && $refs.replies.scrollPercentage < 0.8"
						/>
						<q-scroll-area
							ref="replies"
							:thumb-style="thumbStyle"
							:bar-style="barStyle"
							class="q-px-lg q-py-md"
							style="height: 400px;"
						>
							<q-chat-message
								:name="reply.submittedById == user.id ? 'Me' : reply.submittedBy"
								:avatar="generatePictureSource(reply.userPicture)"
								:text="[ reply.content ]"
								:sent="reply.submittedById == user.id"
								:style="reply.submittedById == user.id ? 'text-align: right;' : 'text-align: left;'"
								:stamp="format(new Date(reply.submittedAt), 'dd.mm.yyyy. hh:MM')"
								v-for="(reply, i) in replies"
								:bg-color="reply.submittedById == user.id ? $q.dark.isActive ? 'dark' : 'blue-5' : $q.dark.isActive ? 'grey-9' : 'blue-2'"
								:text-color="$q.dark.isActive ? 'white' : 'black'"
								:key="i"
							/>
						</q-scroll-area>
					</div>
					<q-list
						dense
						v-else
						class="list-border"
						:class="$q.dark.isActive ? 'border-dark' : 'border-light'"
					>
						<q-item class="no-select text-center">
							<q-item-section class="q-py-md">No replies!</q-item-section>
						</q-item>
					</q-list>
				</q-card-section>
				<q-card-section>
					<q-editor
						ref="reply_editor_ref"
						:toolbar="[]"
						v-model="newReply"
						min-height="5rem"
						@paste.native="evt => pasteCapture(evt)"
					/>
				</q-card-section>
				<q-card-actions class="justify-end q-pt-none q-pb-md">
					<q-btn
						class="q-mt-xs q-mr-sm"
						size="sm"
						@click="sendReply()"
						:color="!$q.dark.isActive ? 'primary' : 'grey-8'"
					>Reply</q-btn>
				</q-card-actions>
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
import {
	generatePictureSource,
	mustNotBeEmptyHtml,
	clearedHtmlMustBeAtLeastNCharacters
} from "../../helpers/helpers";
import { formatDistanceToNow, format } from "date-fns";
import Vue from "vue";

export default {
	name: "CourseDetailsDiscussion",
	mixins: [UserMixin],
	data() {
		return {
			loading: false,
			repliesDialog: false,
			discussions: [],
			activeDiscussionId: null,
			creatingDiscussion: false,
			newDiscussionDialog: false,
			dialogStyle: { width: "55%", "max-width": "90vw" },
			replies: [],
			newDiscussion: {
				content: ""
			},
			dialogStyle: {
				width: "70%",
				"max-width": "90vw"
			},
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
			newReply: ""
		};
	},
	validations: {
		newDiscussion: {
			content: {
				mustNotBeEmptyHtml,
				clearedHtmlMustBeAtLeastNCharacters
			}
		}
	},
	created() {
		this.courseId = this.$route.params.id;
		this.getDiscussions();
	},
	methods: {
		format,
		formatDistanceToNow,
		generatePictureSource,
		getReplies() {
			CourseService.getReplies(this.activeDiscussionId).then(({ data }) => {
				this.replies = data;
				this.scrollToBottom();
			});
		},
		showReplies(discussionId) {
			this.activeDiscussionId = discussionId;
			this.getReplies();
			this.repliesDialog = true;
		},
		closeDialog() {
			this.repliesDialog = false;
		},
		getDiscussions() {
			this.loading = true;
			CourseService.getDiscussions(this.courseId, true)
				.then(({ data }) => {
					this.discussions = data;
				})
				.finally(() => {
					this.loading = false;
				});
		},
		scrollToBottom() {
			Vue.nextTick(() => {
				/*
        
          Component is rendered independently of the chat messages, so relying on the reference is a bad idea, 
          however the scroll size of the component stays consistently the same
        
        */
				this.$refs.replies.setScrollPosition(this.replies.length * 68, 0);
			});
		},
		deleteDiscussion(id) {
			CourseService.deleteDiscussion(id).then(() => {
				this.getDiscussions();
			});
		},
		sendReply() {
			CourseService.sendReply({
				discussionId: this.activeDiscussionId,
				submittedById: this.user.id,
				content: this.newReply.replace(/<\/?("[^"]*"|'[^']*'|[^>])*(>|$)/g, "")
			}).then(() => {
				this.newReply = "";
				this.getReplies();
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
				content: ""
			};

			this.$v.$reset();
		},
		createNewDiscussion() {
			let formData = new FormData();
			let newDiscussion = this.newDiscussion;
			this.creatingDiscussion = true;

			formData.append("submittedById", this.user.id);
			formData.append("courseId", this.courseId);
			formData.append(
				"body",
				newDiscussion.content.replace(/<\/?("[^"]*"|'[^']*'|[^>])*(>|$)/g, "")
			);

			CourseService.createNewDiscussion(formData, this.courseId).finally(() => {
				this.creatingDiscussion = false;
				this.resetNewDiscussionDialog();
				this.getDiscussions();
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
