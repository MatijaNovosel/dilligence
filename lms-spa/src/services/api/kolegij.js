import axios from 'axios';

export default {
  async get(parameters) {
    return await axios.get('Kolegij', {
      params: {
        smjerIDs: parameters.smjerIds,
        name: parameters.name,
        minECTS: parameters.minEcts,
        maxECTS: parameters.maxEcts,
        isvu: parameters.isvu
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