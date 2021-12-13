import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import VueCookies from 'vue3-cookies'
import Multiselect from '@vueform/multiselect'
Vue.component('multiselect', Multiselect)


createApp(App).use(VueCookies,{expireTimes: "7d"}).use(store).use(router).mount('#app')
