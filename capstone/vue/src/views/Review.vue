<template>
  <div class="container">
    <h2>GameTitle</h2>
    <review-card v-bind:item="review" v-bind:hideCommentButton="true" class="review-card" />
    <comment-section v-bind:item="comments" class="comments" />
  </div>
</template>

<script>
import ReviewCard from "../components/ReviewCard.vue";
import reviewService from "../services/ReviewService.js";
import CommentSection from "../components/CommentSection.vue";
import commentService from "../services/CommentService";
export default {
  components: { ReviewCard, CommentSection },
  data() {
    return {
      id: 0,
      comments: [],
      review: {},
    };
  },
  created() {
    this.id = Number.parseInt(this.$route.params.id);
    console.log("reached review.vue created", this.id);
    reviewService.getReview(this.id).then((response) => {
      this.review = response.data;
    });
    commentService.getReviewComments(this.id).then((response) => {
      this.comments = response.data;
    });
  },
};
</script>

<style>
</style>