<template>
<div id="app">
   <h1>Стажировка в Аналитических Решениях</h1>
<h2>Отделы</h2>


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

    <tr v-for="department in departmentList" v-bind:key = "department.Id">
            <td>
                {{department.Id}}
            </td>
            <td>
                {{department.Name}}
            </td>
            <td>
                <router-link :to="{name:'DepartmentEditPage', params:{id: department.Id, name: department.Name}}">Изменить</router-link> | <a href='#' @click="deleteDepartment(department.Id)">Удалить</a>
            </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            Новый отдел
        </td>

        <td>
            <router-link to="/Department/DepartmentCreationPage">Добавить</router-link>
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
        department:{type: Object},
        departmentList:{type: Array}}
    },
        methods: 
        {
            async loadData()
            {
                const response = await fetch(this.$store.state.backendPath +"Department/SendData")
                const serverData = await response.json() 
                this.departmentList = serverData;
            },
       
            async deleteDepartment(id){
                if (this.$store.state.logged === false){
                    this.$router.push('/User/Login');
                     this.$store.commit('setMessage', 'Для продолжения необходимо войти в аккаунт!')
                }                 
                else{
                this.$store.dispatch('checkValidation');
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({id: id})
                };
                fetch(this.$store.state.backendPath +"Department/Delete", requestOptions),
                this.loadData()
                }
            },
        },
            mounted()
            {
                this.loadData()
            },

}
</script>