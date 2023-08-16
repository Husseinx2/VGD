<template>
  <div>
    <div v-for="game in games" :key="game.gameId">  
      <game-card :item="game" />
    </div>
  </div>
</template>

<script>
import GameCard from "./GameCard.vue";
import gameService from "../services/GameService";
export default {
  components: {GameCard},
  props: ["item"],
  data() {
    return {
      games: {},
    };
  },

  methods: {
    getGames() {
      this.games = [];
      this.item.forEach((entry) => {
        gameService.getGame(entry.gameId).then((response) => {
          const game = response.data;
          console.log(game);
          this.games.push(game);
        });
      });
    },
  },
  created() {
    this.getGames();
   },
};
</script>

<style>

</style>