import { HubConnectionBuilder } from "@aspnet/signalr";

export default {
  data() {
    connection: null
  },
  methods: {
    startConnection(hubName) {
      this.connection = new HubConnectionBuilder().withUrl(`https://localhost:5001/${hubName}`).build();
      this.connection.start();
    }
  },
  beforeDestroy() {
    this.connection.stop();
  }
}