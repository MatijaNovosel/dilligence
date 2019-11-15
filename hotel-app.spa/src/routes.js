import Login from '@/views/Login/Login.vue';
import MainLayout from '@/layouts/Main.vue';
import Index from '@/views/Index.vue';

let commonPages = {
  path: '/',
  component: MainLayout,
  children: [{
    path: '',
    name: 'index',
    component: Index
  }, {
    path: 'login',
    name: 'login',
    component: Login
  }]
};

const routes = [
  commonPages,
  {
    path: '*',
    redirect: "/home"
  }
];

export default routes;