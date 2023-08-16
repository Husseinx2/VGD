<template>
  <div id="app">
    <div id="nav">
      <header>
      <nav-bar />
      </header>
    </div>
    <body>
    <router-view />
    </body>
    <footer>
    <the-footer />
    </footer>
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
* {
    background-color:whitesmoke;
    min-height: 100hv;
}

</style>