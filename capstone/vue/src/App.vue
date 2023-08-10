<template>
  <div id="app">
    <div id="nav">
      <nav-bar />
    </div>
    <router-view />
    <the-footer />
  </div>
</template>
<script>

import NavBar from "./components/NavBar.vue";
import TheFooter from './components/TheFooter.vue';

export default {
  components: { NavBar, TheFooter }
};
</script>

<style scoped>
div {
  background-color:whitesmoke;
}
html{
  background-color:whitesmoke;
}
</style>