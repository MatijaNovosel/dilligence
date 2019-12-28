import Vue from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import VueResource from 'vue-resource';
import VueRouter from 'vue-router';
import store from './store/store';
import router from './router';
import Notifications from 'vue-notification';
import axios from 'axios';

Vue.use(Notifications);
Vue.use(VueResource);
Vue.use(VueRouter);

Vue.config.productionTip = false;

axios.interceptors.request.use((config) => {
  config.mode = "cors";
  
  if(store.getters.user.token != null) {
    config.headers.common['Authorization'] = `Bearer ${store.getters.user.token}`;
  }

  return config;
}, function (error) {
  return Promise.reject(error);
});

Vue.filter('upper', function (value) {
    if(!value) return;
    value = value.toUpperCase();
    return value;
});

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app');
