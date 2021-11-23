import {useCookies} from 'vue3-cookies'
const { cookies } = useCookies();
export default
{
    login(state)
        {
          if (cookies.isKey('JWT'))
          state.logged = true;
        },
    logout(state)
        {
          state.logged = false;
          cookies.remove('JWT');
          cookies.remove('Login');
          cookies.remove('Id');
        },
}