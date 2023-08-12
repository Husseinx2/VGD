<template>
  <div class="home">
    <b-alert :show="gameAdded" variant="success" role="alert" dismissible>
      Game Successfully Added
    </b-alert>

    <b-alert :show="gameEdited" variant="success" role="alert" dismissible>
      Game Successfully Edited
    </b-alert>

    <b-alert :show="gameDeleted" variant="success" dismissible role="alert">
      Game Successfully Deleted
    </b-alert>

    <section class="container d-flex justify-content-center">
      <b-nav-form v-on:click.prevent>
        <b-form-input
          v-if="!$route.meta.hideNavbar"
          size="sm"
          v-model="search"
          placeholder="Search Games"
        >
        </b-form-input>
      </b-nav-form>
    </section>

    <section>
      <game-card-vue
        v-for="game in filteredList"
        v-bind:key="game.id"
        v-bind:item="game"
      />
    </section>
  </div>
</template>

<script>
import GameCardVue from "../components/GameCard.vue";
import gameService from "../services/GameService.js";
export default {
  name: "home",
  data() {
    return {
      search: "",
      games: [],
    };
  },
  components: { GameCardVue },
  computed: {
    filteredList() {
      return this.games.filter((game) => {
        return game.title.toLowerCase().includes(this.search) || game.description.toLowerCase().includes(this.search) ;
      });
    },
    gameAdded() {
      console.log("reached gameAdded");
      return this.$store.state.gameAdded;
    },
    gameEdited() {
      console.log("reached gameEdited");
      return this.$store.state.gameEdited;
    },
    gameDeleted() {
      return this.$store.state.gameDeleted;
    },
  },

  methods: {
    loadGames() {
      gameService
        .listGames()
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
  },
  created() {
    this.loadGames();
  },
};
</script>

<style scoped>
</style>
