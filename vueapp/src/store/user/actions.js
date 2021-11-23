import router from "../../router";
import store from "../index.js";

export default 
{
    async checkValidation(context, route)
    {

        if (context.state.logged === true)
        {
            const requestOptions = {
                method: "GET",
                headers: { "Content-Type": "application/json" },
                };
            var response = await fetch(store.state.backendPath +"User/JWTCheck", requestOptions);
            if (response.status === 200)
            {
                console.log('ok'); return true
            }
            else if (response.status === 401)
            { 
            context.commit('logout')
            store.commit('setMessage', 'Невалидный токен, войдите в аккаунт для продолжения')
            store.commit('setRedirectRoute', route)
            router.push('/User/Login');
            }
        }
        else
        {
            store.commit('setMessage', 'Для продолжения необходимо войти в аккаунт')
            store.commit('setRedirectRoute', route)
            router.push('/User/Login');
        }
    },
}