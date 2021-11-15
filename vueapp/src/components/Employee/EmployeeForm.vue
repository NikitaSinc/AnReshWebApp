<template>
<div id="app">
   <h1>Стажировка в Аналитических Решениях</h1>
<h2>Сотрудники</h2>


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

    <tr v-for="employee in employeeList" :key="employee.Id">
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
                <router-link :to="{name:'EmployeeEditPage', params:{id: employee.Id, full_name: employee.Full_name, departmentId:employee.Id_department, salary: employee.Salary}}">Изменить</router-link> | <a href='#' @click="deleteEmployee(employee.Id)">Удалить</a>
            </td>
    </tr>
    <tr>
        <td></td>
        <td>
            Новый сотрудник
        </td><td></td><td>
        </td><td>
            <router-link to="/Employee/EmployeeCreationPage">Добавить</router-link>
        </td>
    </tr>
</table>
</div>
</template>

<script>
export default 
{
    data()
    {
        return{
            employee:{type: Object, Id:Number, Full_name:String, Id_department:Number ,Salary:Number},
            department:{type: Object, Id:Number, Name:String},
            currentDepatrmentId:{type:Number},
            employeeList:{type: Array},
            departmentList:{type: Array}
        }
    },
        methods: 
        {
            async loadData()
            {
                const response = await fetch(this.$store.state.backendPath +"Employee/SendData")
                const serverData = await response.json() 
                this.employeeList = serverData;
            },
       
            async getDepartments()
            {
                const response = await fetch(this.$store.state.backendPath +"Department/SendData")
                const serverData = await response.json() 
                this.departmentList = serverData;
            },

            async deleteEmployee(id){
                if (this.$store.state.logged === false){
                    this.$store.commit('setMessage', 'Для продолжения необходимо войти в аккаунт!');
                    this.$router.push('/User/Login')
                     
                } 
                else{
                this.$store.dispatch('checkValidation');
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({id: id})
                };
                fetch(this.$store.state.backendPath +"Employee/Delete", requestOptions),
                this.loadData()
                }
            },
        },
        beforeMount()
        {
            this.getDepartments()
            this.loadData()
        },
        mounted(){
            this.getDepartments()
            this.loadData()
        }
}
</script>