<template>
      <header>
      <table>
        <td>
      <p>VGD</p>
        </td>
        <td>
     <input placeholder="Search"  type="text" v-model="search" 
        v-for="game in filteredList"
        v-bind:key="game.id"
        v-bind:item="game"/>
        </td>
        <td>
          <button type="submit">Submit</button>
        </td>
      </table>
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