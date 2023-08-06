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
     gameService.getGame(this.id)
     .then((response) => {
       this.game = response.data; 
      })
  },
         
}

</script>

<style>
</style>