import VueRouter from 'vue-router';
import routes from './routes';
import goTo from 'vuetify/es5/services/goto';
import store from '../src/store/store';

const router = new VueRouter({
  routes,
  mode: 'history',
  base: process.env.BASE_URL,
  linkExactActiveClass: 'nav-item active',
  scrollBehavior(to, from, savedPosition) {
    let scrollTo = 0;
    if (to.hash) {
      scrollTo = to.hash;
    } else if (savedPosition) {
      scrollTo = savedPosition.y;
    }
    return goTo(scrollTo);
  }
});

router.beforeEach((to, from, next) => {
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (store.getters.user.token == null) {
      next({
        path: '/login',
        params: { 
          nextUrl: to.fullPath 
        }
      });
    } else {
      next();
    }
  } else if (to.matched.some(record => record.meta.guest)) {
    if (store.getters.user.token == null) {
      next();
    } else {
      next({ 
        name: ''
      });
    }
  } else {
    next();
  }
});

export default router;