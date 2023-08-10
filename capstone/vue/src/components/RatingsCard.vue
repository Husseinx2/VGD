<template>
  <div>
    <label for="rating-inline-center">How would you rate this game?</label>
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
        gameId: null,
        userId: 0,
        ratingValue: 0,
        ratingDateTime: 0,
      },
    };
  },

  methods: {
    updateRating() {
      if (!this.rating.userId) {
        this.rating.gameId = this.item;
        this.rating.userId = this.$store.state.user.userId;
        const date = new Date().toJSON();
        this.rating.ratingDateTime = date;
        RatingService.addRating(this.rating);
      } else {
        console.log("existing");
        const date = new Date().toJSON();
        this.rating.ratingDateTime = date;
        RatingService.updateRating(this.rating);
      }
    },
  },
  created() {
    RatingService.getRating(this.item, this.$store.state.user.userId)
      .then((response) => {
        this.rating = response.data;
      })
      .catch(() => {
        this.rating = {};
      });
  },
};
</script>

<style scoped>
.rating {
  align-content: center;
  vertical-align: middle;
}
</style>