import Vue from 'vue';
import App from './App.vue';
import vuetify from './plugins/vuetify';
import VueResource from 'vue-resource';
import router from './router';
import store from './store/store';

Vue.use(VueResource);
Vue.config.productionTip = false

Vue.filter('upper', function (value) {
    if(!value) return;
    value = value.toUpperCase();
    return value;
})

new Vue({
  router,
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
