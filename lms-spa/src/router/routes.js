
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
        path: 'courses',
        name: 'courses',
        component: () => import('pages/Course/CourseList.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'course/:id',
        name: 'course-details',
        component: () => import('pages/Course/CourseDetails.vue'),
        children: [
          { path: 'home', name: 'course-details-home', component: () => import('pages/Course/CourseDetailsHome.vue') },
          { path: 'notifications', name: 'course-details-notifications', component: () => import('pages/Course/CourseDetailsNotifications.vue') },
          { path: 'participants', name: 'course-details-participants', component: () => import('pages/Course/CourseDetailsParticipants.vue') },
          { path: 'exams', name: 'course-details-exams', component: () => import('pages/Course/CourseDetailsExams.vue') },
          { path: 'tasks', name: 'course-details-tasks', component: () => import('pages/Course/CourseDetailsTasks.vue') }
        ],
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
      },
      {
        path: 'profile/:id',
        name: 'profile',
        component: () => import('pages/Profile.vue'),
        meta: {
          requiresAuth: true
        }
      },
      {
        path: 'exam-edit',
        name: 'exam-edit',
        component: () => import('pages/ExamEdit.vue'),
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
