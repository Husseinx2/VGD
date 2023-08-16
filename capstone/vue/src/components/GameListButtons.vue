<template>
  <b-button-group>
    <b-button
      v-bind:pressed="isOnPlayedList"
      v-b-popover.hover.top="'Already Played'"
      v-on:click="togglePlayedButton"
    >
      <b-icon-check-square />
    </b-button>
    <b-button
      v-bind:pressed="isOnWantToPlayList"
      v-on:click="toggleWantToPlayButton"
      v-b-popover.hover.top="'Want to play'"
    >
      <b-icon-list />
    </b-button>
    <b-button
      v-bind:pressed="isOnCurrentlyPlayingList"
      v-on:click="toggleCurrentlyPlayingButton"
      v-b-popover.hover.top="'Currently Playing'"
    >
      <b-icon-controller />
    </b-button>
  </b-button-group>
</template>

<script>
import GameListService from "../services/GameListService";
export default {
  data() {
    return {
      isOnPlayedList: false,
      isOnWantToPlayList: false,
      isOnCurrentlyPlayingList: false,
    };
  },
  methods: {
    togglePlayedButton() {
      if (this.isOnPlayedList) {
        this.isOnPlayedList = false;
      } else {
        this.isOnPlayedList = true;
      }
    },
    toggleWantToPlayButton() {
      if (this.isOnWantToPlayList) {
        this.isOnWantToPlayList = false;
      } else {
        this.isOnWantToPlayList = true;
      }
    },
    toggleCurrentlyPlayingButton() {
      if (this.isOnCurrentlyPlayingList) {
        this.isOnCurrentlyPlayingList = false;
      } else {
        this.isOnCurrentlyPlayingList = true;
      }
    },
    ///api calls
    getCurrentlyPlayingList() {
      GameListService.getGameList(
        this.$store.state.user.userId,
        "Currently Playing"
      ).then((response) => {
        response.data.gameIds.forEach((id) => {
          if (id == this.$route.params.id) {
            this.isOnCurrentlyPlayingList = true;
          }
        });
      });
    },
    getPlayedList() {
      GameListService.getGameList(this.$store.state.user.userId, "Played").then(
        (response) => {
          response.data.gameIds.forEach((id) => {
            if (id == this.$route.params.id) {
              this.isOnPlayedList = true;
            }
          });
        }
      );
    },
    getWantToPlayList() {
      GameListService.getGameList(
        this.$store.state.user.userId,
        "Want To Play"
      ).then((response) => {
        response.data.gameIds.forEach((id) => {
          if (id == this.$route.params.id) {
            this.isOnWantToPlayList = true;
          }
        });
      });
    },
  },
  created(){
    this.getWantToPlayList();
    this.getCurrentlyPlayingList();
    this.getPlayedList();
  }
};
</script>

<style>
</style>