<template>
    <button class="hamburger--collapse " type="button" :class="{'is-active': burgerActive}" @click="burgerActive = !burgerActive">
        <span class="hamburger-box">
            <span class="hamburger-inner"></span>
        </span>
    </button>  
    <burger-menu v-if="burgerActive" v-bind:mobileSize = mobileSize />
</template>

<script lang="ts">
import BurgerMenu from '@/components/Navigation/BurgerMenu.vue'
import { defineComponent, PropType } from '@vue/runtime-core'

export default defineComponent
({
    props:
    {
        mobileSize:{type: Boolean as PropType<boolean>, required:true}
    },

    components:
    {
        BurgerMenu
    },

    data() {
        return {
            burgerActive:false as boolean
        }
    },
})
</script>

<style lang="css">
    .hamburger-inner, .hamburger-inner::before, .hamburger-inner::after{
        background-color:aliceblue;
    }

    .hamburger--collapse{
        position: fixed;
        left:1%;
        top: 0,5%;
        background: #131524;
        background-color:#131524;
        z-index: 100;
    }
</style>