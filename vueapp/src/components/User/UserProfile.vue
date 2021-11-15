<template>
    <h2>Профиль пользователя</h2>
    <button type="button" @click="this.$store.commit('logout')">Выйти из профиля</button>
    <button type="button" @click="this.$store.dispatch('checkValidation')">Валидация</button>
    <button type="button" @click="fakeJWT">Фейковый токен</button>
</template>
<script>

export default {
    data() { 
        return {

        }
    },
    methods:{
        fakeJWT(){
            localStorage.setItem('JWT','eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJsb2dpbiIsI5iZiI6MTYzNjY1MTMyOCwiZXhwIjoxNjM2NjUxMzg4LCJpYXQiOjE2MzY2NTEzMjgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDQzMDUiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAifQ.ZQ5B5plm2Myh0GqECbfrg9bt0004Avq3dayIcLtiRM')
            this.$store.commit('login')
            this.$store.dispatch('checkValidation')
            }
        }
    }
</script>
