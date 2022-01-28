<template>
    <form>
        <h1>Авторизация</h1>
        <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{$store.state.messageVariable}}</h3>
        <div class="form-group">
            <label >Логин: </label>
            <input v-model="user.login" required >
        </div>
        <div class="form-group">
            <label >Пароль: </label>
            <input type="password" v-model="user.password" required >
        </div>
        <button type="button" @click="loginCheck">Войти</button>
    </form>
</template>

<script lang = "ts">

import store from "@/store"
import {User} from "@/components/User/types"
import { defineComponent } from "@vue/runtime-core"
import { VueCookieNext } from "vue-cookie-next"

export default defineComponent(
{
    data()
    {
        return{
            user: {login: '', password: ''} as User
        }
    },
    
    methods:
    {
        async loginCheck(): Promise<void>{
            if (this.user.login === '' || this.user.password === '')
            {
                this.$store.commit('setMessage', 'Заполните все поля для продолжения')
            }
            const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({user: this.user})
                    }
            const response = await fetch(this.$store.state.backendPath +'User/LoginCheck', requestOptions);
            const serverData = await response.json();
            if (response.status === 200)
            {
                VueCookieNext.setCookie('Login', serverData.Login)
                VueCookieNext.setCookie('Id', serverData.Id)
                VueCookieNext.setCookie('JWT', serverData.JWT)
                this.$store.commit('login')
                this.$store.dispatch('goBack')
            }
            else if(response.status === 401)
            {
                this.$store.commit('setMessage','Пользователь не существует!') 
            }
            else 
            {
                this.$store.commit('setMessage','Ошибка при авторизации пользователя') 
            }
        }
    }
})

</script>