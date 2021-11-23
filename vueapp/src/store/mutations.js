export default
{
    setMessage(state, message)
    {
      state.messageVariable = message;
    },
    clearMessage(state)
    {
      state.messageVariable = null;
    },
    setRedirectRoute(state, route)
    {
      state.redirectRoute = route;
      state.redirected = true;
    },
    unsetRedirectRoute(state)
    {
      state.redirectRoute = '';
      state.redirected = false;
    }
}