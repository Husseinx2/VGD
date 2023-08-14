<template>
  <div class="container">
    <h2>{{game.title}}</h2>
    <review-card
      v-bind:item="review"
      v-bind:hideCommentButton="true"
      class="review-card"
    />
    <comment-section v-bind:item="comments" class="comments" />
  </div>
</template>

<script>
import ReviewCard from "../components/ReviewCard.vue";
import reviewService from "../services/ReviewService.js";
import CommentSection from "../components/CommentSection.vue";
import commentService from "../services/CommentService";
import gameService from "../services/GameService";
export default {
  components: { ReviewCard, CommentSection },
  data() {
    return {
      id: parseInt(this.$route.params.id),
      comments: [],
      review: {},
      game: {},
    };
  },
  mounted() {
    console.log("reached review.vue created", this.id);
    reviewService
      .getReview(this.id)
      .then((response) => {
        this.review = response.data;
        gameService.getGame(this.review.gameId)
        
        .then((response) => {
          this.game = response.data;
          commentService
            .getReviewComments(this.id)
            
            .then((response) => {
              this.comments = response.data;
            })
            .catch(() => {
              console.log("error getting this.id");
            });
        });
      })
      .catch(() => {
        console.log("error getting this.$route.params.id");
      });

    console.log("Reached review.vue gameservice", this.review.gameId);
  },
};
</script>

<style>
</style>