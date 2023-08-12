<template>
  <section>
    <b-card class="list-reviews-card">
 <b-card-header >
     Comment For 
     {{user.username}}'s review
 </b-card-header>      
      <b-card-body class="body">
        <b-card-text> Comment: <br />{{ item.commentContent }} </b-card-text>
        <br />
        <b-card-text>
          Date:
          {{ new Date(item.commentDateTime).toLocaleString("en", options) }}
        </b-card-text>
        <button
          class="btn btn-danger"
          v-b-modal="`${item.commentContent}`"
          v-bind:key="item.commentId"
        >
          Delete <b-icon icon="trash" />
        </button>

      

        <b-modal
          ok-variant="danger"
          ok-title="DELETE"
          cancel-title="CANCEL"
          @ok="deleteComment"
          v-bind:id="item.commentContent"
        >
          Are you sure you want to delete This review?
        </b-modal>
      </b-card-body>
    </b-card>
  </section>
</template>

<script>
import commentService from "../services/CommentService.js";
import userService from "../services/UserService.js";
import ReviewService from "../services/ReviewService.js";
export default {
  props: ["item"],
  data() {
    return {
      user: {},
      review: {},
      game: {},
      options: { year: "numeric", month: "long", day: "numeric" },
    };
  },
  methods: {
    getUsernames() {
      userService.GetUser(this.review.reviewerId).then((response) => {
        this.user = response.data;
      });
    },
    getReview() {
      ReviewService.getReview(this.item.reviewId).then((response) => {
        this.review = response.data;
        this.getUsernames();
      });
    },
    deleteComment() {
      commentService.deleteComment(this.item.commentId).then(() => {
        location.reload();
      });
    },
  },
  created() {
      this.getReview();
  },
};
</script>

<style scoped>
</style>