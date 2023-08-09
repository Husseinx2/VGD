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
      <router-link to="/Advancedsearch">
        Advanced searched
      </router-link>
      <b-alert v-model="showAlert" variant="danger" dismissible>
        Not Found!
      </b-alert>

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
      showAlert: false,
    };
  },
  methods: {
    searchGames() {
      if (this.search) {
        console.log("reached");
        this.games.forEach((game) => {
          if (
            game.title.toUpperCase().includes(this.search.toUpperCase()) ||
            game.description.toUpperCase().includes(this.search.toUpperCase())
          ) {
            this.ids.push(game.id);
          }
        });
        if (this.ids.length == 0) {
          this.showAlert = true;
        } else {
          let ids = this.ids;
          this.ids = [];
          this.$router
            .push({ name: "search", params: { ids: ids } })
            .catch(() => {});
          this.search = "";
        }
        this.ids = [];
      }
    },

    //loading games
    loadGames() {
      GameService.list()
        .then((response) => {
          console.log("Reached created in nav");
          this.games = response.data;
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error loading games: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log("Error loading games: unable to communicate to server");
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error loading games: make request");
          }
        });
    },
  },
  created() {
    this.loadGames();
  },
};
</script>

<style scoped>
header {
  background-color: #c5e2ff;
}
</style>