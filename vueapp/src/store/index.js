import { createStore } from 'vuex'
import state from './state.js'
import mutations from './mutations.js'
import actions from './actions.js'
import user from './user/index.js'
import routes from './routes/index.js'
import  VueCookies  from 'vue3-cookies/dist/interfaces'

export default createStore({
  state,
  mutations,
  actions,
  modules: 
  {
    user,
    routes
  }
})
