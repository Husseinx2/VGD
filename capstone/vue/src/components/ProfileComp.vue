<template>
  <section class="ProfileComp">
       <h1> {{ $store.state.user.username }}</h1>
  </section>
</template>

<script>
export default {
  name: "profileComp",
};
</script>

<style scoped>
section.Profile {
  flex-wrap: wrap;
  display: inline-block;
  height: 120px;
  min-width: 300px;
  border: 2px solid black;
  border-radius: 10px;
  margin: 20px;
}
</style>