import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async getKolegij(id) {
    return await axios.get(`${ROUTING.baseRoute}Kolegij/${id}`);
  },
  async getKolegijSidebar(id) {
    return await axios.get(`${ROUTING.baseRoute}Kolegij/sidebar/${id}`);
  }
}