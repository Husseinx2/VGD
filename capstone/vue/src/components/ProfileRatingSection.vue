<template>
  <section class="ProfileRating">
    <rating-review-card
      v-for="rating in ratings"
      v-bind:key="rating.userId"
      v-bind:rating="rating"
    />
  </section>
</template>

<script>
import ratingService from "../services/RatingService";
import RatingReviewCard from "./RatingReviewCard.vue";
export default {
  name: "profileRating",
  components: { RatingReviewCard },
  props: ["item"],
  data() {
    return {
      ratings: [],
    };
  },
  methods: {
    GetRatings() {
      ratingService.listRatingsByUserId(this.item).then((response) => {
        this.ratings = response.data;
      });
    },
  },

  created() {
    this.GetRatings();
  },
};
</script>

<style>
</style>