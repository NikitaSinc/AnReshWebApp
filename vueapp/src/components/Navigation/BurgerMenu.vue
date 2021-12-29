<template>
    <nav class="nav__bar" :class="{' mobile' : mobileSize}">
        <ol class="nav__list">
            <li class="nav__list__item"> <router-link to="/FileWork/FileWorkPage">Работа с файлом</router-link></li>

            <li class="nav__list__item">Работа с данными
                <ol>
                    <li class="nav__list__item__sec"><router-link to="/Department/DepartmentForm">Отделы</router-link></li>

                    <li class="nav__list__item__sec"><router-link to="/Employee/EmployeeForm">Сотрудники</router-link></li>

                    <li class="nav__list__item__sec"><router-link to="/Skill/SkillForm">Навыки</router-link></li>
                </ol>
            </li>
        </ol>
    </nav>
</template>
<script>
export default {
    props:{
        mobileSize:Boolean
    }
}
</script>

<style lang="css">
    .nav__bar.mobile{
        margin-top: 72px;
    }
</style>