<template>
  <div>
    <q-drawer
      :width="250"
      v-model="drawerOpen"
      show-if-above
      :content-class="`drawer-bg${$q.dark.isActive ? '-dark' : ''} drawer-base`"
    >
      <q-list dense>
        <q-item class="q-my-md">
          <q-item-section avatar>
            <q-avatar
              size="45px"
              style="cursor: pointer;"
              @click="$router.push({ name: 'profile', params: { id: user.id } })"
            >
              <img
                style="border: 1px solid rgba(0, 0, 0, 0.4)"
                :src="generateUserPictureSource(user.picture)"
              />
            </q-avatar>
          </q-item-section>
          <q-item-section>
            <q-item-label lines="1">{{ `${user.name} ${user.surname}` }}</q-item-label>
            <q-item-label caption lines="2">
              <span class="text-weight-bold">{{ $i18n.t("username") }}:</span>
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
          :label="$i18n.t('drawerLinks.' + link.text)"
          :key="link.text"
        >
          <q-list :key="i" dense v-for="(sublink, i) in link.sublinks">
            <q-item @click="$router.push(sublink.route)" class="text-body2" clickable v-ripple>
              <span class="q-pl-md">{{ $i18n.t('drawerLinks.' + sublink.text) }}</span>
            </q-item>
          </q-list>
        </q-expansion-item>
      </q-list>
    </q-drawer>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import { generateUserPictureSource } from "../helpers/helpers";

export default {
  name: "drawer",
  props: ["drawerTrigger"],
  data() {
    return {
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
  watch: {
    drawerTrigger() {
      this.drawerOpen = !this.drawerOpen;
    }
  },
  computed: {
    ...mapGetters(["user"])
  },
  methods: {
    generateUserPictureSource
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