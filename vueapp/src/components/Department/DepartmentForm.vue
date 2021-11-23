<template>
    <h1>Стажировка в Аналитических Решениях</h1>
    <h2>Отделы</h2>
    <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
    <department-creation-page 
        v-if="showCreate"
        @closeCreate="closeCreate"    
    />
    <department-edit-page 
        v-bind:department = department
        v-if="showEdit"
        @closeEdit="closeEdit"    
    />

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
                <a href='#' @click="() => {this.department = department; showEdit = true}">Изменить</a> | <a href='#' @click="deleteDepartment(department.Id)">Удалить</a>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Новый отдел
            </td>

            <td>
                <a href='#' @click="showCreate = true">Добавить</a>
            </td>
        </tr>
    </table>
</template>

<script>
    import DepartmentCreationPage from './DepartmentCreationPage.vue'
    import DepartmentEditPage from './DepartmentEditPage.vue'
    

    export default  
    {
        components: { DepartmentCreationPage, DepartmentEditPage },

        data()
        {
            return {
                showEdit: false,
                showCreate: false,
                department:{type: Object},
                departmentList:{type: Array}
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
                const response = await fetch(this.$store.state.backendPath +"Department/SendData")
                const serverData = await response.json() 
                this.departmentList = serverData;
            },
    
            async deleteDepartment(id)
            {
                    this.$store.dispatch('user/checkValidation', '/Department/DepartmentForm');
                    if (this.$store.state.user.logged === true)
                    {
                        const requestOptions = {
                            method: "POST",
                            headers: { "Content-Type": "application/json" },
                            body: JSON.stringify({id: id})
                        };
                        const response = await fetch(this.$store.state.backendPath +"Department/Delete", requestOptions)
                    
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
        mounted()
        {
            this.loadData()
        },

    }
</script>