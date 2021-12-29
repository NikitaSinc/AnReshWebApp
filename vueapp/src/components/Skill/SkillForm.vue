<template>
    <h1>Стажировка в Аналитических Решениях</h1>
    <h2>Навыки</h2>
    <h3 style="warning" v-if="this.$store.state.messageVariable !== null">{{this.$store.state.messageVariable}}</h3>
    <skill-creation-page 
        v-bind:skill = skill
        v-if="showCreate"
        @closeCreate="closeCreate"    
    />
    <skill-edit-page 
        v-bind:skill = skill
        v-if="showEdit"
        @closeEdit="closeEdit"    
    />

    <table class="table">
        <tr>
            <th>
                ID
            </th>
            <th>
                Отдел
            </th>
            <th>
                Операции
            </th>
        </tr>

        <tr v-for="skill in skillList" :key = "skill.Skill_name">
            <td>
                {{skill.Id}}
            </td>
            <td>
                {{skill.Skill_name}}
            </td>
            <td>
                <a href='#' @click="() => {this.skill = skill; showEdit = true}">Изменить</a> | <a href='#' @click="deleteSkill(skill)">Удалить</a>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                Новый навык
            </td>

            <td>
                <a href='#' @click="()=>{this.skill={}; showCreate = true}">Добавить</a>
            </td>
        </tr>
    </table>
</template>

<script>
    import SkillCreationPage from '@/components/Skill/SkillCreationPage.vue'
    import SkillEditPage from '@/components/Skill/SkillEditPage.vue'
    

    export default  
    {
        components: { SkillCreationPage, SkillEditPage },

        data()
        {
            return {
                showEdit: false,
                showCreate: false,
                skill:{},
                skillList:[{}]
            }
        },

        methods: 
        {
            closeCreate()
            {
                this.skillList.push(this.skill)
                this.showCreate = false
            },

            closeEdit()
            {
                this.showEdit = false
            },

            async loadData()
            {
                const response = await fetch(this.$store.state.backendPath +"Skill/SendData")
                const serverData = await response.json() 
                this.skillList = serverData;
            },
    
            async deleteSkill(skill)
            {
                    this.$store.dispatch('user/checkValidation', '/Department/DepartmentForm');
                    if (this.$store.state.user.logged === true)
                    {
                        const requestOptions = {
                            method: "POST",
                            headers: { "Content-Type": "application/json" },
                            body: JSON.stringify({id: skill.Id})
                        };
                        const response = await fetch(this.$store.state.backendPath +"Skill/Delete", requestOptions)
                    
                        if (response.status === 200)
                        {
                            this.skillList.splice(this.skillList.indexOf(skill))
                        }
                        else
                        {
                            this.$store.commit('setMessage', await response.json())
                        }
                    }
            },
        },
        mounted()
        {
            this.loadData()
        },

    }
</script>