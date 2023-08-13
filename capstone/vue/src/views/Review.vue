<template>
  <div class="container">
    <review-card
    
      v-bind:item="review"
      class="list-reviews-card"
    />
    <comment-section 
    
    v-bind:item="comments"
    class="comments" />
  </div>
</template>


<script>
import ReviewCard from '../components/ReviewCard.vue';
import reviewService from "../services/ReviewService.js";
import CommentSection from "../components/CommentSection.vue";
import CommentService from '../services/CommentService';
export default {
  components: { ReviewCard, CommentSection },
  data() {
  
  return {
      id: 0,
      comments: null,
      review: null,
    };
  },
  created() {
    this.id = Number.parseInt(this.$route.params.id);
    reviewService.getReview(this.id)
    .then((response) => {this.review = response.data})
    CommentService.getReviewComments(this.id)
    .then((response) => {this.comments = response.data})
  },
};
</script>

<style>
</style>