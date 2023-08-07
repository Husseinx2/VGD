<template>
  <b-form v-on:click.prevent >
    <b-form-group id="title" label="Title" label-for="titleInput">
      <b-form-input
        id="titleInput"
        v-model="game.title"
        type="text"
        placeholder="Game Title"
        required
        :state="titleState"
        aria-describedby="input-live-help input-live-feedback"
      ></b-form-input>
      <b-form-invalid-feedback id="input-live-feedback"
        >Enter at least 1 letter</b-form-invalid-feedback
      >
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
    <label for="description">ESRB Rating:</label>
    <b-form-select
      :options="options"
      type="text"
      name="rating"
      v-model="game.esrbRating"
      required
    ></b-form-select>
    <!-- date -->
     <label for="date">Release Date:</label>
      <b-form-datepicker v-model="game.releaseDate" type="date" name="date" />

    <!-- genre -->
    <div>
      <label for="genre">Type a new genre and press enter</label>
      <b-form-tags
        input-id="genre"
        placeholder="Add genre.."
        v-model="game.genres"
      ></b-form-tags>
      <br />

      <!-- platforms -->
      <label for="platforms"
        >Type a new platform and press enter
        <span class="bracket"
          >(NES, Xbox 360, Xbox one, PlayStation 3, PlayStation 4, PlayStation
          5, PC, Nintendo 64, GameCube, Xbox X/S)</span
        ></label
      >
      <br />

      <b-form-tags
        input-id="platforms"
        placeholder="Add platform.."
        v-model="game.platforms"
      ></b-form-tags>
      <br />

      <!-- publishers -->
      <label for="Publishers">Type a new Publisher and press enter </label>
      <b-form-tags
        input-id="Publishers"
        placeholder="Add Publishers.."
        v-model="game.publishers"
      ></b-form-tags>
      <br />

      <!-- developers -->
      <label for="developers">Type a new Developer and press enter </label>
      <b-form-tags
        input-id="developers"
        placeholder="Add Developers.."
        v-model="game.developers"
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
        esrbRating: null,
        releaseDate: "",
        genres: [],
        platforms: [],
        developers: [],
        publishers: [],
      },
      options: [
        { value: null, text: "Select ESRB Rating" },
        { value: "eC", text: "Early Childhood (eC)" },
        { value: "E", text: "Everyone (E)" },
        { value: "T", text: "Teen (T)" },
        { value: "M", text: "Mature (M)" },
        { value: "Ao", text: "Adults Only (Ao)" },
        { value: "RP", text: "Rating Pending (RP)" },
      ],
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
            console.log("error adding game");
          });
      }
    },
    reset() {
      this.game.title = "";
      this.game.description = "";
      this.game.esrbRating = "";
      this.game.releaseDate = "";
      this.game.genres = [];
      this.game.platforms = [];
      this.game.developers = [];
      this.game.publishers = [];
    },
  },
  computed: {
    titleState() {
      return this.game.title.length > 0 ? true : false;
    },
  },
};
</script>

<style scoped>
.bracket {
  font-size: 10px;
  color: grey;
}
</style>