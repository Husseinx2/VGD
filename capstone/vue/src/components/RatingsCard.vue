<template>
  <div>
    <section>
    <label for="rating-inline-center">How would you rate this game?</label>
    </section>
    <div v-on:click="updateRating">
      <b-form-rating
        size="lg"
        no-border
        show-clear
        class="rating"
        id="rating-inline-center"
        inline
        v-model="rating.ratingValue"
        v-bind:value="rating.ratingValue"
        color="orange"
      ></b-form-rating>
    </div>
  </div>
</template>

<script>
import RatingService from "../services/RatingService";
export default {
  props: ["item"],
  data() {
    return {
      rating: {
        gameId: this.item,
        userId: this.$store.state.user.userId,
        ratingValue: null,
        ratingDateTime: null,
      },
      hasRating: false,
      prevRating: {
        gameId: this.item,
        userId: this.$store.state.user.userId,
        ratingValue: null,
        ratingDateTime: null,
      },
    };
  },

  methods: {
    updateRating() {
      this.rating.ratingDateTime = new Date().toJSON();
      if (!this.hasRating) {
        if (this.rating.ratingValue) {
          console.log("add rating");
          RatingService.addRating(this.rating);
        }
      } else if (!this.rating.ratingValue) {
        console.log("delete rating");
        RatingService.deleteRating(this.rating.gameId, this.rating.userId);
      } else {
        console.log("update rating");
        RatingService.updateRating(this.rating);
      }
      this.hasRating = this.rating.ratingValue != null
    },
  },
  created() {
    RatingService.getRating(this.item, this.$store.state.user.userId)
      .then((response) => {
        this.rating = response.data;
        this.hasRating = true;
      })
      .catch(() => {
        this.rating = {
          gameId: this.item,
          userId: this.$store.state.user.userId,
          ratingValue: null,
        };
        this.hasRating = false;
      });
    console.log(this.rating);
  },
};
</script>

<style scoped>
.rating{
    background-color: #e9f4ff;
    
}
</style>