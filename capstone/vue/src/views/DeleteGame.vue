<template>
  <div>
    <h1>Are You sure you want to delete {{ game.title }}?</h1>
    <br />
    <button v-on:click="deleteGame" class="red">yes</button> ||
    <button v-on:click="push">No</button>
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
      GameService.deleteGame(this.game.id).then((response) => {
        if (response.data) {
          this.$router.push("/");
        }
        else {
          console.log("delete problem")
        }
      });
    },
  },
  created() {
    GameService.getGame(this.$route.params.id).then((response) => {
      this.game = response.data;
    });
  },
};
</script>

<style>
.red {
  background-color: red;
}
</style>