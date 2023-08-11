<template>
  <div>
    <section>
      <game-card
        v-for="game in searchResults"
        v-bind:key="game.id"
        v-bind:item="game"
      />
    </section>
  </div>
</template>

<script>
import GameCard from "./GameCard.vue";
import gameService from "../services/GameService";
export default
{
  components: { GameCard }, 
  props: ["item"],  
      data() {
    return {
      searchResults: [],
    };
  },

 created() {
     gameService.search(this.item)
     .then(response => this.searchResults = response.data)
     .catch(() => {
         console.log("Server not found");
         this.searchResults = [];
     })
 }
}
</script>

<style>
</style>