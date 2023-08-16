<template>
  <b-button-group>
    <b-button
      v-bind:pressed="isOnPlayedList"
      v-b-popover.hover.top="'Already Played'"
      v-on:click="togglePlayedButton"
    >
      <b-icon-check-square />
    </b-button>
    <b-button v-bind:pressed="isOnWantToPlayList"  v-on:click="toggleWantToPlayButton"  v-b-popover.hover.top="'Want to play'">
      <b-icon-list />
    </b-button>
    <b-button  v-bind:pressed="isOnCurrentlyPlayingList"  v-on:click="toggleCurrentlyPlayingButton"   v-b-popover.hover.top="'Currently Playing'">
      <b-icon-controller />
    </b-button>
  </b-button-group>
</template>

<script>
export default {
  data() {
    return {
      isOnPlayedList: false,
      isOnWantToPlayList: true,
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
  },
};
</script>

<style>
</style>