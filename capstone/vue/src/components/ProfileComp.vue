<template>
  <section class="ProfileComp">
    <h1>User: {{ user.username }}</h1>
    <button
      v-show="item != $store.state.user.userId && user.role != 'admin'"
      v-b-modal="`${user.username}`"
      v-bind:key="user.userId"
    >
      <b> Delete {{ user.username }}</b> <b-icon icon="trash" />
    </button>
    <b-modal
      ok-variant="danger"
      ok-title="DELETE"
      cancel-title="CANCEL"
      @ok="deleteUser"
      v-bind:id="user.username"
    >
      Are you sure you want to delete {{ user.username }}?
    </b-modal>
    <profile-rating-section v-bind:item="item" />
    <h1 v-show="reviews.length > 0">Reviews:</h1>
    <list-user-reviews
      v-for="review in reviews"
      v-bind:key="review.reviewId"
      v-bind:item="review"
    />
    <h1 v-show="comments.length >0">Comments</h1>

    <list-user-comments
      v-for="comment in comments"
      v-bind:key="comment.commentId"
      v-bind:item="comment"
    />
  </section>
</template>

<script>
import UserService from "../services/UserService";
import ProfileRatingSection from "./ProfileRatingSection.vue";
import reviewService from "../services/ReviewService.js";
import ListUserReviews from "./ListUserReviews.vue";
import commentService from "../services/CommentService";
import ListUserComments from "./ListUserComments.vue";
export default {
  components: { ProfileRatingSection, ListUserReviews, ListUserComments },
  props: ["item"],
  name: "profileComp",
  data() {
    return {
      user: {},
      reviews: [],
      comments: [],
    };
  },
  methods: {
    GetUser() {
      console.log("reached");
      UserService.GetUser(this.item)
        .then((response) => {
          this.user = response.data;
          this.loadReviews();
          this.loadComments();
        })
        .catch(() => {
          this.$router.push({ name: "notFound" });
        });
    },
    deleteUser() {
      UserService.DeleteUser(this.item).then(() => {
        this.$router.push("/profiles");
      });
    },
    loadReviews() {
      reviewService
        .getUserReviews(this.user.userId)
        .then((response) => {
          console.log(response);
          this.reviews = response.data;
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error loading reviews: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log(
              "Error loading reviews: unable to communicate to server"
            );
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error loading reviews: make request");
          }
        });
    },
    loadComments() {
      commentService
        .getUserComments(this.user.userId)
        .then((response) => {
          console.log(response);
          this.comments = response.data;
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error loading comments: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log(
              "Error loading reviews: unable to communicate to server"
            );
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error loading reviews: make request");
          }
        });
    },
  },
  created() {
    this.GetUser();
  },
};
</script>

<style scoped>
section.Profile {
  flex-wrap: wrap;
  display: inline-block;
  height: 120px;
  min-width: 300px;
  border: 2px solid black;
  border-radius: 10px;
  margin: 20px;
}
button:hover {
  background-color: red;
}
</style>