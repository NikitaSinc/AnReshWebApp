<template>
    <form method="post" action="~/Home/Index">
    <h1>Стажировка в Аналитических Решениях</h1>
    <h2>Работа с файлом</h2>
    <div class="file__form">
        <textarea type="text" contenteditable="true" name="newDescription" v-model="file"
                  placeholder="Введите текст или обновите поле из файла"></textarea>
        <br />
        <div class="button__group__index">
            <button type="button" @click="loadData">Обновить из файла</button>
            <button type="button" @click="sendData">Записать в файл</button>
        </div>
    </div>
</form>
</template>
<script lang="ts">

import { defineComponent } from "@vue/runtime-core"

export default defineComponent
({
    data() {
        return{
            file:'' as string
        }
    },

    methods:{
        async loadData(): Promise<void>
        {
            const response = await fetch(this.$store.state.backendPath +"FileWork/GetFile")
            if (response.status === 200)
            {
                const serverData = await response.json() 
                this.file = serverData;
            }
            else 
            {
                this.file = 'Ошибка при загрузке файла'
            }
        },

        async sendData(): Promise<void>
         {
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({data: this.file})
            };
            fetch(this.$store.state.backendPath +"FileWork/SetFile", requestOptions)
        }
    }
})
</script>
