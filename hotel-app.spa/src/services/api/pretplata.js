import axios from 'axios';
import { Routing } from '@/constants/Routing';

export default {
  async getPretplata(studentId) {
    return await axios.get(`${Routing.baseRoute}Pretplata/${studentId}`);
  }
}