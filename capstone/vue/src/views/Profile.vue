<template>
  <div class="Profile" style="font-family: Georgia, 'Times New Roman', Times, serif;">
      <main>
      <h1 style="margin-left:20px; margin-top:12px; margin-bottom:10px; font-family: Georgia, 'Times New Roman', Times, serif; color:#3e75e2;">
          Profile
      </h1>
      <profile-comp style="margin-left:60px; margin-top:12px; margin-bottom:10px; font-family: Georgia, 'Times New Roman', Times, serif;" v-bind:item="$route.params.id" />
      <profile-rating  v-bind:item="$route.params.id"/>
      </main>
  </div>
</template>

<script>
import ProfileComp from '../components/ProfileComp.vue';


export default {
components: {ProfileComp},
}
</script>

<style scoped>

div {
  background-color:whitesmoke;
}
main{
  min-height: 100vh;
}

</style>