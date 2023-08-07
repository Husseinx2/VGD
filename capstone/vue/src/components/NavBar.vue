<template>
    <header>
      <b-navbar v-show="$store.state.user.role == 'user' || $store.state.user.role == 'admin'">
        <b-navbar-brand>VGD</b-navbar-brand>
        <b-nav-form>
          <b-form-input size="sm" placeholder="Search"></b-form-input>
          <b-button size="sm" type="submit">Search</b-button>
        </b-nav-form>
        <b-navbar-nav>
        <b-nav-item class="logout" href="logout">Logout</b-nav-item>
        </b-navbar-nav>
      </b-navbar>
    </header>
</template>

<script>
import gameService from "../services/GameService";
export default {
  data() {
    return {
      newGame: {},
      search: "",

      games: [],
    };
  },
    computed: {
    filteredList() {
      return this.games.filter((game) => {
        return game.title.toLowerCase().includes(this.search);
      });
    },
  },
    methods: {
        loadGames() {
      gameService
        .list()
        .then((response) => {
          console.log("Reached created in Home.vue");
          console.log(response);
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
    }
}
</script>

<style>

</style>