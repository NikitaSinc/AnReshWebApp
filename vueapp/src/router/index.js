import { createRouter, createWebHistory } from 'vue-router'
import store from '../store';

const routes = [
  {
    path: '/FileWork/FileWorkPage',
    component: () => import('../components/FileWork/FileWorkPage.vue')
  },
  {
    path: '/Department/DepartmentForm',
    component: () => import('../components/Department/DepartmentForm.vue'),
  },
  {
    path: '/Department/DepartmentCreationPage',
    component: () => import('../components/Department/DepartmentCreationPage.vue'),
    meta:{
      loginRequired: true
    }
  },
  {
    path: '/Department/DepartmentEditPage',
    name: 'DepartmentEditPage',
    component: () => import('../components/Department/DepartmentEditPage.vue'),
    meta:{
      loginRequired: true
    },
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
    meta:{
      loginRequired: true
    }
  },
  {
    path: '/Employee/EmployeeEditPage',
    name: 'EmployeeEditPage',
    component: () => import('../components/Employee/EmployeeEditPage.vue'),
    meta:{
      loginRequired: true
    },
    props: true
  },
  {
    path: '/User/Login',
    name: 'Login',
    component: () => import('../components/User/Login.vue'),
    props: true
  },
  {
    path: '/User/Registration',
    name: 'Registration',
    component: () => import('../components/User/Registration.vue')
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  scrollBehavior(){
    window.scrollTo(0,0)
  },

});

router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.loginRequired)){
    if (!store.state.logged){
      store.commit('setMessage', 'Для продолжения необходимо войти в аккаунт!')
      next({path:'/User/Login' ,name:'Login'})
    } 
    else {
      if(store.dispatch('checkValidation'))
      next();
    };
  } 
  else next();
});
export default router
