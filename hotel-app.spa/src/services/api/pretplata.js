import axios from 'axios';
import { Routing } from '@/constants/Routing';

export default {
  async getPretplata(studentId) {
    return await axios.get(`${Routing.baseRoute}Pretplata/${studentId}`);
  },

  async postPreplata(studentId, pretplataIDs) {
    return await axios.post(`${Routing.baseRoute}Pretplata`, {
      studentId,
      pretplataIDs
    });
  }
}