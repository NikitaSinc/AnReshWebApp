import { createStore } from 'vuex'
import router from '../router';

export default createStore({
  state: {
    backendPath: "http://localhost:44305/",
    //используй лучше куки, их будешь пробрасывать на бэк, чтобы там валиднуть авторизированного пользака
    currentJWT:localStorage.getItem('JWT'),
    currentLogin:localStorage.getItem('Login'),
    currentId:localStorage.getItem('Id'),
    logged:false,
    messageVariable:''
  },
  mutations: {
    login(state){
      state.currentJWT = localStorage.getItem('JWT');
      state.currentLogin = localStorage.getItem('Login');
      state.currentId = localStorage.getItem('Id');
      state.logged = true;
    },
    logout(state){
      state.currentJWT = null;
      state.currentId = null;
      state.currentLogin = null;
      state.logged = false;
      localStorage.removeItem('JWT');
      localStorage.removeItem('Login');
      localStorage.removeItem('Id');
    },
    setMessage(state, message){
      state.messageVariable = message;
    }
  },
  actions: {
    async checkValidation(){
      const requestOptions = {
              method: "POST",
              headers: { "Content-Type": "application/json" },
              body: JSON.stringify({JWT: this.state.currentJWT})
          };
      var response = await fetch(this.state.backendPath +"User/JWTCheck", requestOptions);
      if (response.status == 200){console.log('ok'); return true}
      else if (response.status == 401){
          this.commit('logout')
          this.commit('setMessage', 'Невалидный токен, войдите в аккаунт для продолжения')
          router.push({name: 'Login', params:{isRedirected: true}});
      }
    },
  },
  modules: {
  }
})
