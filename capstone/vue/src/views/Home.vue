<template>
  <div class="home">
    <p style="color: #8d0cc2; padding-left: 16px; padding-top: 10px">
      You must be authenticated to see this
    </p>
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
      newGame: {},
      search: "",
      games: [],
    };
  },
  components: { GameCardVue },
  computed: {
    filteredList() {
      return this.games.filter((game) => {
        return game.title.toLowerCase().includes(this.search);
      });
    },
  },

  methods: {
    createNewGame() {
      if (this.newGame.title) {
        gameService
          .addGame(this.newGame)
          .then(() => {
            this.newOwner = {};
            this.loadGames();
          })
          .catch((error) => {
            if (error.response) {
              // error.response exists
              // Request was made, but response has error status (4xx or 5xx)
              console.log("Error adding game: ", error.response.status);
            } else if (error.request) {
              // There is no error.response, but error.request exists
              // Request was made, but no response was received
              console.log("Error adding game: unable to communicate to server");
            } else {
              // Neither error.response and error.request exist
              // Request was *not* made
              console.log("Error adding game: make request");
            }
          });
      }
    },
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
  },
  created() {
    this.loadGames();
  },
};
</script>

<style scoped>
</style>
