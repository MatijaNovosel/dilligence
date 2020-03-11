import axios from 'axios';

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
    });
  }
}