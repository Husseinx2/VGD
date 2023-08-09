<template>
  <div>
    <label for="rating">Average Rating</label>
    <b-form-rating size="sm" no-border v-model="value" readonly></b-form-rating>
  </div>
</template>

<script>
import ratingService from "../services/RatingService.js";
export default {
  data() {
    return {
  props: ["game"],
      avgRating: 0,
      ratings: [],
      user: {
        userRating: null,
      },
    };
  },
  methods: {
    //temporary till we get the server side working
    getAvgRatingValue() {
      if (this.user.userRating) {
        ratingService
          .getRating(this.user.userRating)
          .then(() => {})
          .catch();
      }
    },
    GetRatings() {
      ratingService.getRatingByGameId(this.gameId).then((response) => {
        this.ratings = response.data;
      });
    },

  },
  computed: {

  },
  created() {
    this.GetRatings();
  },
};
</script>

<style>
</style>