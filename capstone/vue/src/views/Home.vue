<template>
  <div class="home container">
    <b-alert :show="gameAdded" variant="success" role="alert" dismissible>
      Game Successfully Added
    </b-alert>

    <b-alert :show="gameEdited" variant="success" role="alert" dismissible>
      Game Successfully Edited
    </b-alert>

    <b-alert :show="gameDeleted" variant="success" dismissible role="alert">
      Game Successfully Deleted
    </b-alert>
    <carousel />
    <br />
    <jumbotron />
    <br />
    <what-to-play />
    <back-to-top />
  </div>
</template>

<script>
import BackToTop from "../components/BackToTop.vue";
import Carousel from "../components/Carousel.vue";
import Jumbotron from "../components/Jumbotron.vue";
import WhatToPlay from "../components/WhatToPlay.vue";

export default {
  name: "home",
  components: { BackToTop, WhatToPlay, Carousel, Jumbotron },
  computed: {
    filteredList() {
      return this.games.filter((game) => {
        return (
          game.title.toLowerCase().includes(this.search) ||
          game.description.toLowerCase().includes(this.search)
        );
      });
    },
  },
};
</script>

<style scoped>
</style>
