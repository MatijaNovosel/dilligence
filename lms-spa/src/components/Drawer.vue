<template>
  <div>
    <q-drawer
      :width="250"
      v-model="drawerOpen"
      show-if-above
      :content-class="($q.dark.isActive ? 'drawer-bg-dark' : 'drawer-bg') + ' drawer-base'"
    >
      <q-list dense>
        <q-item class="q-my-md">
          <q-item-section avatar>
            <q-avatar size="40px" @click="editPictureDialog = true">
              <img src="../assets/default-user.jpg" />
            </q-avatar>
          </q-item-section>
          <q-item-section>
            <q-item-label lines="1">{{ `${user.name} ${user.surname}` }}</q-item-label>
            <q-item-label caption lines="2">
              <span class="text-weight-bold">{{ $t("username") }}:</span>
              {{ user.username }}
            </q-item-label>
          </q-item-section>
        </q-item>
        <q-separator />
        <q-expansion-item
          group="drawer"
          dense
          dense-toggle
          class="text-weight-regular"
          v-for="link in links"
          :label="$t('drawerLinks.' + link.text)"
          :key="link.text"
        >
          <q-list :key="i" dense v-for="(sublink, i) in link.sublinks">
            <q-item @click="redirect(sublink.route)" class="text-body2" clickable v-ripple>
              <span class="q-pl-md">{{ $t('drawerLinks.' + sublink.text) }}</span>
            </q-item>
          </q-list>
        </q-expansion-item>
      </q-list>
    </q-drawer>
    <q-dialog v-model="editPictureDialog" persistent>
      <q-card class="picture-dialog">
        <q-toolbar class="bg-primary text-white dialog-toolbar">
          <span>{{ $t('changeProfilePicture') }}</span>
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
            :label="$t('uploadPicture')"
          >
            <template v-slot:prepend>
              <q-icon name="mdi-paperclip" />
            </template>
          </q-file>
        </q-card-section>
        <q-card-actions class="q-pt-none">
          <q-space />
          <q-btn :ripple="false" dense size="sm" color="primary">{{ $t('upload') }}</q-btn>
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Drawer",
  props: ["drawerTrigger"],
  data() {
    return {
      editPictureDialog: false,
      picture: null,
      drawerOpen: false,
      links: [
        {
          icon: "mdi-bullhorn",
          text: "general",
          route: { name: "/" },
          sublinks: [
            {
              text: "home",
              route: { name: "home" }
            },
            {
              text: "employees",
              route: { name: "employees" }
            },
            {
              text: "chats",
              route: { name: "chat" }
            }
          ]
        },
        {
          icon: "mdi-file-document",
          text: "courses",
          route: { name: "courses" },
          sublinks: [
            {
              text: "availableSubjects",
              route: { name: "courses" }
            }
          ]
        },
        {
          icon: "mdi-test-tube",
          text: "exams",
          route: { name: "exams" },
          sublinks: [
            {
              text: "availableExams",
              route: { name: "exams" }
            }
          ]
        }
      ]
    };
  },
  methods: {
    redirect(route) {
      if (this.$router.currentRoute.name === route.name) {
        return;
      }
      this.$router.push(route);
    },
    resetDialog() {
      this.editPictureDialog = false;
      this.picture = null;
    }
  },
  watch: {
    drawerTrigger() {
      this.drawerOpen = !this.drawerOpen;
    }
  },
  computed: {
    ...mapGetters(["user"])
  }
};
</script>

<style lang="sass">
.q-list--dense > .q-item, .q-item--dense
  min-height: 19px
.drawer-base
  background-position: center center
  background-size: cover
  overflow: hidden
.drawer-bg
  background-image: url("../assets/nav-bg.svg")
.drawer-bg-dark
  background-image: url("../assets/nav-bg-dark.png")
</style>