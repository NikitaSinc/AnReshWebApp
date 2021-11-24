import { createRouter, createWebHistory } from 'vue-router'

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
    path: '/Department/DepartmentEditPage',
    name: 'DepartmentEditPage',
    component: () => import('../components/Department/DepartmentEditPage.vue'),
    props: true
  },
  {
    path: '/Employee/EmployeeForm',
    name: 'EmployeeForm',
    component: () => import('../components/Employee/EmployeeForm.vue'),
  },
  {
    path: '/Employee/EmployeeCreationPage',
    name: 'EmployeeCreationPage',
    component: () => import('../components/Employee/EmployeeCreationPage.vue'),
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
  routes
})

export default router
