<template>
    <div class="form-horizontal">
        <h2>Редактирование отдела</h2>
        <hr />

        <div class="form-group">
            <label>Название: </label>
            <input v-model="name" :placeholder="name">
        </div>

        <div class="form-group">
            <button type="button" @click="sendData" >Сохранить</button>
            <router-link to="/Department/DepartmentForm">Назад</router-link>
        </div>
    </div>  
</template>
<script>
export default {
    props: ['id', 'name'],
        methods:{
            async sendData(){
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ name: this.name, id:this.id})
                };
                fetch("http://localhost:44305/Department/Edit", requestOptions),
                this.$router.push('/Department/DepartmentForm')
            }

        }
}

</script>
