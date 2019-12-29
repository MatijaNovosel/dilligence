import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async getStudent(id) {
    return await axios.get(`${ROUTING.baseRoute}Student/${id}`);
  }
}