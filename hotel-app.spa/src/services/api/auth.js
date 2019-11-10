import axios from 'axios';
import { RoutingInformation } from '@/constants/routingInformation';

export default {
  async login(model) {
    return await axios.post(`${RoutingInformation.baseRoute}auth/login`, {
      Username: model.Username,
      Password: model.Password
    });
  },
  async register(model) {
   return await axios.post(`${RoutingInformation.baseRoute}auth/register`, {
      Username: model.Username,
      Password: model.Password
    });
  }
}