
const routes = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [
      {
        path: '',
        name: 'home',
        component: () => import('pages/Index.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'login',
        component: () => import('pages/Login.vue')
      },
      {
        path: 'employees',
        name: 'employees',
        component: () => import('pages/Employees.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'test',
        name: 'test',
        component: () => import('pages/Test.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'chat',
        name: 'chat',
        component: () => import('pages/Chat.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'subjects',
        name: 'subjects',
        component: () => import('pages/Subjects.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'settings',
        name: 'settings',
        component: () => import('pages/Settings.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'exams',
        name: 'exams',
        component: () => import('pages/Exams.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'exams/:id',
        name: 'exam-details',
        component: () => import('pages/ExamDetails.vue'),
        meta: {
          requiresAuth: true
        }
      }
    ]
  }
]

if (process.env.MODE !== 'ssr') {
  routes.push({
    path: '*',
    component: () => import('pages/Error404.vue')
  })
}

export default routes
