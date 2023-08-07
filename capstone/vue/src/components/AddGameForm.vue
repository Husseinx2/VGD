<template>
  <b-form v-on:click.prevent>
    <b-form-group id="title" label="Title" label-for="titleInput">
      <b-form-input
        id="titleInput"
        v-model="game.title"
        type="text"
        placeholder="Game Title"
        required
        :state="formTitle"
        @change="titleState"
        aria-describedby="input-live-feedback"
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
        :state="formComplete"
        @change="descriptionState"
        aria-describedby="input-live-feedback2"
        type="text"
        placeholder="Game Description"
        required
      ></b-form-input>
      <b-form-invalid-feedback id="input-live-feedback2"
        >Enter at least 1 letter</b-form-invalid-feedback
      >
    </b-form-group>
    <!-- esrb rating -->
    <label for="description">ESRB Rating:</label>
    <b-form-select
      :options="options"
      type="text"
      name="rating"
      v-model="game.esrbRating"
      :state="formComplete"
      @change="esrbRatingState"
      aria-describedby="input-live-feedback3"
      required
    ></b-form-select>
    <b-form-invalid-feedback id="input-live-feedback3"
      >Enter a valid rating</b-form-invalid-feedback
    >
    <!-- date -->
    <label for="date">Release Date:</label>
    <b-form-datepicker
      v-model="game.releaseDate"
      :state="formComplete"
      @change="releaseDateState"
      aria-describedby="input-live-feedback4"
      type="date"
      name="date"
    />
    <b-form-invalid-feedback id="input-live-feedback4"
      >Enter a valid date</b-form-invalid-feedback
    >
    <!-- genre -->
    <div>
      <label for="genre">Type a new genre and press enter</label>
      <b-form-tags
        input-id="genre"
        placeholder="Add genre.."
        v-model="game.genres"
        :state="formComplete"
        @change="genresState"
        aria-describedby="input-live-feedback5"
      ></b-form-tags>
      <b-form-invalid-feedback id="input-live-feedback"
        >Please enter valid genre(s)</b-form-invalid-feedback
      >
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
        :state="formComplete"
        @change="platformsState"
        aria-describedby="input-live-feedback6"
      ></b-form-tags>
      <b-form-invalid-feedback id="input-live-feedback6"
        >Please enter valid platform(s)</b-form-invalid-feedback
      >
      <br />

      <!-- publishers -->
      <label for="Publishers">Type a new Publisher and press enter </label>
      <b-form-tags
        input-id="Publishers"
        placeholder="Add Publishers.."
        v-model="game.publishers"
        :state="formComplete"
        @change="publisherState"
        aria-describedby="input-live-feedback7"
      ></b-form-tags>
      <b-form-invalid-feedback id="input-live-feedback7"
        >Please enter valid publisher(s)</b-form-invalid-feedback
      >
      <br />

      <!-- developers -->
      <label for="developers">Type a new Developer and press enter </label>
      <b-form-tags
        input-id="developers"
        placeholder="Add Developers.."
        v-model="game.developers"
        :state="formComplete"
        @change="developersState"
        aria-describedby="input-live-feedback8"
      ></b-form-tags>
      <b-form-invalid-feedback id="input-live-feedback8"
        >Please enter valid developer(s)</b-form-invalid-feedback
      >
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
      formComplete:null,
      formTitle: null,
      formDescription: null,
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
    titleState() {
      return this.game.title.length > 0 ? this.formTitle = true : false;
    },
    addGame() {
      if (
        this.titleState &&
        this.descriptionState &&
        this.esrbRatingState &&
        this.releaseDateState &&
        this.genresState &&
        this.platformsState &&
        this.publisherState &&
        this.developersState
      ) {
        GameService.addGame(this.game)
          .then(() => {
            this.game = {};
            this.$router.push({ name: "home" });
          })
          .catch(() => {
            console.log("error adding game");
          });
      }
      else (
        this.formComplete = false
      )
    },
    reset() {
      this.game.title = "";
      this.game.description = "";
      this.game.esrbRating = null;
      this.game.releaseDate = "";
      this.game.genres = [];
      this.game.platforms = [];
      this.game.developers = [];
      this.game.publishers = [];
    },
  },
  computed: {

    descriptionState() {
      return this.game.description.length > 0 ? true : false;
    },
    esrbRatingState() {
      return this.game.esrbRating != null ? true : false;
    },
    releaseDateState() {
      return this.game.releaseDate != "" ? true : false;
    },
    genresState() {
      return this.game.genres != "" ? true : false;
    },
    platformsState() {
      return this.game.platforms != "" ? true : false;
    },
    publisherState() {
      return this.game.publishers != "" ? true : false;
    },
    developersState() {
      return this.game.developers != "" ? true : false;
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