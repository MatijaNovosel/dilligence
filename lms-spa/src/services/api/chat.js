import axios from 'axios';

export default {
  async getChatDetails(id) {
    return await axios.get("Chat/" + id);
  }
}