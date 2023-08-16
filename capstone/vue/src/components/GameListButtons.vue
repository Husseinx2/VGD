<template>
  <b-button-group>
    <b-button
      v-bind:pressed="isPlayed"
      v-b-popover.hover.top="'Played'"
      v-on:click="togglePlayedButton"
    >
      <b-icon-check-square />
    </b-button>
    <b-button
      v-bind:pressed="isCurrentlyPlaying"
      v-on:click="toggleCurrentlyPlayingButton"
      v-b-popover.hover.top="'Currently Playing'"
    >
      <b-icon-controller />
    </b-button>

    <b-button
      v-bind:pressed="isWantToPlay"
      v-on:click="toggleWantToPlayButton"
      v-b-popover.hover.top="'Want to play'"
    >
      <b-icon-list />
    </b-button>
  </b-button-group>
</template>

<script>
import gameListService from "../services/GameListService";
export default {
  props: ["item"],
  data() {
    return {
      gameId: 0,
      currentUserId: 0,
      isPlayed: false,
      isCurrentlyPlaying: false,
      isWantToPlay: false,
    };
  },
  methods: {
    togglePlayedButton() {
      const gameListType = "Played";
      this.isPlayed = !this.isPlayed;

      if (this.isPlayed) {
        this.addGameToList(gameListType);
      } else {
        this.deleteGameFromList(gameListType);
      }
    },
    toggleCurrentlyPlayingButton() {
      const gameListType = "Currently Playing";
      this.isCurrentlyPlaying = !this.isCurrentlyPlaying;

      if (this.isCurrentlyPlaying) {
        this.addGameToList(gameListType);
      } else {
        this.deleteGameFromList(gameListType);
      }
    },
    toggleWantToPlayButton() {
      const gameListType = "Want To Play";
      this.isWantToPlay = !this.isWantToPlay;

      if (this.isWantToPlay) {
        this.addGameToList(gameListType);
      } else {
        this.deleteGameFromList(gameListType);
      }
    },
    addGameToList(gameListType) {
      const entry = {
        gameId: this.gameId,
        userId: this.currentUserId,
        gameListType: gameListType,
      };
      gameListService
        .addGameToList(entry)
        .catch(() => console.log("Error adding game to list"));
    },
    deleteGameFromList(gameListType) {
      const entry = {
        gameId: this.gameId,
        userId: this.currentUserId,
        gameListType: gameListType,
        gameListDatetime: new Date().toJSON(),
      };
      gameListService
        .deleteGameFromList(entry)
        .catch((error) => console.error("Error deleting game from list", error));
    },
    getIsPlayed() {
      const gameListType = "Played";
      gameListService
        .getGameList(this.$store.state.user.userId, gameListType)
        .then((response) => {
          this.isPlayed = response.data.some(
            (entry) => this.gameId === entry.gameId
          );
        })
        .catch(() => console.log("Error getting isPlayed"));
    },
    getIsCurrentlyPlaying() {
      const gameListType = "Currently Playing";
      gameListService
        .getGameList(this.$store.state.user.userId, gameListType)
        .then((response) => {
          this.isCurrentlyPlaying = response.data.some(
            (entry) => this.gameId === entry.gameId
          );
        })
        .catch(() => console.log("Error getting isCurrentlyPlaying"));
    },
    getIsWantToPlay() {
      const gameListType = "Want To Play";
      gameListService
        .getGameList(this.$store.state.user.userId, gameListType)
        .then((response) => {
          this.isWantToPlay = response.data.some(
            (entry) => this.gameId === entry.gameId
          );
        })
        .catch(() => console.log("Error getting WantToPlay"));
    },
  },
  mounted() {
    this.gameId = this.item;
    this.currentUserId = this.$store.state.user.userId;
    this.getIsPlayed();
    this.getIsCurrentlyPlaying();
    this.getIsWantToPlay();
  },
};
</script>

<style>
</style>