import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueCookies from 'vue3-cookies'


createApp(App).use(VueCookies,{expireTimes: "7d"}).use(store).use(router).mount('#app')
