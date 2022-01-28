import { VueCookieNext } from "vue-cookie-next";
import { MutationTree } from "vuex";
import { RootState } from "./state";

enum MutationTypes {
  clearMessage = "clearMessage",
  setMessage = "setMessage",
  setRedirectRoute = "setRedirectRoute",
  unsetRedirectRoute = "unsetRedirectRoute",
  login = "login",
  logout = "logout"
}

export type Mutations = {
  [MutationTypes.setMessage](state: RootState, message:string) : void
  [MutationTypes.clearMessage](state: RootState) : void
  [MutationTypes.setRedirectRoute](state: RootState, route:string) : void
  [MutationTypes.unsetRedirectRoute](state: RootState) : void
  [MutationTypes.login](state: RootState) : void
  [MutationTypes.logout](state : RootState) : void
}

export const rootMutations : MutationTree<RootState> & Mutations =
{
  [MutationTypes.setMessage](state: RootState, message:string) : void
  {
    state.messageVariable = message;
  },
  [MutationTypes.clearMessage](state: RootState) : void
  {
    state.messageVariable = "";
  },
  [MutationTypes.setRedirectRoute](state: RootState, route:string) : void
  {
    state.redirectRoute = route;
    state.redirected = true;
  },
  [MutationTypes.unsetRedirectRoute](state: RootState) : void
  {
    state.redirectRoute = "";
    state.redirected = false;
  },
  [MutationTypes.login](state: RootState) : void
    {
      if (VueCookieNext.isCookieAvailable('JWT'))
      state.logged = true;
    },
  [MutationTypes.logout](state: RootState) : void
    {
      state.logged = false;
      VueCookieNext.removeCookie('JWT');
      VueCookieNext.removeCookie('Login');
      VueCookieNext.removeCookie('Id');
    },

}
