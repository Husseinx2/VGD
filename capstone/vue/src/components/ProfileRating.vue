<template>
  <section class="ProfileRating">
    <b-card v-for="rating in ratings" v-bind:key="rating.userId">
      <b-card-header>Game: {{ rating.gameId }}</b-card-header>
      <b-card-text> Rating: {{ rating.ratingValue }}</b-card-text>
      <b-card-text
        >Date:
        {{
          new Date(rating.ratingDateTime).toLocaleString("en", options)
        }}</b-card-text
      >
    </b-card>
  </section>
</template>

<script>
import ratingService from "../services/RatingService";

export default {
  name: "profileRating",
  props: ["item"],
  data() {
    return {
      ratings: [],
      options: { year: "numeric", month: "long", day: "numeric" },
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