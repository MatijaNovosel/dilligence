import axios from 'axios';
import { RoutingInformation } from '@/constants/routingInformation';
import jwt_decode from 'jwt-decode';
import NotificationService from '../notification';

export default {
  async login(model) {
    return await axios.post(`${RoutingInformation.baseRoute}auth/login`, {
      Username: model.Username,
      Password: model.Password
    }).catch(error => {
      NotificationService.error(`${error.name}: ${error.message}`, 'Unable to log in!');
    });
  },
  async register(model) {
   return await axios.post(`${RoutingInformation.baseRoute}auth/register`, {
      Username: model.Username,
      Password: model.Password
    }).catch(error => {
      NotificationService.error(`${error.name}: ${error.message}`, 'Unable to register!');
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