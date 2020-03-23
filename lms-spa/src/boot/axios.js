import Vue from 'vue'
import axios from 'axios'
import apiConfig from '../api.config';
// import store from '../store/index';

Vue.prototype.$axios = axios

axios.interceptors.request.use((config) => {
  config.mode = "cors";
  config.url = apiConfig.baseRoute + config.url;

  /*
  config.headers.common['Authorization'] = `Bearer ${store.state.user.token}`;
  */

  /*
  if (config.url.includes("File")) {
    config.headers.common['Accept'] = "multipart/form-data";
  }
  */

  return config;
}, function (error) {
  return Promise.reject(error);
});