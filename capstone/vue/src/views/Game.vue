<template>
  <div>
    <section class="desc">
      <game-description v-bind:item="game" />
    </section>
    <section class="rating">
      <ratings-card v-bind:item="rating" />
    </section>
    <section class="detail">
      <game-details v-bind:item="game" />
    </section>
  </div>
</template>

<script>
import gameService from "../services/GameService";
import GameDescription from "../components/GameDescription.vue";
import GameDetails from "../components/GameDetails.vue";
import RatingsCard from "../components/RatingsCard.vue";
import RatingService from "../services/RatingService";
export default {
  components: { GameDescription, GameDetails, RatingsCard },
  data() {
    return {
      id: 0,
      game: {},
      rating: {},
    };
  },
  created() {
    this.id = this.$route.params.id;
    gameService
      .getGame(this.id)
      .then((response) => {
        this.game = response.data;
        if (!this.game) {
          this.$router.push({ name: "notFound" });
        }
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error getting game: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log("Error getting game: unable to communicate to server");
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error getting game: make request");
        }
        this.$router.push("/*");
      });
    ///ratings
    RatingService.getRating(this.id, this.$store.state.user.userId).then((response) => {
      this.rating = response.data;
    }).catch(()=> {
      this.rating = {}
    })
  },
};
</script>

<style scoped>
.desc {
  margin: 50px;
}
</style>