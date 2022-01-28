<template>
    <div class="#app">
        <layout/>
    </div>
</template>
<script lang = 'ts'>
import { defineComponent } from 'vue'

import Layout from "@/components/Navigation/Layout.vue"
import DepartmentForm from "@/components/Department/DepartmentForm.vue"
import EmployeeForm from "@/components/Employee/EmployeeForm.vue"
import FileWorkPage from "@/components/FileWork/FileWorkPage.vue"
import SkillForm from "@/components/Skill/SkillForm.vue"
import Login from "@/components/User/Login.vue"
import Registration from "@/components/User/Registration.vue"
import { VueCookieNext } from 'vue-cookie-next'

export default defineComponent({
    components:
    {
        Layout,
        DepartmentForm,
        EmployeeForm,
        FileWorkPage,
        SkillForm,
        Login,
        Registration
    },

    beforeMount()
    {
        if(VueCookieNext.getCookie('JWT') === null){
            this.$store.commit('logout')
            
        } else {
            this.$store.commit('login')
        }
    }
})

</script>
