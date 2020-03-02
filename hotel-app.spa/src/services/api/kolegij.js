import axios from 'axios';

export default {
  async getKolegij(id) {
    return await axios.get(`Kolegij/${id}`);
  },
  async getKolegijSidebar(id) {
    return await axios.get(`Kolegij/sidebar/${id}`);
  }
}