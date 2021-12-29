<template>
    <h1>Отделы</h1>
    <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
    <department-creation-page 
        v-bind:departmentList = departmentList
        v-if="showCreate"
        @closeCreate="closeCreate"    
    />
    <department-edit-page 
        v-bind:department = department
        v-bind:departmentList = departmentList
        v-if="showEdit"
        @closeEdit="closeEdit"  
        @delete="deleteDepartment"  
    />

    <div>
        <h2>Фильтры</h2>
        <br>
        <label>Название: </label>
        <input v-model="filter.Name" placeholder="Начните вводить название" @input="loadData()">
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
                Секторы
            </th>
            <th>
                Группы
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
            <td >
                <tr v-for="sector in department.Childrens" :key="sector">
                    {{sector.Name}}
                </tr>
            </td>
            <td>
                <tr v-for="sector in department.Childrens" :key="sector">
                    <tr v-for="group in sector.Childrens" :key="group">
                        {{group.Name}}
                    </tr>
                </tr>
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
                Новый сектор
            </td>
            <td>
                Новая группа
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
                filter:{},
                showEdit: false,
                showCreate: false,
                department:{},
                departmentList:[]
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
                const requestOptions = {
                            method: "POST",
                            headers: { "Content-Type": "application/json" },
                            body: JSON.stringify({department: this.filter})
                        };
                const response = await fetch(this.$store.state.backendPath +"Department/SendData", requestOptions)
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
                        this.loadData()
                    }
                    else
                    {
                        this.$store.commit('setMessage', await response.json())
                    }
                }
                this.showEdit = false
            },
        },
        mounted()
        {
            this.loadData()
        },

    }
</script>