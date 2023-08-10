<template>
  <div class="Profile">
      <main>
      <h1>
          Profile
      </h1>
      <profile-comp v-bind:item="$store.state.user.userId" />
      <profile-rating  v-bind:item="$store.state.user.userId"/>
      </main>
  </div>
</template>

<script>
import ProfileComp from '../components/ProfileComp.vue';
import ProfileRating from '../components/ProfileRating.vue';

export default {
components: {ProfileComp, ProfileRating},
}
</script>

<style>
div {
  background-color:whitesmoke;
}

</style>