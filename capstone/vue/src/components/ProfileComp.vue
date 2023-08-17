<template>
  <section class="ProfileComp container">
    <h1>User: {{ user.username }}</h1>
    <b-button
      class="btn btn-danger"
      v-show="item != $store.state.user.userId && user.role != 'admin'"
      v-b-modal="`${user.username}`"
      v-bind:key="user.userId"
    >
      <b> Delete {{ user.username }}</b> <b-icon icon="trash" />
    </b-button>
    <b-modal
      ok-variant="danger"
      ok-title="DELETE"
      cancel-title="CANCEL"
      @ok="deleteUser"
      v-bind:id="user.username"
    >
      Are you sure you want to delete {{ user.username }}?
    </b-modal>

    <div class="accordion" role="tablist">
      <b-card no-body class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block v-b-toggle.accordion-1 variant="info"
            >Toggle Rating</b-button
          >
        </b-card-header>
        <b-collapse id="accordion-1" accordion="my-accordion" role="tabpanel">
          <profile-rating-section v-bind:item="item" />
        </b-collapse>
      </b-card>

      <b-card no-body class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block v-b-toggle.accordion-2 variant="info"
            >Toggle Reviews</b-button
          >
        </b-card-header>
        <b-collapse id="accordion-2" accordion="my-accordion" role="tabpanel">
          <review-section v-bind:item="reviews" />
        </b-collapse>
      </b-card>

      <b-card no-body class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block v-b-toggle.accordion-3 variant="info" role="tab"
            >Toggle Comments</b-button
          >
        </b-card-header>
        <b-collapse id="accordion-3" accordion="my-accordion" role="tabpanel">
          <h1 v-show="comments.length > 0">Comments:</h1>

          <list-user-comments
            v-for="comment in comments"
            v-bind:key="comment.commentId"
            v-bind:item="comment"
          />
        </b-collapse>
      </b-card>

      <b-card no-body class="mb-1">
        <b-card-header header-tag="header" class="p-1" role="tab">
          <b-button block v-b-toggle.accordion-4 variant="info" role="tab"
          >Toggle Game Lists</b-button
          >
        </b-card-header>
        <b-collapse id="accordion-4" accordion="my-accordion" role="tabpanel">
          <h1>Lists:</h1>
          <game-list-section />
        </b-collapse>
      </b-card>
    </div>
  </section>
</template>
<script>
import UserService from "../services/UserService";
import ProfileRatingSection from "./ProfileRatingSection.vue";
import reviewService from "../services/ReviewService.js";
import commentService from "../services/CommentService";
import ListUserComments from "./ListUserComments.vue";
import ReviewSection from "./ReviewSection.vue";
import GameListSection from "./GameListSection.vue";
export default {
  components: {
    ProfileRatingSection,
    ListUserComments,
    ReviewSection,
    GameListSection,
  },
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
</style>