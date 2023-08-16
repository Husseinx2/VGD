<template>
  <div>
<edit-game-form class="editGame" />
 </div>
</template>

<script>
import EditGameForm from '../components/EditGameForm.vue'
export default {
components: {EditGameForm}
}
</script>

<style scoped>
.editGame{
  width: 50%;
  margin-left:25%;
  padding-right: 10%
}
</style>