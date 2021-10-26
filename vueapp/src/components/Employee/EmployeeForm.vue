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

    <tr v-for="employee in employeeList" v-bind:key="employee.Id">
            <td>
                {{employee.Id}}
            </td>
            <td>
                {{employee.Full_name}}
            </td>
            <td>
                {{employee.Department.Name}}
            </td>
            <td>
                {{employee.Salary}}
            </td>
            <td>
                <router-link :to="{name:'EmployeeEditPage', params:{id: employee.Id, full_name: employee.Full_name, departmentName: employee.Department.Name, departmentId:employee.Department.Id, salary: employee.Salary}}">Изменить</router-link> | <a href='' @click="deleteEmployee(employee.Id)">Удалить</a>
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
        employee:{type: Object, Id:Number, Full_name:String, Department:Object,Salary:Number},
        Department:{type: Object, Id:Number, Name:String},
        employeeList:{type: Array}
        }
    },
        methods: 
        {
            async loadData()
            {
                const response = await fetch("http://localhost:44305/Employee/SendData")
                const serverData = await response.json() 
                this.employeeList = serverData;

            },
       
            async deleteEmployee(id){
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({id: id})
                };
                fetch("http://localhost:44305/Employee/Delete", requestOptions),
                this.loadData()
            },
         },
        beforeMount()
        {
            this.loadData()
        },
        mounted(){
            this.loadData()
        }
}
</script>