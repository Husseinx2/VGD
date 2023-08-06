<template>
  <div>
    <game-card v-bind:item="game" />
  </div>
</template>

<script>
import GameCard from "../components/GameCard.vue";
import gameService from "../services/GameService";

export default {
  components: { GameCard },
  data() {
    return {
      id: 0,
      game: {},
    };
  },
  created() {
    this.id = this.$route.params.id;
    gameService
      .getGame(this.id)
      .then((response) => {
        this.game = response.data;
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error getting game: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log("Error getting game: unable to communicate to server");
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error getting game: make request");
        }
      });
  },
};
</script>

<style>
</style>