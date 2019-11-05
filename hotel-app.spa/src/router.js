import Vue from 'vue';
import Router from 'vue-router';
import Index from './views/Index.vue';
import Account from './views/Account.vue';
import Subjects from './views/Subjects.vue';
import Employees from './views/Employees.vue';
import Calendar from './views/Calendar.vue';
import SubjectPage from './views/SubjectPage.vue';

Vue.use(Router);

export default new Router ({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [{
        path: "/",
        name: "index",
        component: Index
    }, {
        path: "/account",
        name: "account",
        component: Account
    }, {
        path: "/subjects",
        name: "subjects",
        component: Subjects
    }, {
        path: "/subjectPage/:id",
        name: "subjectPage",
        component: SubjectPage
    }, {
        path: "/employeeList",
        name: "employeeList",
        component: Employees
    }, {
        path: "/calendar",
        name: "calendar",
        component: Calendar
    }]
})