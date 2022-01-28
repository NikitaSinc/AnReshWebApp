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
                    <select v-model="selectedDepartment" @change="this.selectedSector = {}; this.selectedGroup = {};">
                        <option :value="{}" disabled hidden>Отдел</option>
                        <option v-for="department in departmentList" :key="department" :value="department ">{{department.Name}}</option>
                    </select>
                </div>

                <div class="form-group" v-if="JSON.stringify(selectedDepartment) !== '{}'">
                    <label>Сектор: </label>
                    <select v-model="selectedSector" @change="this.selectedGroup = {};">
                        <option :value="{}" >Не выбрано</option>
                        <option v-for="sector in selectedDepartment.Childrens" :key="sector" :value="sector">{{sector.Name}}</option>
                    </select>
                </div>

                <div class="form-group" v-if="(JSON.stringify(selectedSector) !== '{}')">
                    <label>Группа: </label>
                    <select v-model="selectedGroup" @change=" this.department = selectedGroup;">
                        <option :value="{}" >Не выбрано</option>
                        <option v-for="group in selectedSector.Childrens" :key="group" :value="group">{{group.Name}}</option>
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
                    <input type="number" v-model="employee.Salary" placeholder="Заработная плата">
                </div>


                <div class="form-group">
                    <button type="button" @click="sendData" >Сохранить</button>
                    <a href='#' @click="this.$emit('closeCreate')">Назад</a>
                </div>
            </div>
        </div>  
    </div>
</template>

<script lang = "ts">

import { defineComponent, PropType } from "@vue/runtime-core"
import { Department } from "../Department/types"
import { Skills } from "../Skill/types"
import { Employee } from "./types"

export default defineComponent
({
    props:{employee:{type: Object as PropType<Employee>, required: true}},
    data()
    {
        return {
            selectedDepartment:{} as Department,
            selectedSector:{} as Department,
            selectedGroup:{} as Department,
            department:{} as Department,
            departmentList:[] as Array<Department>,
            skill:{} as Skills,
            skillList:[] as Array<Skills>,
            index: 0 as number
        }
    },
    methods:{
        async loadDepartmentData(): Promise<void>
        {
            const response = await fetch(this.$store.state.backendPath +"Department/SendData")
            const serverData = await response.json() 
            this.departmentList = serverData;

        },
        
        async getSkills(): Promise<void>
        {
            const response = await fetch(this.$store.state.backendPath +"Skill/SendData")
            const serverData = await response.json() 
            this.skillList = serverData;
        },

        async sendData(): Promise<void>
        {
            if ((this.employee.Full_name === "") || (this.department.Id === 0) || (this.employee.Skills === []))
            {
                this.$store.commit('setMessage', 'Заполните все поля!')
            }
            else
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
            }

        },
        
    },
    beforeMount()
    {
        
        this.$store.dispatch('checkValidation', '/Employee/EmployeeForm');
        this.loadDepartmentData();
        this.getSkills();
    }
})
</script>
