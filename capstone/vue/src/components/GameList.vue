<template>
 <game-card class="gamecard"  v-bind:item="game" v-bind:key="game.id"/>
</template>

<script>
import GameService from "../services/GameService";
import GameCard from './GameCard.vue';
export default {
  components: { GameCard },
  props: ["item"],
  data() {
    return {
      game: {},
    };
  },
  methods: {
    getGame() {
      GameService.getGame(this.item).then((response) => {
        this.game = response.data;
      });
    },
  },
  created(){
      this.getGame();
  }
};
</script>

<style>
.gamecard {
  height: 250px;
}
</style>