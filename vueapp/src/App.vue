<template>
    <div class="#app">
        <layout/>
    </div>
</template>
<script>
import store from '@/store/index.js'

import SkillForm from '@/components/Skill/SkillForm.vue'
import SkillCreationPage from '@/components/Skill/SkillCreationPage.vue'
import SkillEditPage from '@/components/Skill/SkillEditPage.vue'

import DepartmentForm from '@/components/Department/DepartmentForm.vue'
import DepartmentCreationPage from '@/components/Department/DepartmentCreationPage.vue'
import DepartmentEditPage from '@/components/Department/DepartmentEditPage.vue'

import EmployeeForm from '@/components/Employee/EmployeeForm.vue'
import EmployeeCreationPage from '@/components/Employee/EmployeeCreationPage.vue'
import EmployeeEditPage from '@/components/Employee/EmployeeEditPage.vue'

import Login from '@/components/User/Login.vue'
import LoginForm from '@/components/User/LoginForm.vue'
import UserProfile from '@/components/User/UserProfile.vue'
import Registration from '@/components/User/Registration.vue'

import Layout from '@/components/Layout.vue'

import FileWorkPAge from '@/components/FileWork/FileWorkPage.vue'
import Multiselect from '@vueform/multiselect'
Vue.component('multiselect', Multiselect)

export default {
    store:{
        store
    },
    components:{
        Layout
    },
    beforeMount(){
        if(this.$cookies.get('JWT') === null){
            store.commit('user/logout')
        } else {
            store.commit('user/login')
        }
    }
} 

</script>
