<template>
  <div>
<add-game-form-vue class="addGame" />
 </div>
</template>

<script>
import AddGameFormVue from '../components/AddGameForm.vue'
export default {
components: {AddGameFormVue}
}
</script>

<style scoped>
.addGame{
  width: 50%;
  margin-left:25%;
  padding-right: 10%
}
</style>