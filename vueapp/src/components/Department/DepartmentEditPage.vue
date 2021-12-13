<template>
    <div class="popup">
        <div class="popup__window">
            <div class="form-horizontal">
                <h2>Редактирование отдела</h2>
                <hr />
                <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
                <div class="form-group">
                    <label>Название: </label>
                    <input v-model="department.Name" :placeholder="department.Name">
                </div>

                <div class="form-group">
                    <button type="button" @click="sendData" >Сохранить</button>
                    <a href='#' @click="this.$emit('closeEdit')">Назад</a>
                </div>
            </div>
        </div>
    </div>  
</template>

<script>
export default 
    {
        props: {department:Object},
        methods:
        {
            async sendData()
            {
                const requestOptions = 
                {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({ department: this.department})
                };
                const response = await fetch(this.$store.state.backendPath +"Department/Edit", requestOptions)
                if (response.status === 200)
                {
                    this.$emit('closeEdit')
                }
                else
                {
                    this.$store.commit('setMessage', await response.json())
                }
            }
        },
        beforeMount()
        {
            this.$store.dispatch('user/checkValidation', '/Department/DepartmentForm')
        }
    }
</script>
