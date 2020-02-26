import axios from 'axios';

export default {
  async getZaposlenik(id) {
    return await axios.get(`Zaposlenik/${id}`);
  },

  async getZaposlenici(name, surname, odjel, vrstaZaposljenja, skip, take) {
    return await axios.get('Zaposlenik', {
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