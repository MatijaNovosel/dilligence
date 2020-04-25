import { HubConnectionBuilder } from "@aspnet/signalr";

export default {
  data() {
    connection: null
  },
  methods: {
    /**
     * Connects to a socket that communicates with the backend, creating a connection instance over a secure HTTPS line.
     * @param {string} hubName - Name of the backend communication hub.
     */
    startConnection(hubName) {
      this.connection = new HubConnectionBuilder().withUrl(`https://localhost:5001/${hubName}`).build();
      this.connection.start();
    }
  },
  beforeDestroy() {
    this.connection.stop();
  }
}