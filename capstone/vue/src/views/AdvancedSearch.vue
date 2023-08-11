<template>
  <div class="home">
          <aside id="sidebar-right" title="Filter Options" left shadow >
      <div class="sidebar-work">
        <b-form-group>
       <b-form-radio v-model="selected"  name="some-radios" value="title">Title</b-form-radio>
       <b-form-radio v-model="selected"  name="some-radios" value="esrbRating">ESRB</b-form-radio>
       <b-form-radio v-model="selected"  name="some-radios" value="year">Year</b-form-radio>
       <b-form-radio v-model="selected"  name="some-radios" value="genreName">Genre</b-form-radio>
       <b-form-radio v-model="selected"  name="some-radios" value="platformName">Platform</b-form-radio>
       <b-form-radio v-model="selected"  name="some-radios" value="developerName">Developer</b-form-radio>
       <b-form-radio v-model="selected"  name="some-radios" value="publisherName">Publisher</b-form-radio>
    </b-form-group>
      </div>
    </aside>
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
  selected: "",
  name: "home",
  data() {
    return {
      selected: {},
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
main{
  min-height: 100vh;
}

</style>
