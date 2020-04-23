import axios from 'axios';

export default {
  async getNotifications(userId) {
    return await axios.get('Notification', {
      params: {
        userId
      }
    });
  }
}