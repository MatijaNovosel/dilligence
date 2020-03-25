import axios from 'axios';

export default {
  async getChatDetails(id) {
    return await axios.get("Chat/" + id);
  },
  async sendMessage(payload) {
    return await axios.post("Chat", payload);
  },
  async getAvailableUsers(id) {
    return await axios.get("Chat/available/" + id);
  },
  async createNewChat(payload) {
    return await axios.post("Chat/new", payload);
  },
  async deleteMessage(id) {
    return await axios.delete("Chat/" + id);
  }
}