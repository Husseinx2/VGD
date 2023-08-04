<template>
  <form v-on:click.prevent>
    <input type="text" name="title" placeholder="title" v-model="game.title" />
    <input
      type="text"
      name="description"
      placeholder="description"
      v-model="game.description"
    />
    <input
      type="text"
      name="esrbrRating"
      placeholder="rating"
      v-model="game.ESRBRating"
    />
    <input type="date" name="releaseDate" v-model="game.releaseDate" />
    <br />
    <input type="submit" v-on:click="addGame" />
  </form>
</template>

<script>
import GameService from "../services/GameService";
export default {
  data() {
    return {
      game: {
        id: 0,
        title: "",
        description: "",
        ESRBRating: "",
        releaseDate: "",
      },
    };
  },
  methods: {
    addGame() {
      if (this.game.title) {
        GameService.addGame(this.game)
          .then(() => {
            this.game = {};
            this.$router.push({ name: "home" });
          })
          .catch(() => {
          });
      }
    },
  },
};
</script>

<style>
</style>