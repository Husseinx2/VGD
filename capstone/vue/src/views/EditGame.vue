<template>
  <div>
    <h1>
      {{ game.title }}
    </h1>
    <form v-on:click.prevent>
      <label for="title">Title: </label>
      <input v-model="game.title" name="title" type="text" />
      <br />
      <label for="description">Description:</label>
      <br />
      <textarea name="description" v-model="game.description"> </textarea>
      <br />
      <label for="rating">ESBR Rating</label>
      <input type="text" name="rating" v-model="game.esrbRating" />
      <label for="date">Release Date:</label>
      <input v-model="game.releaseDate" type="date" name="date" />
      <br />
      <br />

    <input type="submit" v-on:click="submitEdit" />
    </form>
  </div>
</template>

<script>
import gameService from "../services/GameService.js";
export default {
  data() {
    return {
      game: {},
    };
  },
  methods: {
    submitEdit () {
      console.log("Reached")
      if (this.game) {
        gameService.editGame(this.game).then(() => {
          gameService.list();
          this.$router.push("/");
        });
      }
    },
  },
  created() {
    gameService.getGame(this.$route.params.id).then((response) => {
      this.game = response.data;
    });
  },
};
</script>

<style scoped>
textarea {
  width: 50%;
  height: 150px;
}
</style>