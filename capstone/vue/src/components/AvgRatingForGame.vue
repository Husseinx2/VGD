<template>
  <div class="container">
    <b-form-rating
      size="lg"
      no-border
      v-model="AvgRating"
      readonly
      color="orange"
      show-value=true
      precision=2
    ></b-form-rating>
  </div>
</template>

<script>
import ratingService from "../services/RatingService.js";
export default {
  data() {
    return {
      ratings: [],
      user: {
        userRating: this.$store.state.user,
      },
    };
  },
  props: ["item"],
  methods: {
    GetRatings() {
      ratingService.listRatingsByGameId(this.item.id).then((response) => {
        this.ratings = response.data;
      });
    },
  },
  computed: {
    AvgRating() {
      let sum = 0;
      this.ratings.forEach((rating) => {
        sum += rating.ratingValue;
      });
      return sum / this.ratings.length;
    },
  },
  created() {
    this.GetRatings();
  },
};
</script>

<style>
</style>