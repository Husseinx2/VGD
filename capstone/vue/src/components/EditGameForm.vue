<template>
  <div>
    <b-form class="edit" v-on:click.prevent>
      <label for="title">Title: </label>
      <b-form-input
        v-model="game.title"
        :state="titleState"
        name="title"
        type="text"
        aria-describedby="input-live-feedback"
      />
      <b-form-invalid-feedback id="input-live-feedback"
        >Enter at least 1 letter</b-form-invalid-feedback
      >
      <br />
      <label for="description">Description:</label>
      <br />
      <b-form-textarea
        name="description"
        v-model="game.description"
        :state="descriptionState"
        aria-describedby="input-live-feedback2"
      >
      </b-form-textarea>
      <b-form-invalid-feedback id="input-live-feedback2"
        >Enter at least 1 letter</b-form-invalid-feedback
      >
      <br />
      <label for="esrbRating">ESRB Rating:</label>
      <b-form-select
        :options="options"
        type="text"
        name="rating"
        v-model="game.esrbRating"
        :state="esrbRatingState"
        aria-describedby="input-live-feedback3"
      ></b-form-select>
      <b-form-invalid-feedback id="input-live-feedback3"
        >Please select a valid input</b-form-invalid-feedback
      >
      <br />
      <br />
      <label for="date">Release Date:</label>
      <b-form-datepicker v-model="game.releaseDate" type="date" name="date" />
      <br />
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
        >
        </b-form-tags>
      </div>
      <input type="submit" v-on:click="submitEdit" />
    </b-form>
  </div>
</template>

<script>
import gameService from "../services/GameService.js";
export default {
  data() {
    return {
      options: [
        { value: null, text: "Select ESRB Rating" },
        { value: "eC", text: "Early Childhood (eC)" },
        { value: "E", text: "Everyone (E)" },
        { value: "T", text: "Teen (T)" },
        { value: "M", text: "Mature (M)" },
        { value: "Ao", text: "Adults Only (Ao)" },
        { value: "RP", text: "Rating Pending (RP)" },
      ],
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
    };
  },
  computed: {
    titleState() {
      return this.game.title.length > 0 ? true : false;
    },
    descriptionState() {
      return this.game.description.length > 0 ? true : false;
    },
    esrbRatingState() {
      return this.game.esrbRating != null ? true : false;
    },
  },
    methods: {
      submitEdit() {
        console.log("Reached");
        if (this.game.esrbRating != null && this.titleState) {
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
.bracket {
  font-size: 10px;
  color: grey;
}
</style>