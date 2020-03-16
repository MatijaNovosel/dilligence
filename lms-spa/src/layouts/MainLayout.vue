<template>
	<q-layout view="hHh lpR fff">
		<div v-if="$router.currentRoute.path != '/login'">
			<Navbar @drawerState="drawer = !drawer" />
			<Drawer @avatarClicked="editPictureDialog = true" :drawerTrigger="drawer" />
		</div>
		<q-page-container :class="{' drawer-bg': $router.currentRoute.path == '/login' }">
			<router-view />
		</q-page-container>
		<q-dialog v-model="editPictureDialog" persistent>
			<q-card class="upload-dialog">
				<q-toolbar class="bg-primary text-white dialog-toolbar">
					<span>Change profile picture</span>
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
				<q-card-section class="text-center q-pb-none">
					<q-img src="../assets/default-user.jpg" class="border-box-image"></q-img>
				</q-card-section>
				<q-card-section>
					<q-file
						accept=".jpg, .pdf, image/*"
						dense
						outlined
						v-model="picture"
						clearable
						label="Upload picture"
					>
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
					>Upload</q-btn>
				</q-card-actions>
			</q-card>
		</q-dialog>
	</q-layout>
</template>

<script>
import Navbar from "../components/Navbar";
import Drawer from "../components/Drawer";

export default {
	name: "MainLayout",
	components: {
		Navbar,
		Drawer
	},
	data() {
		return {
			editPictureDialog: false,
			drawer: null,
			picture: null
		};
	},
	methods: {
		resetDialog() {
			this.editPictureDialog = false;
			this.picture = null;
		}
	}
};
</script>

<style lang="sass">
.navbar-img
  width: 40px
  height: 40px
.drawer-bg
  background-image: url("../assets/nav-bg.svg") !important
  background-position: center center
  background-size: cover
  overflow: hidden !important
.q-item__section--avatar
  cursor: pointer
.border-box-image
  width: 30%
  height: 30%
  border: 1px solid rgba(0, 0, 0, 0.12)
  border-radius: 10px
</style>