<template>
	<div>
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
						:content-style="{ [$v.newDiscussion.content.$invalid && $v.newDiscussion.content.$dirty && 'border']: '1px solid #C10015', 'background-color': newDiscussion.backgroundColor }"
						v-model="newDiscussion.content"
						min-height="5rem"
						@input="$v.newDiscussion.content.$touch()"
						@paste.native="evt => pasteCapture(evt)"
					/>
					<div
						v-if="$v.newDiscussion.content.$invalid && $v.newDiscussion.content.$dirty"
						class="error-text q-pl-sm"
					>This field is required!</div>
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
									<q-color no-header class="container-border" v-model="newDiscussion.backgroundColor" />
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
					<div v-for="(image, i) in imageBase64Strings" :key="i" style="width: 100px; height: 100px;">
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
import { required, minLength, numeric } from "vuelidate/lib/validators";

export default {
	name: "CourseDetailsDiscussion",
	mixins: [UserMixin],
	data() {
		return {
			newDiscussionDialog: false,
			imageBase64Strings: [],
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
	},
	watch: {
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
			this.$v.$reset();
		},
		createNewDiscussion() {
			console.log("Kurcina!");
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
