import axios from 'axios';
import { Routing } from '@/constants/Routing';

export default {
  async getZaposlenik(id) {
    return await axios.get(`${Routing.baseRoute}Zaposlenik/${id}`);
  },

  async getZaposlenici(name, surname, odjel, vrstaZaposljenja, skip, take) {
    return await axios.get(`${Routing.baseRoute}Zaposlenik`, {
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