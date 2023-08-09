<template>
  <div>
<edit-game-form-vue class="editGame" />
 </div>
</template>

<script>
import EditGameFormVue from '../components/EditGameForm.vue'
export default {
components: {EditGameFormVue}
}
</script>

<style scoped>
.editGame{
  width: 50%;
  margin-left:25%;
  padding-right: 10%
}
</style>