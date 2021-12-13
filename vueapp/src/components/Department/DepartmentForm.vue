<template>
    <h1>Отделы</h1>
    <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
    <department-creation-page 
        v-bind:department = department
        v-if="showCreate"
        @closeCreate="closeCreate"    
    />
    <department-edit-page 
        v-bind:department = department
        v-if="showEdit"
        @closeEdit="closeEdit"    
    />

    <div>
        <h2>Фильтры</h2>
        <br>
        <label>ФИО: </label>
        <input v-model="department.Name" placeholder="Начните вводить ФИО" @input="filter()">
    </div>

    <table class="table">
        <tr>
            <th>
                ID
            </th>
            <th>
                Отдел
            </th>
            <th>
                Операции
            </th>
        </tr>

        <tr v-for="department in departmentList" :key = "department.Name">
            <td>
                {{department.Id}}
            </td>
            <td>
                {{department.Name}}
            </td>
            <td>
                <a href='#' @click="() => {this.department = department; showEdit = true}">Изменить</a> | <a href='#' @click="deleteDepartment(department)">Удалить</a>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Новый отдел
            </td>

            <td>
                <a href='#' @click="()=>{this.department={};showCreate = true}">Добавить</a>
            </td>
        </tr>
    </table>
</template>

<script>
    import DepartmentCreationPage from '@/components/Department/DepartmentCreationPage.vue'
    import DepartmentEditPage from '@/components/Department/DepartmentEditPage.vue'
    

    export default  
    {
        components: { DepartmentCreationPage, DepartmentEditPage },

        data()
        {
            return {
                showEdit: false,
                showCreate: false,
                department:{},
                departmentList:[{}]
            }
        },

        methods: 
        {
            closeCreate()
            {
                this.departmentList.push(this.department)
                this.showCreate = false
            },

            closeEdit()
            {
                this.showEdit = false
            },

            async filter()
            {
                const requestOptions = {
                            method: "POST",
                            headers: { "Content-Type": "application/json" },
                            body: JSON.stringify({department: this.department})
                        };
                const response = await fetch(this.$store.state.backendPath +"Department/SendData", requestOptions)
                const serverData = await response.json() 
                this.departmentList = serverData;
            },

            async loadData()
            {
                const response = await fetch(this.$store.state.backendPath +"Department/SendData")
                const serverData = await response.json() 
                this.departmentList = serverData;
            },
    
            async deleteDepartment(department)
            {
                    this.$store.dispatch('user/checkValidation', '/Department/DepartmentForm');
                    if (this.$store.state.user.logged === true)
                    {
                        const requestOptions = {
                            method: "POST",
                            headers: { "Content-Type": "application/json" },
                            body: JSON.stringify({id: department.Id})
                        };
                        const response = await fetch(this.$store.state.backendPath +"Department/Delete", requestOptions)
                    
                        if (response.status === 200)
                        {
                            this.departmentList.splice(this.departmentList.indexOf(department))
                        }
                        else
                        {
                            this.$store.commit('setMessage', await response.json())
                        }
                    }
            },
        },
        mounted()
        {
            this.loadData()
        },

    }
</script>