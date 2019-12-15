import axios from 'axios';
import { RoutingInformation } from '@/constants/routingInformation';

export default {
  async getKolegij(id) {
    return await axios.get(`${RoutingInformation.baseRoute}Kolegij/${id}`);
  },

  async getKolegiji(smjerIDs, name, minECTS, maxECTS, isvu, skip, take) {
    return await axios.get(`${RoutingInformation.baseRoute}Kolegij`, {
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
  }
}