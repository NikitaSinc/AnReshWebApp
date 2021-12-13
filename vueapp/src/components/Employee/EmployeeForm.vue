<template>
    <h1>Сотрудники</h1>

    <employee-creation-page 
        v-bind:employee = this.employee
        v-if="showCreate"
        @closeCreate="closeCreate"    
    />

    <employee-edit-page 
        v-bind:employee = employee
        v-if="showEdit"
        @closeEdit="closeEdit"    
    />

    <div>
        <h2>Фильтры</h2>
        <br>
        <label>ФИО: </label>
        <input v-model="employee.Full_name" placeholder="Начните вводить ФИО" @input="filter()">
        
        <label> Отдел: </label>
        <select v-model="department" @change="this.employee.Id_department = this.department.Id; filter()">
            <option value="null" >Любой </option>
            <option v-for="department in departmentList" :key="department" v-bind:value="department">{{department.Name}}</option>
        </select>

        <label> Навыки: </label>
        <select v-model="this.employee.Skills" multiple size=3 @change="filter()">
            <option v-for="skill in skillList" :value="skill.Id" :key="skill.Id">{{skill.Skill_name}} </option>
        </select>
        <button style="margin-left: 15px; width: 30%; height:10%" type="button" @click="employee = {}; loadData()"> Очистить</button>
    </div>

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
                Навыки
            </th>
            <th>
                Заработная плата
            </th>
            <th>
                Операции
            </th>
        </tr>

        <tr v-for="employee in pageLimit" :key="employee.Id" >
                <td>
                    {{employee.Id}}
                </td>
                <td>
                    {{employee.Full_name}}
                </td>
                <td v-for="department in departmentList.filter(function(elem){if (elem.Id === employee.Id_department) return elem})" :key="department.Id">
                    {{department.Name}}
                </td>
                <td >
                    <div v-for="index in employee.Skills" :key="index">
                        <div v-for="skill in skillList.filter(function(elem){if (index === elem.Id) return elem})" :key="skill.Skill_name">
                        {{skill.Skill_name}}
                        </div>
                    </div>
                </td>
                <td>
                    {{employee.Salary}}
                </td>
                <td>
                    <a href='#' @click="() => {this.employee = employee; showEdit = true}">Изменить</a> | <a href='#' @click="deleteEmployee(employee)">Удалить</a>
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

            </td>
            <td>
                <a href='#' @click="() => { this.employee={}; showCreate = true}">Добавить</a>
            </td>
        </tr>
                <tr>
            <th>

            </th>
            <th>

            </th>
            <th>

            </th>
            <th>

            </th>
            <th>
                Кол-во: {{employeeCount}}
            </th>
            <th>
                Средняя з/п: {{averageSalary}}
            </th>
        </tr>
    </table>
    <div>
        <a>Страница {{currentPage.pageIndex}} из {{pageList.length}}</a> 
        <br>
        <a v-if="currentPage.pageIndex !== 1" href='#' 
            @click="currentPage = pageList[pageList.indexOf(currentPage)-1]">Назад</a> | 
        <a v-if="currentPage.pageIndex !== pageList.length" href='#' 
            @click="currentPage = pageList[pageList.indexOf(currentPage)+1]">Вперед</a>
    </div>
</template>

<script>
import EmployeeCreationPage from '@/components/Employee/EmployeeCreationPage.vue'
import EmployeeEditPage from '@/components/Employee/EmployeeEditPage.vue'

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
            employee:{Skills: []},
            department:{},
            skill:{},
            employeeList:[{}],
            departmentList:[{}],
            skillList:[{}],
            pageList:[],
            currentPage:{pageIndex:1, start:0, end:10},
            rowPerPage:10,
            index:Number
        }
    },

    computed:
    {
        employeeCount()
        {   
            return this.employeeList.length
        },

        averageSalary()
        {
            return Math.floor(this.employeeList.reduce(function(total, curr){return total + curr.Salary;}, 0) / this.employeeList.length) 
        },

        pageLimit()
        {
            return this.employeeList.slice(this.currentPage.start, this.currentPage.end)
        },
    },

    methods: 
    {

        closeCreate()
        {
            this.showCreate = false
            this.employeeList.push(this.employee)
        },

        closeEdit()
        {
            this.showEdit = false
        },

        async filter()
        {
            var requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({employee: this.employee})
            };
            const response = await fetch(this.$store.state.backendPath +"Employee/SendData", requestOptions)
            const serverData = await response.json() 
            this.employeeList = serverData;
            this.setPages();
        },

        async loadData()
        { 
            var  requestOptions = {
                method: "GET",
                headers: { "Content-Type": "application/json" }
            };
            const response = await fetch(this.$store.state.backendPath +"Employee/SendData", requestOptions)
            const serverData = await response.json() 
            this.employeeList = serverData;
            this.setPages();
        },

        setPages()
        {
            this.pageList = [];
            var lastPrevPage = 0;
            for (var i = 0; i <= Math.floor(this.employeeList.length / this.rowPerPage); ++i)
            {
                var pageIndex = i+1;
                var start = lastPrevPage;
                var end = lastPrevPage + this.rowPerPage
                this.pageList.push({pageIndex, start, end})
                lastPrevPage = lastPrevPage + this.rowPerPage + 1
            }
            this.currentPage = this.pageList[0]
        },
    
        async getSkills()
        {
            const response = await fetch(this.$store.state.backendPath +"Skill/SendData")
            const serverData = await response.json() 
            this.skillList = serverData;
        },

        async getDepartments()
        {
            const response = await fetch(this.$store.state.backendPath +"Department/SendData")
            const serverData = await response.json() 
            this.departmentList = serverData;
        },

        async deleteEmployee(employee){
            this.$store.dispatch('user/checkValidation', '/Employee/EmployeeForm');
            if (this.$store.state.user.logged === true)
            {
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({id: employee.Id})
                };
                const response = await fetch(this.$store.state.backendPath +"Employee/Delete", requestOptions)
                if (response.status === 200)
                {
                    this.employeeList.splice(this.employeeList.indexOf(employee))
                }
                else
                {
                    this.$store.commit('setMessage', await response.json())
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