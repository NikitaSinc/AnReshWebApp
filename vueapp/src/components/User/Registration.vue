<template>
    <form>
        <h1>Регистрация</h1>
        <h3 style="warning" v-if="this.$store.state.messageVariable">{{this.$store.state.messageVariable}}</h3>
        <div class="form-group">
            <label >Логин: </label>
            <input v-model="user.login" required >
        </div>
        <div class="form-group">
            <label >Пароль: </label>
            <input type="password" v-model="user.password" required >
        </div>
        <button type="button" @click="registrate">Зарегестрироваться</button>
    </form>
</template>
<script>
export default{
    data(){
        return{
            user:{login:'', password:''}
        }
    },
    methods:{
        async registrate(){
            const requestOptions = {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({user: this.user})
                    }
            const response = await fetch(this.$store.state.backendPath +'User/Registrate', requestOptions);
            if (response.status === 200){
                this.$router.push('Login');
                this.$store.commit('setMessage', 'Пользователь зарегестрирован, введите данные, чтобы войти')
                }
            else if(response.status === 401){this.$store.commit('setMessage', 'Пользователь с таким логином уже существует')}
        }
    },
}
</script>