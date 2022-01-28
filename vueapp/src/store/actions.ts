import { RootState } from "./state";
import router from "../router";
import { ActionTree } from "vuex";

enum ActionTypes {
    goBack = "goBack",
    checkValidation = "checkValidation"
}

export const rootActions : ActionTree<RootState, any> = 
{
    async [ActionTypes.goBack](store) : Promise<void>{
        if (store.state.redirected === true) 
        {
            router.push(store.state.redirectRoute);
            store.commit('unsetRedirectRoute')
        }
    },
    
    async [ActionTypes.checkValidation](store, route:string) : Promise<void>
    {

        if (store.state.logged === true)
        {
            const requestOptions = {
                method: "GET",
                headers: { "Content-Type": "application/json" },
                };
            var response = await fetch(store.rootState.backendPath +"User/JWTCheck", requestOptions);
            if (response.status === 200)
            {
                console.log('ok');
            }
            else if (response.status === 401)
            { 
            store.commit('logout')
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
