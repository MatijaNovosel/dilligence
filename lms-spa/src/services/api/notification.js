import axios from 'axios';

export default {
  async getNotifications(userId) {
    return await axios.get('Notification/user/' + userId);
  },
  async getTotalNotifications(userId) {
    return await axios.get('Notification/user-total/' + userId);
  }
}