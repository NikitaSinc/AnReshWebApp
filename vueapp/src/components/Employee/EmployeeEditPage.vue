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
                    <select v-model="selectedDepartment" @change="this.selectedSector = {}, this.selectedGroup = {}">
                        <option :value="{}" >Не выбрано</option>
                        <option v-for="department in departmentList" :key="department" v-bind:value="department">{{department.Name}}</option>
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
                    <select v-model="selectedGroup" @change="this.employee.Id_department = this.selectedGroup.Id;">
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
                    <input type="number" v-model="employee.Salary" :placeholder="employee.Salary">
                </div>

                <div class="form-group">
                    <button type="button" @click="sendData" >Сохранить</button>
                    <a href='#' @click="this.$emit('closeEdit')">Назад</a>
                </div>
            </div> 
        </div>
    </div> 
</template>

<script lang="ts">

import { defineComponent, PropType } from '@vue/runtime-core';
import { Employee } from './types';
import { Department } from '../Department/types';
import { Skills } from '../Skill/types';

export default defineComponent
({
    props: 
    {
        employee:{type: Object as PropType<Employee>, required: true}
    },

    data(){
        return{
            selectedDepartment:{} as Department,
            selectedSector:{} as Department,
            selectedGroup:{} as Department,
            department:{} as Department,
            departmentList:[] as Array<Department>,
            skill:{} as Skills,
            skillList:[] as Array<Skills>,
            index:0 as number
        }
    },

    methods:
    {
        findGroup(): void
        {
            for (var i = 0; i<this.departmentList.length; i++)
            {
                for (var y = 0; y<this.departmentList[i].Childrens.length; y++)
                {
                    for (var z = 0; z<this.departmentList[i].Childrens[y].Childrens.length; z++)
                    {
                        if (this.departmentList[i].Childrens[y].Childrens[z].Id === this.employee.Id_department)
                        {
                            this.selectedGroup = this.departmentList[i].Childrens[y].Childrens[z];
                            this.selectedSector = this.departmentList[i].Childrens[y];
                            this.selectedDepartment = this.departmentList[i];
                        }
                    }
                }
            }
        },

        async loadDepartmentData(): Promise<void>
        {
            const response = await fetch(this.$store.state.backendPath +"Department/SendData")
            const serverData = await response.json() 
            this.departmentList = serverData;
            this.findGroup();
        },

        async getSkills(): Promise<void>
        {
            const response = await fetch(this.$store.state.backendPath +"Skill/SendData")
            const serverData = await response.json() 
            this.skillList = serverData;
        },

        async sendData(): Promise<void>
        {
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
                this.$store.commit('setMessage', await response.json())
            }
        },
    },

    beforeMount()
    { 
        this.getSkills();
        this.loadDepartmentData();  
        
    },
    mounted()
    { 
        this.$store.dispatch('checkValidation', '/Employee/EmployeeForm');    
    }
})
</script>

