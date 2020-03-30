import axios from 'axios';

export default {
  async get(userId, smjerIds, name, subscribed, nonSubscribed) {
    return await axios.get('Kolegij', {
      params: {
        userId,
        smjerIds,
        name,
        subscribed,
        nonSubscribed
      }
    });
  },
  async getKolegij(id) {
    return await axios.get(`Kolegij/${id}`);
  },
  async getKolegijSidebar(id) {
    return await axios.get(`Kolegij/sidebar/${id}`);
  }
}