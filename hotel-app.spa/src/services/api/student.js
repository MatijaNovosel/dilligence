import axios from 'axios';
import { Routing } from '@/constants/Routing';

export default {
  async getStudent(id) {
    return await axios.get(`${Routing.baseRoute}Student/${id}`);
  }
}