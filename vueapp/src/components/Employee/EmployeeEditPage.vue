<template>
    <div class="popup">
        <div class="popup__window">
            <div class="form-horizontal">
                <h2>Редактирование данных сотрудника</h2>
                <hr />
                <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
                <div class="form-group">
                    <label>ФИО: </label>
                    <input v-model="employee.Full_name" :placeholder="employee.Full_name">
                </div>
                <div class="form-group">
                    <label>Отдел: </label>
                    <select v-model="department">
                        <option value="null" 
                        disabled hidden 
                        v-for="department in departmentList.filter(function(elem){if (elem.Id === employee.Id_department) return elem})" 
                        :key="department.Id">{{department.Name}}
                        </option>
                        <option v-for="department in departmentList" :key="department.Id" :value="department">{{department.Name}}</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Заработная плата: </label>
                    <input v-model="employee.Salary" :placeholder="employee.Salary">
                </div>


                <div class="form-group">
                    <button type="button" @click="sendData" >Сохранить</button>
                    <a href='#' @click="this.$emit('closeEdit')">Назад</a>
                </div>
            </div> 
        </div>
    </div> 
</template>
<script>
    export default 
    {
        props: 
        {
            employee:Object
        },

        data(){
            return{
                department:{type: Object()},
                departmentList:{type: Array(), department:Object()}
            }
        },

        methods:
        {
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
                    body: JSON.stringify({employee: this.employee})
                };
                const response = await fetch(this.$store.state.backendPath +"Employee/Edit", requestOptions)
                if (response.status === 200)
                {
                    this.$emit('closeEdit')
                }
                else
                {
                    this.$store.commit('setMessage', 'Ошибка при редактировании данных')
                }
            },
        },

        beforeMount()
        {
           this.loadDepartmentData();  
        },
        mounted()
        { 
            this.$store.dispatch('user/checkValidation', '/Employee/EmployeeForm');
        }
    }
</script>

