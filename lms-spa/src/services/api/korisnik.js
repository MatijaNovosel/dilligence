import axios from 'axios';

export default {
  async searchKorisnik(name) {
    return await axios.get("Korisnik", {
      params: {
        name
      }
    });
  }
}