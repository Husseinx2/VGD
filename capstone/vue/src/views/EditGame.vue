<template>
  <div>
    <b-form class="edit" @submit="addGame" @reset="reset">
      <label for="title">Title: </label>
      <b-form-input v-model="game.title" :state="titleState" name="title" type="text" aria-describedby="input-live-help input-live-feedback" />
      <b-form-invalid-feedback id="input-live-feedback">Enter at least 1 letter</b-form-invalid-feedback>
      <br />
      <label for="description">Description:</label>
      <br />
      <b-form-textarea name="description" v-model="game.description"> </b-form-textarea>
      <br />
      <label for="description">ESRB Rating:</label>
      <b-form-select :options="options" type="text" name="rating" v-model="game.esrbRating"></b-form-select>
      <br />
      <br />
      <label for="date">Release Date:</label>
      <b-form-datepicker  type="date" name="date" />
      <br />
    <input type="submit"  />
    </b-form>
  </div>
</template>

<script>
import gameService from "../services/GameService.js";
export default {
  data() {
    return {
      options:[
        {value: null, text: 'Select ESRB Rating'},
        {value: 'eC', text: 'Early Childhood (eC)'},
        {value: 'E', text: 'Everyone (E)'},
        {value: 'T', text: 'Teen (T)'},
        {value: 'M', text: 'Mature (M)'},
        {value: 'Ao', text: 'Adults Only (Ao)'},
        {value: 'RP', text: 'Rating Pending (RP)'}
      ],
      game: {
        title: '',
        esrbRating: '',
      },
    };
  },
  computed: {
    titleState() {
        return this.game.title.length > 0 ? true : false
      }
    },
  methods: {
    submitEdit () {
      console.log("Reached")
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

</style>