<template>
    <div class="popup">
        <div class="popup__window">
            <div class="form-horizontal">
                <h2>Внесение данных о новом сотруднике</h2>
                <hr />
                <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>

                <div class="form-group">
                    <label>ФИО: </label>
                    <input v-model="employee.Full_name" placeholder="ФИО">
                </div>

                <div class="form-group">
                    <label>Отдел: </label>
                    <select v-model="department">
                        <option value="null" disabled hidden>Отдел</option>
                        <option v-for="department in departmentList" :key="department.Id" :value="department ">{{department.Name}}</option>
                    </select>
                </div>

                <div class="form-group">
                    <label>Навыки: </label>
                    <div>
                        <select v-model="this.employee.Skills" multiple>
                            <option v-for="skill in skillList" :value="skill.Id" :key="skill.Id">{{skill.Skill_name}} </option>
                        </select>
                    </div>
                </div>

                <div class="form-group">
                    <label>Заработная плата: </label>
                    <input v-model="employee.Salary" placeholder="Заработная плата">
                </div>


                <div class="form-group">
                    <button type="button" @click="sendData" >Сохранить</button>
                    <a href='#' @click="this.$emit('closeCreate')">Назад</a>
                </div>
            </div>
        </div>  
    </div>
</template>
<script>
    export default 
    {
        props:{employee:Object},
        data()
        {
            return {
                department:{Name: String(), Id: Number()},
                departmentList:[{type: Array, department:Object}],
                skill:{},
                skillList:[{}],
                index:Number
            }
        },
        methods:{
            async loadDepartmentData()
            {
                const response = await fetch(this.$store.state.backendPath +"Department/SendData")
                const serverData = await response.json() 
                this.departmentList = serverData;
                this.department = null;
            },
            
            async getSkills()
            {
                const response = await fetch(this.$store.state.backendPath +"Skill/SendData")
                const serverData = await response.json() 
                this.skillList = serverData;
            },

            async sendData()
            {
                this.employee.Id_department = this.department.Id
                const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({employee: this.employee})
                };
                const response = await fetch(this.$store.state.backendPath +"Employee/Create", requestOptions)
                this.employee.Id = await response.json();
                if (response.status === 200)
                {
                    this.$emit('closeCreate')
                }
                else
                {
                    this.$store.commit('setMessage', await response.json())
                }
            },
            
        },
        beforeMount()
        {
            this.$store.dispatch('user/checkValidation', '/Employee/EmployeeForm');
            this.loadDepartmentData();
            this.getSkills();
        }
    }
</script>
