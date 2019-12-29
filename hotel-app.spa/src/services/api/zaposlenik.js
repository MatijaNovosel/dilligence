import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async getZaposlenik(id) {
    return await axios.get(`${ROUTING.baseRoute}Zaposlenik/${id}`);
  },

  async getZaposlenici(name, surname, odjel, vrstaZaposljenja, skip, take) {
    return await axios.get(`${ROUTING.baseRoute}Zaposlenik`, {
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