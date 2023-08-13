<template>
  <div class="container">
    <review-card
    
      v-bind:item="review"
      class="list-reviews-card"
    />
  </div>
</template>


<script>
import ReviewCard from '../components/ReviewCard.vue';
import reviewService from "../services/ReviewService.js";
export default {
  components: { ReviewCard },
  data() {
  
  return {
      id: 0,

      review: null,
    };
  },
  created() {
    this.id = Number.parseInt(this.$route.params.id);
    reviewService.getReview(this.id)
    .then((response) => {this.review = response.data})
  },
};
</script>

<style>
</style>