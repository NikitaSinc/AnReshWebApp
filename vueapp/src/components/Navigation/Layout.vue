<template>
    <burger  v-if="mobileSize" v-bind:mobileSize = mobileSize />
    <burger-menu v-if="!mobileSize" v-bind:mobileSize = mobileSize />
    <header class="header">
        <div class="nav__top">
            <div class="nav__top__user ">
                <p v-if="this.$store.state.user.logged === false">
                    <img src="@/components/images/login__pic.png" alt="login" class="login__pic" />
                    <router-link to="/User/Login">Войти</router-link>   |   <router-link to="/User/Registration">Зарегистрироваться</router-link>
                </p>
                <p v-else>
                    <img src="@/components/images/login__pic.png" alt="login" class="login__pic" />
                    <router-link to="/User/Login">Текущий пользователь: {{this.$cookies.get('Login')}}</router-link>
                </p>
            </div>
        </div>

        <div class="header__logo" v-if="!mobileSize">
            <a href="https://otc.ru" class="header__logo__link">
                <img src="@/components/images/header__logo__back.png" alt="OTC" class="header__logo__back" />
                <img src="@/components/images/header__logo__otc.png" alt="OTC" class="header__logo__otc" />
            </a>
        </div>
    </header>

    <div class="wrapper__content__form" :class="{' mobile': mobileSize}">
        <router-view/>
    </div>
</template>

<script>
    import Burger from '@/components/Navigation/Burger.vue'
    import BurgerMenu from '@/components/Navigation/BurgerMenu.vue'

    export default
    {
        components: 
        {
            Burger,
            BurgerMenu 
        },

        data()
        {
            return{
                mobileSize: false
            }
        },

        methods:
        {
            width(){
                if (window.innerWidth < 1200) 
                {
                    this.mobileSize = true
                }
                else
                {
                    this.mobileSize = false
                }
            }
        },

        created() 
        {
            this.width()
            window.addEventListener('resize', this.width);
        },
    }
</script>

<style>
    .nav__top__user{
        color:snow

    }
</style>