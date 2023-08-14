<template>
  <div class="container">
    <h1>Reviews</h1>
    <div class="container">
      <add-review-form v-if="!userReview" />
      <review-card
        v-for="review in item"
        v-bind:key="review.reviewId"
        v-bind:item="review"
      />
    </div>
  </div>
</template>

<script>
import AddReviewForm from "./AddReviewForm.vue";
import ReviewCard from "./ReviewCard.vue";
export default {
  components: { ReviewCard, AddReviewForm },
  props: ["item"],
  data() {
    return {
      userHasReview: false,
    };
  },
  created() {
    this.userHasReview = this.item.some(
      (review) => review.reviewerId === this.$store.state.user.userId
    );
  },
};
</script>

<style>
</style>