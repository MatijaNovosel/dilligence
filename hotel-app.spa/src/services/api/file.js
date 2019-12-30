import axios from 'axios';
import { ROUTING } from '@/constants/Routing';

export default {
  async upload(file) {
    return await axios.post(`${ROUTING.baseRoute}File/Upload`, file);
  }
}