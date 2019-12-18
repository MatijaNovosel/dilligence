import axios from 'axios';
import { Routing } from '@/constants/Routing';

export default {
  async getKolegij(id) {
    return await axios.get(`${Routing.baseRoute}Kolegij/${id}`);
  },

  async getKolegiji(smjerIDs, name, minECTS, maxECTS, isvu, skip, take) {
    return await axios.get(`${Routing.baseRoute}Kolegij`, {
      params: {
        smjerIDs,
        name,
        minECTS,
        maxECTS,
        isvu,
        skip,
        take
      }
    });
  },

  async getKolegijNews(kolegijId, skip, take) {
    return await axios.get(`${Routing.baseRoute}Kolegij/Vijesti/${kolegijId}`, {
      params: {
        kolegijId,
        skip,
        take
      }
    });
  }
}