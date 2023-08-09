<template>
  <div class="container">
    <h1>Are you sure you want to delete {{ game.title }}?</h1>
    <br />

    <div class="b-btn-toolbar">
      <b-button-group class="mx-4">
        <button class="btn btn-danger" v-on:click="deleteGame">Yes</button>
      </b-button-group>
      <b-button-group class="mx-1">
        <button class="btn btn-secondary" v-on:click="push">No</button>
      </b-button-group>
    </div>
  </div>
</template>

<script>
import GameService from "../services/GameService";
export default {
  data() {
    return {
      game: {},
    };
  },
  methods: {
    push() {
      this.$router.push("/");
    },
    deleteGame() {
      GameService.deleteGame(this.game.id).then(() => {
        this.$router.push("/");
      });
    },
  },
  created() {
    GameService.getGame(this.$route.params.id)
      .then((response) => {
        this.game = response.data;
      })
      .catch(() => {
        console.log("delete Error");
      });
  },
};
</script>

<style>

</style>