import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async getPretplata(studentId) {
    return await axios.get(`${ROUTING.baseRoute}Pretplata/${studentId}`);
  },

  async postPreplata(studentId, pretplataIDs) {
    return await axios.post(`${ROUTING.baseRoute}Pretplata`, {
      studentId,
      pretplataIDs
    });
  }
}