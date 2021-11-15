<template>
    <div class="form-horizontal">
        <h2>Редактирование данных сотрудника</h2>
        <hr />

        <div class="form-group">
            <label>ФИО: </label>
            <input v-model="full_name" :placeholder="full_name">
        </div>
        <div class="form-group">
            <label>Отдел: </label>
            <select v-model="department">
                <option value="null" disabled hidden>{{departmentName}}</option>
                <option v-for="department in departmentList" :key="department.Id" :value="department">{{department.Name}}</option>
            </select>
        </div>
        <div class="form-group">
            <label>Заработная плата: </label>
            <input v-model="salary" :placeholder="salary">
        </div>


        <div class="form-group">
            <button type="button" @click="sendData" >Сохранить</button>
            <router-link to="/Employee/EmployeeForm">Назад</router-link>
        </div>
    </div>  
</template>
<script>
export default {
    data(){
        return{
            department:{type: Object},
            departmentList:{type: Array, department:Object}
        }
    },

    props: ['id', 'full_name', 'departmentName', 'departmentId', 'salary'],
        methods:{

            async loadDepartmentData(){
                const response = await fetch(this.$store.state.backendPath +"Department/SendData")
                const serverData = await response.json() 
                this.departmentList = serverData;
                this.department = null;
            },

            async sendData(){
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({id: this.id, full_name: this.full_name, departmentId: this.department.Id, salary: this.salary})
                };
                fetch(this.$store.state.backendPath +"Employee/Edit", requestOptions),
                this.$router.push('/Employee/EmployeeForm')
            },
        },
        beforeMount(){
                this.loadDepartmentData();
            }
}

</script>
