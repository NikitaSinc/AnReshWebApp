<template>
    <div v-if="this.$store.state.user.logged === false">
        <login-form></login-form>   
    </div>
    <div v-else>
        <user-profile></user-profile>   
    </div>
</template>
<script>
import LoginForm from './LoginForm.vue'
import UserProfile from './UserProfile.vue'

export default {
    components: { LoginForm, UserProfile }
}
</script>  
