<template>
	<div>
		<div class="row q-col-gutter-md">
			<div v-if="loading" class="col-12 text-center q-mt-lg">
				<q-spinner size="3em" />
			</div>
			<div v-else class="col-12" :key="content.id" v-for="content in sidebarContents">
				<file-cabinet
					@delete="deleteSidebar"
					@doneUploading="getCourseFiles"
					:courseId="courseId"
					:content="content"
				/>
			</div>
		</div>
		<q-dialog :maximized="$q.screen.xs || $q.screen.sm" v-model="newSidebarDialog" persistent>
			<q-card :style="$q.screen.xs || $q.screen.sm || dialogStyle">
				<q-toolbar
					:class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
					class="text-white dialog-toolbar"
				>
					<span>Create new cabinet</span>
					<q-space />
					<q-btn
						:ripple="false"
						dense
						size="sm"
						color="white"
						flat
						round
						icon="mdi-close-circle"
						@click="reset"
					/>
				</q-toolbar>
				<q-card-section>
					<q-input
						label="Title"
						outlined
						dense
						hide-bottom-space
						no-error-icon
						:error="$v.newSidebar.title.$invalid && $v.newSidebar.title.$dirty"
						@input="$v.newSidebar.title.$touch()"
						v-model="newSidebar.title"
					/>
				</q-card-section>
				<q-card-actions class="q-pt-none">
					<q-space />
					<q-btn
						class="q-mr-sm"
						:ripple="false"
						:disabled="$v.newSidebar.$invalid"
						dense
						size="sm"
						color="primary"
						@click="createNewSidebar"
					>Create</q-btn>
				</q-card-actions>
			</q-card>
		</q-dialog>
		<q-page-sticky
			position="bottom-right"
			:offset="[18, 18]"
			v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageCourseFiles, Privileges.CanUploadCourseFiles)
            && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)"
		>
			<q-fab direction="left" :color="!$q.dark.isActive ? 'primary' : 'grey-8'" fab icon="add">
				<q-fab-action
					icon="mdi-file-plus"
					:color="!$q.dark.isActive ? 'primary' : 'grey-8'"
					label="New cabinet"
					@click="newSidebarDialog = true"
				/>
			</q-fab>
		</q-page-sticky>
	</div>
</template>

<script>
import CourseService from "../../services/api/course";
import FileCabinet from "../../components/file-cabinet";
import UserMixin from "../../mixins/userMixin";
import { required, minLength } from "vuelidate/lib/validators";

export default {
	name: "CourseDetailsFiles",
	mixins: [UserMixin],
	components: {
		"file-cabinet": FileCabinet
	},
	validations: {
		newSidebar: {
			title: {
				required,
				minLength: minLength(4)
			}
		}
	},
	created() {
		this.courseId = this.$route.params.id;
		this.getCourseFiles();
	},
	data() {
		return {
			courseId: null,
			sidebarContents: null,
			newSidebarDialog: false,
			loading: false,
			dialogStyle: {
				width: "50%",
				"max-width": "60vw"
			},
			newSidebar: {
				title: null
			}
		};
	},
	methods: {
		deleteSidebar(sidebarId) {
			CourseService.deleteSidebar(sidebarId, this.courseId).then(() => {
				this.getCourseFiles();
			});
		},
		createNewSidebar() {
			CourseService.createNewSidebar({
				title: this.newSidebar.title,
				courseId: this.courseId
			}).then(() => {
				this.getCourseFiles();
				this.$v.$reset();
				this.reset();
			});
		},
		getCourseFiles() {
			this.loading = true;
			CourseService.getCourseSidebar(this.courseId)
				.then(({ data }) => {
					this.sidebarContents = data;
				})
				.finally(() => {
					this.loading = false;
				});
		},
		reset() {
			this.newSidebar = {
				title: null
			};
			this.newSidebarDialog = false;
		}
	}
};
</script>

<style lang="sass">
.q-btn--fab .q-btn__wrapper
  padding: 10px
  min-height: 12px
  min-width: 12px
</style>