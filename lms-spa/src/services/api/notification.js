import axios from 'axios';

export default {
  async getNotifications(userId) {
    return await axios.get('Notification/user/' + userId);
  },
  async getTotalNotifications(userId) {
    return await axios.get('Notification/user-total/' + userId);
  },
  async markNotificationAsSeen(notificationIds, userId) {
    return await axios.post('Notification/seen', {
      notificationIds, userId
    });
  },
  async createNotification() {
    return await axios.post('Notification', payload);
  }
}