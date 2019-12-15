import axios from 'axios';
import { RoutingInformation } from '@/constants/routingInformation';

export default {
  async getZaposlenik(id) {
    return await axios.get(`${RoutingInformation.baseRoute}Zaposlenik/${id}`);
  },

  async getZaposlenici(name, surname, odjel, vrstaZaposljenja, skip, take) {
    return await axios.get(`${RoutingInformation.baseRoute}Zaposlenik`, {
      params: {
        name,
        surname,
        odjel,
        vrstaZaposljenja,
        skip,
        take
      }
    });
  }
}