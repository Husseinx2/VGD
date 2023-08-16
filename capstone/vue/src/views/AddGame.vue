<template>
  <div>
<add-game-form class="addGame" />
 </div>
</template>

<script>
import AddGameForm from '../components/AddGameForm.vue'
export default {
components: {AddGameForm}
}
</script>

<style scoped>
.addGame{
  width: 50%;
  margin-left:25%;
  padding-right: 10%
}
</style>