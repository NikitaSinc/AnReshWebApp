<template>
    <div v-if="this.$store.state.logged === false">
        <login-form v-bind:isRedirected="this.isRedirected"></login-form>   
    </div>
    <div v-else>
        <user-profile></user-profile>   
    </div>
</template>
<script>
import LoginForm from './LoginForm.vue'
import UserProfile from './UserProfile.vue'

export default {
    
    components: { LoginForm, UserProfile },
    data() {return{}
    },
    methods:{
    }
}
</script>  
