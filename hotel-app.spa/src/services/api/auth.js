import axios from 'axios';
import { RoutingInformation } from '@/constants/routingInformation';
import jwt_decode from 'jwt-decode';

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
  },
  loggedIn() {
    const token = localStorage.getItem('token');
    const decoded = jwt_decode(token);
    const exp = decoded.exp * 1000; // Expires at (EXP)
    if(new Date().getTime() > exp) {
      return false;
    }
    return true;
  },
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  }
}