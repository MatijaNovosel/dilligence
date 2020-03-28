import axios from 'axios';

export default {
  async get(smjerIds, name, minEcts, maxEcts, isvu) {
    return await axios.get('Kolegij', {
      params: {
        smjerIds,
        name,
        minEcts,
        maxEcts,
        isvu
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