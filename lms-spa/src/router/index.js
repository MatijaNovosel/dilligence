import Vue from 'vue'
import VueRouter from 'vue-router'

import routes from './routes'

Vue.use(VueRouter)

export default function ({ store }) {
  const Router = new VueRouter({
    scrollBehavior: () => ({ x: 0, y: 0 }),
    routes,
    mode: "history",
    mode: process.env.VUE_ROUTER_MODE,
    base: process.env.VUE_ROUTER_BASE
  });

  const originalPush = VueRouter.prototype.push;
  VueRouter.prototype.push = function push(location) {
    return originalPush.call(this, location).catch(err => err);
  }

  return Router;
}
