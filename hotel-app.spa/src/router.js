import VueRouter from 'vue-router';
import routes from './routes';
import goTo from 'vuetify/es5/services/goto';

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

export default router;