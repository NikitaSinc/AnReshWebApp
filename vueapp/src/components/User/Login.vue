<template>
    <div v-if="this.$store.state.user.logged === false">
        <login-form></login-form>   
    </div>
    <div v-else>
        <user-profile></user-profile>   
    </div>
</template>
<script>
import LoginForm from '@/components/User/LoginForm.vue'
import UserProfile from '@/components/User/UserProfile.vue'

export default {
    components: { LoginForm, UserProfile }
}
</script>  
