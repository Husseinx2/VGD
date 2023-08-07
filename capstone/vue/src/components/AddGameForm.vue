<template>
  <b-form v-on:click.prevent>
    <b-form-group id="title" label="Title" label-for="titleInput">
      <b-form-input
        id="titleInput"
        v-model="game.title"
        type="text"
        placeholder="Game Title"
        required
      ></b-form-input>
    </b-form-group>
    <b-form-group
      id="description"
      label="Description"
      label-for="descriptionInput"
    >
      <b-form-input
        id="descriptionInput"
        v-model="game.description"
        type="text"
        placeholder="Game Description"
        required
      ></b-form-input>
    </b-form-group>
    <!-- esrb rating -->
    <b-form-group id="rating" label="Rating" label-for="ratingInput">
      <b-form-select
        id="ratingInput"
        v-model="game.esrbRating"
        :options="ratings"
        required
      ></b-form-select>
    </b-form-group>
    <!-- date -->
    <b-form-group id="date" label="Description" label-for="dateInput">
      <b-form-input
        id="dateInput"
        v-model="game.releaseDate"
        type="date"
        placeholder="Game Release Date"
        required
      ></b-form-input>
    </b-form-group>

    <!-- genre -->
    <div>
      <label for="tags-basic">Type a new genre and press enter</label>
      <b-form-tags
        input-id="tags-basic"
        placeholder="add genre.."
        v-model="game.genre"
      ></b-form-tags>
    </div>
    <br />
    <br />
    <b-button type="submit" v-on:click="addGame" variant="primary"
      >Submit</b-button
    >
    <b-button type="reset" v-on:click="reset" variant="danger">Reset</b-button>
  </b-form>
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
        esrbRating: "",
        releaseDate: "",
        genre: [],
      },
      ratings: ["M", "E"],
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
            console.log("error adding game")
          });
      }
    },
    reset() {
      this.game.title = "";
      this.game.description = "";
      this.game.genre = "";
      this.game.esrbRating = "";
      this.game.releaseDate = "";
    },
  },
};
</script>

<style>
</style>