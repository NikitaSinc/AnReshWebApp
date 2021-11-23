<template>
    <h2>Профиль пользователя</h2>
    <button type="button" @click="this.$store.commit('user/logout')">Выйти из профиля</button>
    <button type="button" @click="this.$store.dispatch('user/checkValidation')">Валидация</button>
</template>
<script>

export default {
    data() { 
        return {

        }
    },
    methods:{
        }
    }
</script>
