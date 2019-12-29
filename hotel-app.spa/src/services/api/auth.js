import axios from 'axios';
import { ROUTING } from '@/constants/Routing';
import jwt_decode from 'jwt-decode';
import NotificationService from '../notification';
import store from '../../store/store';

export default {
  async login(model) {
    return await axios.post(`${ROUTING.baseRoute}auth/login`, {
      Username: model.Username,
      Password: model.Password
    });
  },
  
  async register(model) {
   return await axios.post(`${ROUTING.baseRoute}auth/register`, {
      Username: model.Username,
      Password: model.Password
    }).catch(error => {
      console.log(error);
      NotificationService.error(`${error.name}: ${error.message}`, 'Unable to register!');
    });
  },
  
  async loggedIn() {
    const token = localStorage.getItem('token');
    const decoded = jwt_decode(token);
    const exp = decoded.exp * 1000; // Expires at (EXP)
    if(new Date().getTime() > exp) {
      return false;
    }
    return true;
  },
  
  async logout() {
    store.dispatch('removeUserData');
  }
}