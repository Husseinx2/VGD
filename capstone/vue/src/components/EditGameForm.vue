<template>
  <b-form @submit.stop.prevent="onSubmit">
    <b-form-group class="titleInput">
      <label>Title</label>
      <b-form-input
        class="form__input"
        id="titleInput"
        v-model="$v.game.title.$model"
        :state="validateState('title')"
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
        :state="validateState('description')"
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
    <b-form-group>
      <label for="description">ESRB Rating:</label>
      <b-form-select
        :options="options"
        type="text"
        name="rating"
        v-model="$v.game.esrbRating.$model"
        :state="validateState('esrbRating')"
        aria-describedby="input-live-feedback3"
        required
      ></b-form-select>
      <b-form-invalid-feedback id="input-live-feedback3"
        >Enter a valid rating</b-form-invalid-feedback
      >
    </b-form-group>
    <!-- date -->
    <b-form-group>
      <label for="date">Release Date:</label>
      <b-form-datepicker
        v-model="$v.game.releaseDate.$model"
        :state="validateState('releaseDate')"
        aria-describedby="input-live-4"
        type="date"
        name="date"
        required
      />
      <b-form-invalid-feedback id="input-live-4"
        >Enter a valid date</b-form-invalid-feedback
      >
    </b-form-group>

    <!-- Image -->
    <b-form-group id="urlInput" label="Image Url" label-for="urlInput">
      <b-form-input
        id="urlInput"
        v-model="$v.game.imageUrl.$model"
        :state="validateState('imageUrl')"
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
    <b-form-group>
      <label for="genre">Select genre(s):</label>
      <b-form-tags
        required
        input-id="genre"
        placeholder="Pick some"
        v-model="$v.game.genres.$model"
        aria-describedby="input-live-5"
        tag-placeholder="Add this as new tag"
      ></b-form-tags>
      <b-form-invalid-feedback id="input-live-5"
        >Please enter valid genre(s)</b-form-invalid-feedback
      >
    </b-form-group>

    <b-form-group>
      <!-- platforms -->
      <label for="platforms"
        >Type a new platform and press enter
       </label
      >
      <b-form-tags
        input-id="platforms"
        placeholder="Add platform.."
        v-model="$v.game.platforms.$model"
        :state="validateState('platforms')"
        required
        aria-describedby="input-live-6"
      ></b-form-tags>
      <b-form-invalid-feedback id="input-live-6"
        >Please enter valid platform(s)</b-form-invalid-feedback
      >
    </b-form-group>
    <br />
    <b-form-group>
      <!-- publishers -->
      <label for="Publishers">Type a new Publisher and press enter </label>
      <b-form-tags
        input-id="Publishers"
        placeholder="Add Publishers.."
        v-model="$v.game.publishers.$model"
        :state="validateState('publishers')"
        required
        aria-describedby="input-live-7"
      ></b-form-tags>
      <b-form-invalid-feedback id="input-live-7"
        >Please enter valid publisher(s)</b-form-invalid-feedback
      >
    </b-form-group>
    <br />
    <b-form-group>
      <!-- developers -->
      <label for="developers">Type a new Developer and press enter </label>
      <b-form-tags
        input-id="developers"
        placeholder="Add Developers.."
        v-model="$v.game.developers.$model"
        :state="validateState('developers')"
        required
        aria-describedby="inp-liv-8"
      ></b-form-tags>
      <b-form-invalid-feedback id="inp-liv-8"
        >Please enter valid developer(s)</b-form-invalid-feedback
      >
    </b-form-group>
    <input type="submit" variant="primary" />
  </b-form>
</template>

<script>
import { required, minLength, alpha, url } from "vuelidate/lib/validators";
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
        imageUrl: "",
        trailerUrl: "",
        genres: [],
        platforms: [],
        developers: [],
        publishers: [],
      },
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
      platforms: {
        required,
      },
      publishers: {
        required,
      },
      developers: {
        required,
      },
      imageUrl: {
        required,
        url,
      },
    },
  },
  methods: {
    onSubmit() {
      this.$v.game.$touch();
      if (this.$v.game.$anyError) {
        return;
      }
      this.submitEdit();
    },
    validateState(state) {
      const { $dirty, $error } = this.$v.game[state];
      return $dirty ? !$error : null;
    },
    submitEdit() {
      console.log("Reached");
      if (
        this.game
      ) {
        gameService.editGame(this.game).then(() => {
          gameService.listGames();
          this.$store.commit("GAME_EDITED",true);
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