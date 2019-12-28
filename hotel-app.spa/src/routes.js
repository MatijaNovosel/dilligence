import Login from '@/views/Login/Login.vue';
import MainLayout from '@/layouts/Main.vue';
import Index from '@/views/Index.vue';
import Subjects from '@/views/Subjects.vue';
import Exams from '@/views/Exams.vue';
import SubjectPage from '@/views/SubjectPage.vue';
import Employees from '@/views/Employees.vue';
import MySubjects from '@/views/MySubjects.vue';
import AuthTest from '@/views/AuthTest.vue';

let commonPages = {
  path: '/',
  component: MainLayout,
  children: [{
    path: 'home',
    name: 'home',
    component: Index,
    meta: { 
      requiresAuth: true
    }
  }, {
    path: 'login',
    name: 'login',
    component: Login,
    meta: { 
      guest: true
    }
  }, {
    path: 'authTest',
    name: 'auth-test',
    component: AuthTest,
    meta: {
      requiresAuth: true  
    }
  }]
};

let employeePages = {
  path: '/employees',
  component: MainLayout,
  children: [{
    path: '',
    name: 'employees',
    component: Employees,
    meta: { 
      requiresAuth: true
    }
  }]
};

let subjectPages = {
  path: '/subjects',
  component: MainLayout,
  children: [{
    path: '',
    name: 'subjects',
    component: Subjects,
    meta: { 
      requiresAuth: true
    }
  }, {
    path: 'details/:id',
    name: 'subject-details',
    component: SubjectPage,
    meta: { 
      requiresAuth: true
    }
  }, {
    path: 'my',
    name: 'my-subjects',
    component: MySubjects,
    meta: { 
      requiresAuth: true
    }
  }]
};

let examPages = {
  path: '/exams',
  component: MainLayout,
  children: [{
    path: '',
    name: 'exams',
    component: Exams,
    meta: { 
      requiresAuth: true
    }
  }]
};

const routes = [
  commonPages,
  subjectPages,
  examPages,
  employeePages,
  {
    path: '*',
    redirect: "/home"
  }
];

export default routes;