<template>
<div id="app">
    <form>
        <h1>Авторизация</h1>
        <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
        <div class="form-group">
            <label >Логин: </label>
            <input v-model="loginValue" required >
        </div>
        <div class="form-group">
            <label >Пароль: </label>
            <input type="password" v-model="passwordValue" required >
        </div>
        <button type="button" @click="loginCheck">Войти</button>
    </form>

    
</div>
</template>

<script>
export default {

    data(){
        return{
            loginValue: '',
            passwordValue: ''
        }
    },
    methods:
    {
        async loginCheck(){
            const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({login: this.loginValue, password: this.passwordValue})
                    }
            const response = await fetch(this.$store.state.backendPath +'User/LoginCheck', requestOptions);
            const serverData = await response.json();
            const JWT = serverData.JWT;
            console.log(serverData)
            if(serverData.StatusCode === 401){this.$store.commit('setMessage','Пользователь не существует!') }
            else{
                this.showMessage = false;
                localStorage.setItem('Login', serverData.Login)
                localStorage.setItem('Id', serverData.Id)
                localStorage.setItem('JWT', JWT);
                this.$store.commit('login');
                if (this.isRedirected) this.$router.go(-1);
            }
        }
    },
    unmounted(){
        this.$store.commit('setMessage',null) 
    }
}
</script>
<style>
    .warning{
        color:red
    }
</style>