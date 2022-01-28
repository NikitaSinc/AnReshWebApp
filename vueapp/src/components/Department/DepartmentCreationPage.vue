<template>
    <div class="popup">
        <div class="popup__window">
            <div class="form-horizontal">
                <h2>Создание подразделения</h2>
                <hr />
                <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
                <div class="form-group">
                    <label>Название: </label>
                    <input v-model="department.Name" placeholder="Новое подразделение">
                </div>

                <div class="form-group">
                    <label> Вышестоящий отдел </label>
                    <select v-model="selectedDepartment" @change="selectedSector = {}">
                        <option :value="{}">
                            Корневой
                        </option>
                        <option v-for="department in departmentList" :key="department" :value="department">
                            {{department.Name}}
                        </option>
                    </select>
                </div>

                <div class="form-group" v-if="JSON.stringify(selectedDepartment) !== '{}'">
                    <label> Вышестоящий сектор </label>
                    <select v-model="selectedSector">
                        <option :value="{}">
                            {{selectedDepartment.Name}}/
                        </option>
                        <option v-for="sector in selectedDepartment.Childrens" :key="sector" :value="sector">
                            {{selectedDepartment.Name}}/{{sector.Name}}
                        </option>
                    </select>
                </div>                

                <div class="form-group">
                    <button type="button" @click="sendData" >Сохранить</button>
                    <a href='#' @click="this.$emit('closeCreate')">Назад</a>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang= "ts">

import store from "@/store"
import { defineComponent, PropType } from "@vue/runtime-core"
import { Department} from "@/components/Department/types"

export default defineComponent
({   
    props:{departmentList:{type:Array as PropType<Array<Department>>, required: true}},

    data(){
        return{
            selectedDepartment:{} as Department,
            selectedSector:{} as Department,
            department:{} as Department,
        }
    },

    methods:
    {
        async sendData(): Promise<void>
        {
            if(JSON.stringify(this.selectedSector) !== '{}')
            {
                this.department.Pid = this.selectedSector.Id
            }
            else
            {
                if (JSON.stringify(this.selectedDepartment) !== '{}')
                {
                    this.department.Pid = this.selectedDepartment.Id
                }
                else 
                {
                    
                    this.department.Pid = 0
                }
            }
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ department: this.department })
            };
            const response = await fetch(store.state.backendPath +"Department/Create", requestOptions)
            if (response.status === 200)
            {
                this.department.Id = await response.json()
                this.$emit('closeCreate')
            }
            else
            {
                this.$store.commit('setMessage', await response.json())
            }
        }
    },
    beforeMount()
    {
        this.$store.dispatch('checkValidation', '/Department/DepartmentForm')
    }
})
</script>
