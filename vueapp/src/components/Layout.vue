<template>
        <header class="header">

            <nav class="nav__bar">
                <ol class="nav__list">

                    <li class="nav__list__item"> <router-link to="/FileWork/FileWorkPage">Работа с файлом</router-link></li>

                    <li class="nav__list__item">Работа с данными
                        <ol>
                            <li class="nav__list__item__sec"><router-link to="/Department/DepartmentForm">Отделы</router-link></li>

                            <li class="nav__list__item__sec"><router-link to="/Employee/EmployeeForm">Сотрудники</router-link></li>
                        </ol>
                    </li>
                </ol>
            </nav>

            <div class="nav__top">
                <div class="nav__top__user">
                    <p v-if="this.$store.state.logged === false">
                        <img src="./images/login__pic.png" alt="login" class="login__pic" />
                        <router-link to="/User/Login">Войти</router-link>   |   <router-link to="/User/Registration">Зарегистрироваться</router-link>
                    </p>
                    <p v-else>
                        <img src="./images/login__pic.png" alt="login" class="login__pic" />
                        <router-link to="/User/Login">Текущий пользователь: {{this.$store.state.currentLogin}}</router-link>
                    </p>
                </div>
            </div>

            <div class="header__logo">
                <a href="https://otc.ru" class="header__logo__link">
                    <img src="./images/header__logo__back.png" alt="OTC" class="header__logo__back" />
                    <img src="./images/header__logo__otc.png" alt="OTC" class="header__logo__otc" />
                </a>
            </div>
        </header>

        <div class="wrapper__content__form">
            <router-view/>
        </div>
</template>
<script lang="ts">

</script>
<style>
    .nav__top__user{
        color:snow

    }
</style>