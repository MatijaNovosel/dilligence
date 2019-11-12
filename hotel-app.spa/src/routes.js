import Login from '@/views/Login/Login.vue';
import App from '@/App.vue';

let generalPages = {
  path: '/login',
  component: App,
  children: [{
    path: '',
    name: 'login',
    component: Login
  }]
};

const routes = [
  generalPages,
  {
    path: '*',
    redirect: "/home"
  }
];

export default routes;