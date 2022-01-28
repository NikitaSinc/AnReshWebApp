<template>
    <div v-if="this.$store.state.logged === false">
        <login-form></login-form>   
    </div>
    <div v-else>
        <user-profile></user-profile>   
    </div>
</template>
<script lang = "ts">
import LoginForm from '@/components/User/LoginForm.vue'
import UserProfile from '@/components/User/UserProfile.vue'
import { defineComponent } from '@vue/runtime-core'


export default defineComponent({
    components: { LoginForm, UserProfile }
})
</script>  
