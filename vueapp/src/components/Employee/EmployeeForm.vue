<template>
    <h1>Сотрудники</h1>

    <employee-creation-page 
        v-bind:employee = employee
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
        <input v-model="filter.Full_name" placeholder="Начните вводить ФИО" @input="loadData()">
        
        <label> Отдел: </label>
        <select v-model="this.selectedDepartments" multiple size=3 @change="setDepartmentsFilter()">
            <option v-for="department in departmentList" :key="department" :value="department">{{department.Name}}</option>
        </select>

        <label v-if="this.selectedDepartments.length === 1"> Сектор: </label>
        <select v-if="this.selectedDepartments.length === 1" v-model="this.selectedSectors" multiple size=3 @change="setSectorsFilter()">
                <option v-for="sector in sectorsToSelect" :key="sector" :value="sector">{{sector.Name}}</option>
        </select>

        <label v-if="(this.selectedSectors.length === 1) && (this.selectedDepartments.length === 1)"> Группа: </label>
        <select v-if="(this.selectedSectors.length === 1) && (this.selectedDepartments.length === 1)" v-model="this.selectedGroups" multiple size=3 @change="setGroupsFilter()">
                <option v-for="group in groupsToSelect" :key="group" :value="group">{{group.Name}}</option>
        </select>

        <label> Навыки: </label>
        <select v-model="this.filter.Skills" multiple size=3 @change="loadData()">
            <option v-for="skill in skillList" :value="skill.Id" :key="skill.Id">{{skill.Skill_name}} </option>
        </select>
        <a href="#" style="margin-left: 15px; width: 30%; height:10%" @click="clearFilter"> Очистить</a>
    </div>

    <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>


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

        <tr v-for="employee in employeeList" :key="employee.Id" >
            <td>
                {{employee.Id}}
            </td>
            <td>
                {{employee.Full_name}}
            </td>
            <td>
                <div v-for="department in departmentList" :key="department.Id">
                    <div v-for="sector in department.Childrens" :key="sector.Id">
                        <div v-for="group in sector.Childrens.filter(function(elem){if (elem.Id === employee.Id_department) return elem})" :key="group.Id">
                            {{department.Name}} ({{sector.Name}}/{{group.Name}})
                        </div>
                    </div>
                </div>
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
                <a href='#' @click="() => { this.employee={Skills:[]}; showCreate = true}">Добавить</a>
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
                Кол-во: {{this.paginator.RowsCount}}
            </th>
            <th>
                Средняя з/п: {{this.paginator.AVGSalary}}
            </th>
        </tr>
    </table>
    <div>
        <label>Страница </label>
        <a v-if="currentPage !== 1" href='#' 
            @click="currentPage = pageList[pageList.indexOf(currentPage)-1]; loadData()">&lt;&nbsp; </a>
        <select v-model="this.currentPage" @change="loadData()">
            <option v-for="page in pageList" :value="page" :key="page" >{{page}} &nbsp; </option>
        </select>
        <a v-if="currentPage.pageIndex !== pageList.length" href='#' 
            @click="currentPage = pageList[pageList.indexOf(currentPage)+1];loadData()">&nbsp; &gt;</a>
        <a href="#" @click="this.currentPage = this.pageList[this.pageList.length -1];loadData()"> из {{pageList.length}}</a> 
        
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
            filter:{Departments:[], Skills: []},
            employee:{Skills: []},
            department:{},
            skill:{},
            selectedDepartments:[],
            sectorsToSelect:[],
            selectedSectors:[],
            groupsToSelect:[],
            selectedGroups:[],
            employeeList:[],
            departmentList:[],
            skillList:[],
            currentPage:1,
            pageList:[],
            paginator:{},
            index:Number
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

        setDepartmentsFilter(){
            this.filter.Departments = [];
            this.sectorsToSelect = [];
            this.groupsToSelect = [];
            for(var i=0; i < this.selectedDepartments.length; i++)
            {
                for(var y=0; y < this.selectedDepartments[i].Childrens.length; y++)
                {
                    this.sectorsToSelect.push(this.selectedDepartments[i].Childrens[y])
                    for(var z=0; z< this.selectedDepartments[i].Childrens[y].Childrens.length; z++)
                    {
                        console.log(this.selectedDepartments[i].Childrens[y].Childrens[z].Id)
                        this.filter.Departments.push(this.selectedDepartments[i].Childrens[y].Childrens[z].Id)
                    }
                }
            }
            this.loadData()
        },
        
        setSectorsFilter()
        {
            this.filter.Departments = [];
            this.groupsToSelect = [];
            for(var i=0; i < this.selectedSectors.length; i++)
                {
                    for(var y=0; y < this.selectedSectors[i].Childrens.length; y++)
                    {
                        this.groupsToSelect.push(this.selectedSectors[i].Childrens[y])
                        this.filter.Departments.push(this.selectedSectors[i].Childrens[y].Id)
                    }
                }
            this.loadData()
        },

        setGroupsFilter()
        {
            this.filter.Departments = [];
            for(var i=0; i < this.selectedGroups.length; i++)
            {
                this.filter.Departments.push(this.selectedGroups[i].Id)
            }
            this.loadData()
        },

        clearFilter()
        {
            this.selectedDepartments = [];
            this.selectedSectors = [];
            this.selectedGroups = [];
            this.filter = {Departments:[],Skills: []}; 
            this.loadData()
        },

        async loadData()
        { 
            var requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({employeeFilterModel: this.filter, currentPage: this.currentPage})
            };
            const response = await fetch(this.$store.state.backendPath +"Employee/SendData", requestOptions)
            const serverData = await response.json() 
            console.log(serverData)
            this.employeeList = serverData.employeeList;
            this.paginator = serverData.Paginator;
            this.setPages();
        },

        setPages()
        {
            
            this.pageList = []
            for(var i = 1; i<=this.paginator.LastPage; i++){
                this.pageList.push(i); 
            }
            if (this.currentPage > this.pageList.length)
            {
                this.currentPage = 1; 
                this.loadData();
            }
            
            
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
                    if (this.employeeList.length === 0){
                        this.currentPage --;
                        this.loadData(); 
                    }
                    
                    
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