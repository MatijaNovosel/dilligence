import axios from 'axios';
import { Routing } from '@/constants/Routing';

export default {
  async getKorisnik(id) {
    return await axios.get(`${Routing.baseRoute}Korisnik/${id}`);
  }
}