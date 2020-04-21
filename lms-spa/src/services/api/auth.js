import axios from 'axios';

export default {
  /**
   * Attempts to authorize the user, returning a token and other neccessary data.
   * @param {Object} payload - Request payload.
   * @param {string} payload.username - Username of the user.
   * @param {string} payload.password - Password of the user.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async login(payload) {
    return await axios.post('auth/login', {
      ...payload
    });
  },
  /**
   * Attempts to register the user.
   * @param {Object} payload - Request payload.
   * @param {string} payload.username - Username of the user.
   * @param {string} payload.password - Password of the user.
   * @return {AxiosPromise<any>} Axios promise to be resolved in the view.
   */
  async register(payload) {
    return await axios.post('auth/register', {
      Username: payload.Username,
      Password: payload.Password
    });
  }
}