import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async getKorisnik(id) {
    return await axios.get(`${ROUTING.baseRoute}Korisnik/${id}`);
  }
}