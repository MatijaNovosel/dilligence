import { HubConnectionBuilder } from "@aspnet/signalr";

export default {
  data() {
    connection: null
  },
  methods: {
    startConnection(hubName) {
      this.connection = new HubConnectionBuilder().withUrl(`http://localhost:5000/${hubName}`).build();
      this.connection.start();
    }
  },
  beforeDestroy() {
    this.connection.stop();
  }
}