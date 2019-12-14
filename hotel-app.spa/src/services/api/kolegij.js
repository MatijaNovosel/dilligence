import axios from 'axios';
import { RoutingInformation } from '@/constants/routingInformation';

export default {
  async getKolegij(id) {
    return await axios.get(`${RoutingInformation.baseRoute}Kolegij/${id}`);
  },

  async getKolegiji(skip, take) {
    return await axios.get(`${RoutingInformation.baseRoute}Kolegij`, {
      params: {
        skip,
        take
      }
    });
  },

  async getKolegijBySmjerID(smjerIDs, skip, take) {
    return await axios.get(`${RoutingInformation.baseRoute}Kolegij/bySmjerID`, {
      params: {
        smjerIDs,
        skip,
        take
      }
    });
  }
}