<template>
  <div class="container">
    <br />
    <b-form-group>
    <h2>{{game.title}}</h2>
    </b-form-group>
    <b-form-group>
    <review-card
      v-bind:item="review"
      v-bind:hideCommentButton="true"
      class="review-card"
    />
    </b-form-group>
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
  components: { ReviewCard, CommentSection},
  data() {
    return {
      id: parseInt(this.$route.params.id),
      comments: {},
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
              console.log("reached review.vue commentService", this.comments)
            })
             console.log("Reached review.vue gameservice", this.review)

            .catch(() => {
              console.log("error getting this.id");
            });
        });
      })
      .catch(() => {
        console.log("error getting this.$route.params.id");
      });

  },
};
</script>

<style scoped>
.comments{
  min-height: 100vh;
}
.container{
  background-color: #e4e5f1
}
*{
  background-color: #e4e5f1;
}
</style>