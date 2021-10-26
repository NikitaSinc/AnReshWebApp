<template>
<div id="app">
    <form class="form-horizontal" @submit.prevent>
        <h2>Создание отдела</h2>
        <hr />
            <div class="form-group">
                <label>Название: </label>
                <input v-model="name" placeholder="Новый отдел">
            </div>

            <div class="form-group">
                <button type="button" @click="sendData" >Сохранить</button>
                <router-link to="/Department/DepartmentForm">Назад</router-link>
            </div>
    </form>
</div>

</template>

<script>
export default {
    data(){
        return{
              name: '',  
            }
        },   

    methods:{
        async sendData(){
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ name: this.name })
            };
            fetch("http://localhost:44305/Department/Create", requestOptions),
            this.$router.push('/Department/DepartmentForm')
        }
    }
}
</script>
