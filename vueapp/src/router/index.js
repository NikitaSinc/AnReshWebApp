import { createRouter, createWebHistory } from 'vue-router'
import store from '../store';

const routes = [
  {
    path: '/FileWork/FileWorkPage',
    name: 'FileWorkPage',
    component: () => import('../components/FileWork/FileWorkPage.vue')
  },
  {
    path: '/Department/DepartmentForm',
    name: 'DepartmentForm',
    component: () => import('../components/Department/DepartmentForm.vue'),
  },
  {
    path: '/Department/DepartmentCreationPage',
    component: () => import('../components/Department/DepartmentCreationPage.vue'),
  },
  {
    path: '/Department/DepartmentForm',
    component: () => import('../components/Department/DepartmentForm.vue'),
  },
  {
    path: '/Employee/EmployeeForm',
    name: 'EmployeeForm',
    component: () => import('../components/Employee/EmployeeForm.vue'),
  },
  {
    path: '/User/Login',
    name: 'Login',
    component: () => import('../components/User/Login.vue'),
  },
  {
    path: '/User/Registration',
    name: 'Registration',
    component: () => import('../components/User/Registration.vue')
  },
  {
    path: '/Skill/SkillForm',
    name: 'SkillForm',
    component: () => import('../components/Skill/SkillForm.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  scrollBehavior()
  {
    window.scrollTo(0,0)
  },
})
  
router.beforeEach((to, from, next) => {
  if (store.state.redirected === true)
  {
    next()
  }
  else 
  {
    store.commit('clearMessage')
    next()
  }
})

export default router
