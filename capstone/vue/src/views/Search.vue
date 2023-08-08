<template>
  <div>
      <game-card v-for="game in games" v-bind:key="game.id" v-bind:item="game" />
  </div>
</template>

<script>
import GameCard from '../components/GameCard.vue';
import GameService from '../services/GameService';
export default {
  components: { GameCard },
  data() {
    return {
      games: [],
    };
  },
  created() {
      if (this.$route.params.ids.length > 1) {
    this.$route.params.ids.forEach((id) => {
        GameService.getGame(id).then((response) => {
           this.games.push(response.data);
        })
    });
      }
      else  {
          GameService.getGame(this.$route.params.ids).then((response) => {
           this.games.push(response.data);
        })
      }
  },
};
</script>

<style>
</style>