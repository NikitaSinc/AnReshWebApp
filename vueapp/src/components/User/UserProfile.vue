<template>
    <h2>Профиль пользователя</h2>
    <button type="button" @click="$store.commit('logout')">Выйти из профиля</button>
    <button type="button" @click="$store.dispatch('checkValidation')">Валидация</button>
</template>
<script lang = "ts">
import { defineComponent } from "@vue/runtime-core"

export default defineComponent({
    data() { 
        return {
        }
    },
    methods:{
        }
    })
</script>
