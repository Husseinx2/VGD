<template>
  <section class="ProfileRating">
    <h1></h1>
  </section>
</template>

<script>
import ratingService from "../services/RatingService";

export default {
  name: "profileRating",
  props: ["item"],
  data() {
    return {
      methods: {
        GetRatings() {
          ratingService.listRatingsByUserId(this.item.id).then((response) => {
            this.ratings = response.data;
          });
        },
      },
    };
  },
};
</script>

<style>
</style>