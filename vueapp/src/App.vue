<template>
    <div class="#app">
        <router-view/>
    </div>
</template>
<script>
import DepartmentForm from './components/Department/DepartmentForm.vue'
import DepartmentCreationPage from './components/Department/DepartmentCreationPage.vue'
import DepartmentEditPage from './components/Department/DepartmentEditPage.vue'
import EmployeeForm from './components/Employee/EmployeeForm.vue'
import EmployeeCreationPage from './components/Employee/EmployeeCreationPage.vue'
import EmployeeEditPage from './components/Employee/EmployeeEditPage.vue'

export default {
    components:{
        DepartmentForm,
        EmployeeForm,
        DepartmentEditPage,
        DepartmentCreationPage,
        EmployeeCreationPage,
        EmployeeEditPage
    }, 
} 

</script>
