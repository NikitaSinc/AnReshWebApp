import router from "../router";
export default
{
    async goBack(context){
        if (context.state.redirected === true) 
        {
            router.push(context.state.redirectRoute);
            context.commit('unsetRedirectRoute')
        }
      },
}