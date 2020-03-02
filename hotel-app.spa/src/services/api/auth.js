import axios from 'axios';
import NotificationService from '../notification';
import store from '../../store/store';

export default {
  async login(payload) {
    return await axios.post('auth/login', {
      Username: payload.Username,
      Password: payload.Password
    });
  },

  async register(payload) {
    return await axios.post('auth/register', {
      Username: payload.Username,
      Password: payload.Password
    }).catch(error => {
      NotificationService.error(`${error.name}: ${error.message}`, 'Unable to register!');
    });
  },

  async logout() {
    store.dispatch('removeUserData');
  }
}