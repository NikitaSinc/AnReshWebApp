<template>
    <div class="popup">
        <div class="popup__window">
            <div class="form-horizontal">
                <h2>Создание отдела</h2>
                <hr />
                <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
                <div class="form-group">
                    <label>Название: </label>
                    <input v-model="skill.Skill_name" placeholder="Новый отдел">
                </div>

                <div class="form-group">
                    <button type="button" @click="sendData" >Сохранить</button>
                    <a href='#' @click="this.$emit('closeCreate')">Назад</a>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from "@vue/runtime-core";
import { Skills } from "./types";

export default defineComponent
({
    props:{skill:{type: Object as PropType<Skills>, required: true}},

    methods:
    {
        async sendData(): Promise<void>
        {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ skill: this.skill })
            };
            const response = await fetch(this.$store.state.backendPath +"Skill/Create", requestOptions)
            if (response.status === 200)
            {
                this.skill.Id = await response.json()
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
        this.$store.dispatch('user/checkValidation', '/Skill/DepartmentForm')
    }
})
</script>
