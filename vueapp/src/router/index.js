import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '/Department/DepartmentForm',
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
    path: '/Employee/EmployeeEditPage',
    name: 'EmployeeEditPage',
    component: () => import('../components/Employee/EmployeeEditPage.vue'),
    props: true
  },

]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
