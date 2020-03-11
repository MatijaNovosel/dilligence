import jwt_decode from 'jwt-decode';

export default ({ router, store }) => {
  router.beforeEach((to, from, next) => {
    const token = store.getters.user.token;
    if (to.matched.some(record => record.meta.requiresAuth)) {
      if (token == null) {
        next({
          path: '/login',
          params: { nextUrl: to.fullPath }
        });
      } else {
        const decoded = jwt_decode(token);
        const exp = decoded.exp * 1000;
        if (new Date().getTime() > exp) {
          store.dispatch('removeUserData');
          next({
            path: '/login',
            params: { nextUrl: to.fullPath }
          });
        }
        next();
      }
    } else if (to.matched.some(record => record.meta.guest)) {
      if (token == null) {
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
}
