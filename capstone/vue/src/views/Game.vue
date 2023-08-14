<template>
  <div>
    <section class="desc">
      <game-description v-bind:item="game" />
      <review-section v-bind:item="freshReviews" />
    </section>
  </div>
</template>

<script>
import gameService from "../services/GameService";
import reviewService from "../services/ReviewService";
import GameDescription from "../components/GameDescription.vue";
import ReviewSection from "../components/ReviewSection.vue";
export default {
  components: { GameDescription, ReviewSection },
  data() {
    return {
      id: 0,
      game: {},
      reviews: [],
    };
  },
  computed:{
    freshReviews(){
      return this.reviews;
    }
  },
  methods: {
    showAddReviewSection() {
      return this.reviews.all(
        (review) => review.reviewerId !== this.$store.state.user.userId
      );
    },
  },
  created() {
    this.id = parseInt(this.$route.params.id);
    gameService
      .getGame(this.id)
      .then((response) => {
        this.game = response.data;
        if (!this.game) {
          this.$router.push({ name: "notFound" });
        }
        console.log("reached game.vue this.id", this.id)
        reviewService
          .getGameReviews(this.id)
          .then((response) => {
            this.reviews = response.data;
            console.log("reached game.vue reviews array", this.reviews)
          })
          .catch((error) => {
            if (error.response) {
              // error.response exists
              // Request was made, but response has error status (4xx or 5xx)
              console.log("Error getting reviews: ", error.response.status);
            } else if (error.request) {
              // There is no error.response, but error.request exists
              // Request was made, but no response was received
              console.log(
                "Error getting reviews: unable to communicate to server"
              );
            } else {
              // Neither error.response and error.request exist
              // Request was *not* made
              console.log("Error getting reviews: make request");
            }
          });
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
        this.$router.push({ name: "notFound" });
      });
  },
};
</script>

<style scoped>
.desc {
  margin: 50px;
}
</style>