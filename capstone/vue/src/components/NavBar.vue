<template>
  <header>
    <b-navbar
      v-show="
        $store.state.user.role == 'user' || $store.state.user.role == 'admin'
      "
    >
      <b-navbar-brand href="/">VGD</b-navbar-brand>
      <b-nav-form v-on:click.prevent>
        <b-form-input
          v-if="!$route.meta.hideNavbar"
          type="search"
          size="sm"
          v-model="search"
          placeholder="Search"
        >
        </b-form-input>
        <b-button
          v-if="!$route.meta.hideNavbar"
          v-on:click="searchGames"
          size="sm"
          type="submit"
          >Search</b-button
        >
      </b-nav-form>
      <b-navbar-nav class="ml-auto">
        <b-nav-item
          class="addGame"
          href="addGame"
          v-show="$store.state.user.role == 'admin'"
          >Add a Game</b-nav-item
        >
        <b-nav-item class="logout" href="logout">Logout</b-nav-item>
      </b-navbar-nav>
    </b-navbar>
  </header>
</template>

<script>
import GameService from "../services/GameService";
export default {
  data() {
    return {
      search: "",
      ids: [],
      games: [],
    };
  },
  methods: {
    searchGames() {
      if (this.search) {
        GameService.list().then((response) => {
          this.games.push(response.data);
        });
        console.log("reached");
        console.log(this.games.forEach((game) => {console.log(game.title)}))
      }
    },
  },
};
</script>

<style scoped>
header {
  background-color: #c5e2ff;
}
</style>