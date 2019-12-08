import Vue from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import VueResource from 'vue-resource';
import VueRouter from 'vue-router';
import store from './store/store';
import router from './router';
import Notifications from 'vue-notification';
import VueCodeHighlight from 'vue-code-highlight';

require('../node_modules/vue-code-highlight/themes/prism-twilight.css');

Vue.use(Notifications);
Vue.use(VueResource);
Vue.use(VueRouter);
Vue.use(VueCodeHighlight);

Vue.config.productionTip = false;

/*

axios.interceptors.request.use((config) => {
  config.withCredentials = config.url == "/token";
  //config.mode = "cors";

  //if (config.method === "post") {
  //  config.headers["Content-Type"] = 'application/x-www-form-urlencoded; charset=utf-8';
  //}

  config.url = (env.VUE_APP_SERVER_URL != null ? env.VUE_APP_SERVER_URL : "") + "/api" + config.url;

  if (config.method === "get") {  // IE is caching get request
    config.url += (config.url.indexOf('?') !== -1 ? "&t=" : "?t=") + new Date().getTime()
  }

  //if (store.getters.isAuthenticated) {
  if (config.withCredentials == false) {
    config.headers.common['Authorization-Bearer'] = 'Bearer ' + sessionStorage.getItem('token');
  }
  //}

  return config;
}, function (error) {
  return Promise.reject(error);
});

axios.interceptors.response.use(function (response) {
  return response;
}, function (error) {
  if (error.response === undefined) {
    return;
  }

  if (error.response.status === 500) {
    if (error.response.data.tag == "token") {
      store.dispatch("logoutUser");
      router.push({ name: "login", query: { from: router.currentRoute.fullPath } });
    }
    else {
      alert(i18n.t("error"));
    }
    return;
  }

  if (error.response.status === 403) {
    router.push("/");
    return;
  }

  if (error.response.status === 401) {
    store.dispatch("logoutUser");
    return;
  }

  NProgress.done();
  return Promise.reject(error);
});

*/

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
