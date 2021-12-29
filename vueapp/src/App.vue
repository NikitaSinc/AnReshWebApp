<template>
    <div class="#app">
        <layout/>
    </div>
</template>
<script>
import store from '@/store/index.js'

import SkillForm from '@/components/Skill/SkillForm.vue'
import FileWork from '@/components/FileWork/FileWorkPage.vue'

import DepartmentForm from '@/components/Department/DepartmentForm.vue'


import EmployeeForm from '@/components/Employee/EmployeeForm.vue'


import Login from '@/components/User/Login.vue'
import LoginForm from '@/components/User/LoginForm.vue'
import UserProfile from '@/components/User/UserProfile.vue'
import Registration from '@/components/User/Registration.vue'

import Layout from '@/components/Navigation/Layout.vue'

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
