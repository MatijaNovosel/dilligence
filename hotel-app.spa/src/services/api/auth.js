import axios from 'axios';
import NotificationService from '../notification';
import store from '../../store/store';

export default {
  async login(model) {
    return await axios.post('auth/login', {
      Username: model.Username,
      Password: model.Password
    });
  },

  async register(model) {
    return await axios.post('auth/register', {
      Username: model.Username,
      Password: model.Password
    }).catch(error => {
      NotificationService.error(`${error.name}: ${error.message}`, 'Unable to register!');
    });
  },

  async logout() {
    store.dispatch('removeUserData');
  }
}