<template>
	<div>
		<q-card flat bordered>
			<q-toolbar
				:class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
				class="text-center justify-center items-center text-white dialog-toolbar"
			>
				<span class="caption text-white">{{ content.title }}</span>
				<q-space />
				<div class="q-mr-sm">
					<q-btn
						:ripple="false"
						dense
						size="sm"
						color="white"
						flat
						round
						icon="mdi-plus-box-multiple"
						@click="addFileDialog = !addFileDialog"
						v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageCourseFiles, Privileges.CanUploadCourseFiles)
            && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)
            && content.title != 'Notification files'"
					>
						<q-tooltip>{{ $i18n.t('uploadFiles') }}</q-tooltip>
					</q-btn>
					<q-btn
						:ripple="false"
						dense
						size="sm"
						color="white"
						flat
						round
						class="q-ml-sm"
						v-if="content.files.length > 1"
						:icon="downloadMultiple ? 'mdi-lock-open-variant' : 'mdi-lock'"
						@click="changeDownloadToMultiple"
					>
						<q-tooltip>{{ $i18n.t('downloadMultiple') }}</q-tooltip>
					</q-btn>
					<q-btn
						:ripple="false"
						dense
						size="sm"
						color="white"
						flat
						round
						class="q-ml-sm"
						icon="mdi-close-circle"
						v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageCourseFiles, Privileges.CanDeleteCourseFiles) 
            && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)
            && content.title != 'Notification files'"
						@click="$deleteSidebar(content.id)"
					>
						<q-tooltip>Delete cabinet</q-tooltip>
					</q-btn>
				</div>
			</q-toolbar>
			<q-list separator v-if="content != null">
				<q-separator />
				<template v-if="content.files.length != 0">
					<q-item v-for="file in content.files" :key="file.id + file.name">
						<q-item-section avatar>
							<q-icon size="xs" :name="fileIcon(file.name.slice(file.name.lastIndexOf('.') + 1))" />
						</q-item-section>
						<q-separator vertical />
						<q-item-section class="q-pl-md">
							<q-item-label>{{ file.name }}</q-item-label>
							<q-item-label caption>{{ file.contentType }}</q-item-label>
						</q-item-section>
						<q-item-section side>
							<template v-if="!downloadMultiple">
								<q-btn-group outline>
									<q-btn
										size="sm"
										flat
										round
										icon="mdi-download"
										@click="download(file.contentType, file.data, file.name)"
									/>
									<q-btn
										icon="mdi-minus-circle"
										flat
										round
										size="sm"
										@click="deleteFile(file.id)"
										v-if="hasCoursePrivileges(courseId, Privileges.CanManageCourse, Privileges.CanManageCourseFiles, Privileges.CanDeleteCourseFiles) 
                    && hasCoursePrivileges(courseId, Privileges.IsInvolvedWithCourse)
                    && content.title != 'Notification files'"
									/>
								</q-btn-group>
							</template>
							<q-checkbox
								class="q-pr-xs"
								v-else
								v-model="downloadMultipleSelection"
								dense
								:val="file.id"
								color="primary"
								size="sm"
							></q-checkbox>
						</q-item-section>
					</q-item>
				</template>
				<q-item v-else>
					<q-item-section class="q-pl-md text-center">No files found!</q-item-section>
				</q-item>
				<q-item class="justify-center" v-show="downloadMultiple">
					<q-space />
					<q-btn
						size="xs"
						class="q-py-xs"
						color="primary"
						icon="mdi-download"
						:loading="downloadingMultiple"
						@click="downloadMultipleFiles"
					/>
					<q-space />
					<q-btn
						:ripple="false"
						dense
						size="sm"
						flat
						round
						class="select-all"
						v-if="downloadMultiple"
						icon="mdi-check-box-multiple-outline"
						@click="selectAllFiles"
					>
						<q-tooltip>{{ $i18n.t('selectAll') }}</q-tooltip>
					</q-btn>
				</q-item>
			</q-list>
		</q-card>
		<q-dialog v-model="addFileDialog" persistent>
			<q-card class="upload-dialog">
				<q-toolbar
					:class="[ $q.dark.isActive ? 'dark-dialog-background' : 'bg-primary']"
					class="text-white dialog-toolbar"
				>
					<span>Upload files</span>
					<q-space />
					<q-btn
						:ripple="false"
						dense
						size="sm"
						color="white"
						flat
						round
						icon="mdi-close-circle"
						@click="resetDialog"
					/>
				</q-toolbar>
				<q-card-section>
					<q-file dense multiple outlined v-model="files" clearable label="Files">
						<template v-slot:prepend>
							<q-icon name="mdi-paperclip" />
						</template>
					</q-file>
				</q-card-section>
				<q-card-actions class="q-pt-none">
					<q-space />
					<q-btn
						:ripple="false"
						dense
						:loading="itemUploading"
						:disabled="itemUploading"
						size="sm"
						color="primary"
						@click="upload"
						class="q-mr-sm q-mb-sm"
					>{{ $i18n.t('upload') }}</q-btn>
				</q-card-actions>
			</q-card>
		</q-dialog>
	</div>
</template>

<script>
import { download, fileIcon } from "../helpers/helpers";
import FileService from "../services/api/file";
import UserMixin from "../mixins/userMixin";

export default {
	name: "file-cabinet",
	props: ["content", "headerColor", "courseId"],
	mixins: [UserMixin],
	data() {
		return {
			downloadMultipleSelection: [],
			downloadingMultiple: false,
			downloadMultiple: false,
			addFileDialog: false,
			files: null,
			itemUploading: false,
			closeAfterCompletion: false
		};
	},
	methods: {
		download,
		fileIcon,
		selectAllFiles() {
			this.content.files.forEach(file => {
				if (!this.downloadMultipleSelection.includes(file.id)) {
					this.downloadMultipleSelection.push(file.id);
				}
			});
		},
		deleteFile(fileId) {
			FileService.deleteFile(fileId).then(() => {
				this.content.files = this.content.files.filter(
					file => file.id != fileId
				);
			});
		},
		downloadMultipleFiles() {
			this.downloadingMultiple = true;
			FileService.downloadMultiple(this.downloadMultipleSelection)
				.then(({ data }) => {
					this.download(data.contentType, data.data, data.name);
				})
				.finally(() => {
					this.downloadingMultiple = false;
				});
		},
		changeDownloadToMultiple() {
			[this.downloadMultiple, this.downloadMultipleSelection] = [
				!this.downloadMultiple,
				[]
			];
		},
		resetDialog() {
			this.addFileDialog = false;
			this.files = null;
		},
		handleDownload(item) {
			item.downloading = true;
			this.download(item.contentType, item.data, item.name);
			setTimeout(() => (item.downloading = false), 500);
		},
		upload() {
			this.itemUploading = true;
			var formData = new FormData();
			this.files.forEach(x => formData.append("files", x));
			FileService.uploadSidebar(formData, this.content.id).finally(() => {
				this.$emit("doneUploading");
				this.itemUploading = false;
				if (this.closeAfterCompletion) {
					this.addFileDialog = false;
				}
				this.files = null;
				this.resetDialog();
			});
		},
		$deleteSidebar(sidebarId) {
			this.$emit("delete", sidebarId);
		}
	}
};
</script>

<style lang="sass">
.dialog-toolbar
  min-height: 30px
.upload-dialog
  width: 80vh
  max-width: 80vw
.q-item__section--main ~ .q-item__section--side
  padding-left: 0px
.select-all
  position: absolute
  right: 15px
  top: 12px
</style>