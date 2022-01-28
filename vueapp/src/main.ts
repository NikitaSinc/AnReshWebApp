import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store, { key } from './store/index'
import { VueCookieNext } from 'vue-cookie-next'
import { useStore } from 'vuex'


const app = createApp(App)
app.use(store, key)
app.use(VueCookieNext,{expireTimes: "7d"})
app.use(router)
app.mount('#app')
