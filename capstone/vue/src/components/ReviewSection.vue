<template>
  <div class="container">
    <h1>Reviews</h1>
    <div class="container">
      <review-card
        v-for="review in item"
        v-bind:key="review.reviewId"
        v-bind:item="review"
      />
    </div>
  </div>
</template>

<script>
import ReviewCard from "./ReviewCard.vue";
export default {
  components: { ReviewCard },
  props: ["item"],
};
</script>

<style>
</style>