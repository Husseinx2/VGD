<template>
  <b-form v-on:click.prevent>
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
      <label for="genre">Type a new genre and press enter</label>
      <b-form-tags
        input-id="genre"
        placeholder="add genre.."
        v-model="game.Genres"
      ></b-form-tags>
      <!-- platforms -->
      <label for="platforms"
        >Type a new platform and press enter
        <span class="bracket"
          >(NES, Xbox 360, Xbox one, PlayStation 3, PlayStation 4, PlayStation
          5, PC, Nintendo 64, GameCube, Xbox X/S)</span
        ></label
      >
      <b-form-tags
        input-id="platforms"
        placeholder="add platform.."
        v-model="game.Platforms"
      ></b-form-tags>
      <!-- publishers -->
      <label for="Publishers">Type a new Publisher and press enter </label>
      <b-form-tags
        input-id="Publishers"
        placeholder="add Publishers.."
        v-model="game.Publishers"
      ></b-form-tags>

      <!-- developers -->
      <label for="developers">Type a new Developer and press enter </label>
      <b-form-tags
        input-id="developers"
        placeholder="add developers.."
        v-model="game.Developers"
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
        Genres: [],
        Platforms: [],
        Developers: [],
        Publishers:[]
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
      this.game.genre = "";
      this.game.esrbRating = "";
      this.game.releaseDate = "";
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