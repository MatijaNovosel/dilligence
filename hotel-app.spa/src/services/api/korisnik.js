import axios from 'axios';

export default {
  async getKorisnik(id) {
    return await axios.get(`Korisnik/${id}`);
  }
}