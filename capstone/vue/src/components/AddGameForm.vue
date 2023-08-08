<template>
  <b-form @submit.stop.prevent="onSubmit">
    <b-form-group class="titleInput">
      <label>Title</label>
      <b-form-input
        class="form__input"
        id="titleInput"
        v-model="$v.game.title.$model"
        :state="validateTitle('title')"
        type="text"
        placeholder="Game Title"
        required
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
      <b-form-textarea
        id="descriptionInput"
        v-model.trim="$v.game.description.$model"
        :state="validateDescription('description')"
        aria-describedby="input-live-feedback2"
        type="text"
        placeholder="Game Description"
        required
      ></b-form-textarea>
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
      v-model="$v.game.esrbRating.$model"
      :state="validateEsrbRating('esrbRating')"
      aria-describedby="input-live-feedback3"
      required
    ></b-form-select>
    <b-form-invalid-feedback id="input-live-feedback3"
      >Enter a valid rating</b-form-invalid-feedback
    >
    <!-- date -->
    <label for="date">Release Date:</label>
    <b-form-datepicker
      v-model="$v.game.releaseDate.$model"
      :state="validateReleaseDate('releaseDate')"
      aria-describedby="input-live-feedback4"
      type="date"
      name="date"
      required
    />
    <b-form-invalid-feedback id="input-live-feedback4"
      >Enter a valid date</b-form-invalid-feedback
    >

    <!-- Image -->
    <b-form-group id="urlInput" label="Image Url" label-for="urlInput">
      <b-form-input
        id="urlInput"
        v-model="game.imageUrl"
        :state="urlState"
        aria-describedby="input-live-feedback2"
        type="text"
        placeholder="Image Url"
        required
      ></b-form-input>
      <b-form-invalid-feedback id="input-live-feedback2"
        >Enter at least 1 letter</b-form-invalid-feedback
      >
    </b-form-group>

    <!-- genre -->
    <div>
      <label for="genre">Select genre(s):</label>
      <multiselect
        required
        input-id="genre"
        placeholder="Pick some"
        :options="allGenres"
        v-model="$v.game.genres.$model"
        :multiple="true"
        :close-on-select="false"
        track-by="value"
        :custom-label="nameReturn"
        label="value"
        :taggable="true"
        @tag="addTag"
        aria-describedby="input-live-feedback5"
        tag-placeholder="Add this as new tag"
      ></multiselect>
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
        :state="platformsState"
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
        :state="publisherState"
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
        :state="developersState"
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
import Multiselect from "vue-multiselect";
import { required, minLength, alpha } from "vuelidate/lib/validators";
import GameService from "../services/GameService";
export default {
  components: { Multiselect },
  data() {
    return {
      game: {
        id: 0,
        title: "",
        description: "",
        esrbRating: null,
        releaseDate: "",
        imageUrl: "",
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
      allGenres: [
        { value: "Platformer" },
        { value: "Third-Person Shooter" },
        { value: "First-Person Shooter" },
        { value: "Action" },
        { value: "Adventure" },
        { value: "Puzzle" },
        { value: "Open-World" },
        { value: "Horror" },
        { value: "Sports" },
        { value: "Role-Playing" },
        { value: "Fighting" },
      ],
    };
  },
  validations: {
    game: {
      title: {
        required,
        minLength: minLength(1),
      },
      description: {
        required,
        minLength: minLength(1),
      },
      esrbRating: {
        required,
        alpha,
      },
      releaseDate: {
        required,
      },
      genres: {
        required,
      },
    },
  },
  methods: {
    addTag(newTag) {
      const tag = {
        value: newTag,
      };
      this.allGenres.push(tag);
    },
    nameReturn({ value }) {
      return `${value}`;
    },
    onSubmit() {
      this.$v.game.$touch();
      if (this.$v.game.$anyError) {
        return;
      }
    },
    validateTitle(title) {
      const { $dirty, $error } = this.$v.game[title];
      return $dirty ? !$error : null;
    },
    validateDescription(description) {
      const { $dirty, $error } = this.$v.game[description];
      return $dirty ? !$error : null;
    },
    validateReleaseDate(releaseDate) {
      const { $dirty, $error } = this.$v.game[releaseDate];
      return $dirty ? !$error : null;
    },
    validateEsrbRating(esrbRating) {
      const { $dirty, $error } = this.$v.game[esrbRating];
      return $dirty ? !$error : null;
    },
    addGame() {
      if (this.game) {
        GameService.addGame(this.game)
          .then(() => {
            this.game = {};
            this.$router.push({ name: "home" });
          })
          .catch(() => {
            console.log("error adding game");
          });
      } else this.formComplete = false;
    },
    reset() {
      this.game.title = "";
      this.game.description = "";
      this.game.esrbRating = null;
      this.game.releaseDate = "";
      this.game.imageUrl;
      this.game.genres = [];
      this.game.platforms = [];
      this.game.developers = [];
      this.game.publishers = [];
    },
  },
};
</script>

<style scoped src="vue-multiselect/dist/vue-multiselect.min.css">
.bracket {
  font-size: 10px;
  color: grey;
}
</style>