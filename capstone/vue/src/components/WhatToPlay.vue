<template>
  <div class="container">
    <div class="text-center">
      <h1>What To Play</h1>
      <div>
        <div class="d-flex justify-content-center">
          <b-nav-form v-on:click.prevent>
            <b-form-input
              v-if="!$route.meta.hideNavbar"
              size="sm"
              v-model="search"
              placeholder="Filter by title"
            >
            </b-form-input>
          </b-nav-form>
        </div>
      </div>
    </div>
    <div class="container">
      <div class="row justify-content-center">
        <div class="col-md-6" v-for="game in filteredList" :key="game.id">
          <!-- Content for each column using data from 'game' -->
          <game-card :item="game" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import gameService from "../services/GameService.js";
import GameCard from "./GameCard.vue";
export default {
  data() {
    return {
      search: "",
      games: [],
    };
  },
  components: { GameCard },
  computed: {
    filteredList() {
      return this.games.filter((game) => {
        return (
          game.title.toLowerCase().includes(this.search.toLowerCase())
        );
      });
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

<style>
</style>