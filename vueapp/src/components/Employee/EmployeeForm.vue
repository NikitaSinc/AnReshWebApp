<template>
    <h1>Стажировка в Аналитических Решениях</h1>
    <h2>Сотрудники</h2>

        <employee-creation-page 
            v-if="showCreate"
            @closeCreate="closeCreate"    
        />

        <employee-edit-page 
            v-bind:employee = employee
            v-if="showEdit"
            @closeEdit="closeEdit"    
        />

    <table class="table">
        <tr>
            <th>
                ID
            </th>
            <th>
                ФИО
            </th>
            <th>
                Отдел
            </th>
            <th>
                Заработная плата
            </th>
            <th>
                Операции
            </th>
        </tr>

        <tr v-for="employee in employeeList" :key="employee.Full_name" >
                <td>
                    {{employee.Id}}
                </td>
                <td>
                    {{employee.Full_name}}
                </td>
                <td v-for="department in departmentList.filter(function(elem){if (elem.Id === employee.Id_department) return elem})" :key="department.Id">
                    {{department.Name}}
                </td>
                <td>
                    {{employee.Salary}}
                </td>
                <td>
                    <a href='#' @click="() => {this.employee = employee; showEdit = true}">Изменить</a> | <a href='#' @click="deleteEmployee(employee.Id)">Удалить</a>
                </td>
        </tr>
        <tr>
            <td>

            </td>
            <td>
                Новый сотрудник
            </td>
            <td>

            </td>
            <td>

            </td>
            <td>
                <a href='#' @click="showCreate = true">Добавить</a>
            </td>
        </tr>
    </table>
</template>

<script>
import EmployeeCreationPage from './EmployeeCreationPage.vue'
import EmployeeEditPage from './EmployeeEditPage.vue'

export default 
{
    components:
    {
        EmployeeCreationPage,
        EmployeeEditPage
    },
    data()
    {
        return{
            showEdit: false,
            showCreate: false,
            employee:{ Id:Number(), Full_name:String(), Id_department:Number() ,Salary:Number()},
            department:{ Id:Number(), Name:String()},
            employeeList:{type: Array},
            departmentList:{type: Array},
            skillList:{type: Array}

        }
    },
        methods: 
        {
            closeCreate()
            {
                this.showCreate = false
                this.loadData()
            },

            closeEdit()
            {
                this.showEdit = false
                this.loadData()
            },

            async loadData()
            {
                const response = await fetch(this.$store.state.backendPath +"Employee/SendData")
                const serverData = await response.json() 
                this.employeeList = serverData;
            },
       
            async getSkills()
            {
                const response = await fetch(this.$store.state.backendPath +"Skills/SendData")
                const serverData = await response.json() 
                this.skillList = serverData;
            },

            async getDepartments()
            {
                const response = await fetch(this.$store.state.backendPath +"Department/SendData")
                const serverData = await response.json() 
                this.departmentList = serverData;
            },

            async deleteEmployee(id){
                this.$store.dispatch('user/checkValidation', '/Employee/EmployeeForm');
                if (this.$store.state.user.logged === true)
                {
                    const requestOptions = {
                        method: "POST",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify({id: id})
                    };
                    const response = await fetch(this.$store.state.backendPath +"Employee/Delete", requestOptions)
                    if (response.status === 200)
                    {
                        this.loadData()
                    }
                    else
                    {
                        this.$store.commit('setMessage', 'Ошибка при удалении данных')
                    }
                }
            },

        },
        beforeMount(){
            this.getSkills()
            this.getDepartments()
            this.loadData()
        }
}
</script>