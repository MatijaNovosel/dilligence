import Vue from 'vue'
import axios from 'axios'
import apiConfig from '../api.config';

Vue.prototype.$axios = axios

axios.interceptors.request.use((config) => {
  config.mode = "cors";
  config.url = apiConfig.baseRoute + config.url;

  /*
  if (store.getters.user.token != null) {
    config.headers.common['Authorization'] = `Bearer ${store.getters.user.token}`;
  }
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