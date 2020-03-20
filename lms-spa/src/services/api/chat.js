import axios from 'axios';

export default {
  async getChatDetails(id) {
    return await axios.get("Chat/" + id);
  },
  async sendMessage(payload) {
    return await axios.post("Chat", payload);
  }
}