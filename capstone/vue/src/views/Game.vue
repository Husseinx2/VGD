<template>
  <div class="game">
    <game-description v-bind:item="game" />
    <game-details v-bind:item="game" />
  </div>
</template>

<script>
import gameService from "../services/GameService";
import GameDescription from '../components/GameDescription.vue';
import GameDetails from '../components/GameDetails.vue';

export default {
  components: { GameDescription, GameDetails},
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
        if (!this.game) {
          this.$router.push({name: 'notFound'});
        }
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
        this.$router.push("/*")
      });

 
  },
};
</script>

<style scoped>
 .game {
   margin:50px
 }
</style>