<template>
  <div class="Profile">
      <main>
      <h1>
          Profile
      </h1>
      <profile-comp v-bind:item="$route.params.id" />
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