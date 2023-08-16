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
    <section>
    <carousel />
    </section>
    <br />
    <jumbotron class="my-5" />
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
    gameAdded(){
      return this.$store.state.gameAdded;
    },
    gameEdited(){
      return this.$store.state.gameEdited;
    },
    gameDeleted(){
      return this.$store.state.gameDeleted;
    }
  },
};
</script>

<style scoped>
.my-5{
  margin-left: 15%;
}
</style>
