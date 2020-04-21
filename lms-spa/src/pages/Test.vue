<template>
  <q-page>
    <template v-for="content in sidebarContents">
      <FileCabinet :key="content.id" :content="content"></FileCabinet>
    </template>
    <div class="row text-center justify-center q-my-md">
      <div class="col-3">
        <q-input v-model="notification" :label="$t('enterNewNotification')"></q-input>
      </div>
      <div class="col-1 flex flex-center">
        <q-btn size="md" dense color="primary" @click="sendNotification">{{ $t('send') }}</q-btn>
      </div>
    </div>
    <div class="row text-center justify-center full-width">
      <div class="col-3">
        <q-list bordered separator dense>
          <q-item clickable v-ripple v-for="vijest in vijesti" :key="vijest.id">
            <q-item-section>
              <q-item-label>{{ vijest.naslov }}</q-item-label>
              <q-item-label caption>{{ vijest.opis }}</q-item-label>
            </q-item-section>
          </q-item>
        </q-list>
      </div>
    </div>
  </q-page>
</template>

<script>
import FileCabinet from "../components/FileCabinet";
import CourseService from "../services/api/course";
import {
  HubConnectionBuilder,
  LogLevel,
  HttpTransportType
} from "@aspnet/signalr";
import apiConfig from "../api.config";

export default {
  name: "Test",
  components: {
    FileCabinet
  },
  created() {
    this.connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/vijesti-hub")
      .configureLogging(LogLevel.Information)
      .build();
    this.connection.on("EVENT", response => {
      this.vijesti = [...this.vijesti, response.payload];
    });
    CourseService.getCourseSidebar(147).then(({ data }) => {
      this.sidebarContents = data;
    });
    this.connection.start();
    this.getData();
  },
  methods: {
    getData() {
      this.$axios.get("Notification/147").then(({ data }) => {
        this.vijesti = data;
      });
    },
    sendNotification() {
      this.$axios.post("Notification", { naslov: this.notification });
    }
  },
  data() {
    return {
      connection: null,
      notification: null,
      vijesti: null,
      sidebarContents: null
    };
  },
  beforeDestroy() {
    this.connection.stop();
  }
};
</script>

<style scoped lang="sass">
.my-card
  width: 100%
  max-width: 350px
  margin: 5px
</style>