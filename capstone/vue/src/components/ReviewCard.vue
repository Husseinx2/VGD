<template>
  <div class="container">
    <b-card class="review-card">
      <b-card-header>{{ user.username }} </b-card-header>
      <b-card-body class="body">
        <b-card-text> {{ item.reviewContent }} </b-card-text>
        <br />
        <b-card-text>
          Date:
          {{ new Date(item.reviewDateTime).toLocaleString("en", options) }}
        </b-card-text>
        <b-button-group class="mx-1">
          <b-button
            class="btn btn-info"
            v-show=!hideCommentButton
            v-bind:to=reviewLink
            >Comments <b-icon icon="chat" />
          </b-button>
        </b-button-group>
        <b-button-group class="mx-1">
          <b-button
            class="btn btn-danger"
            v-b-modal="`${item.reviewContent}`"
            v-bind:key="item.reviewId"
            v-show="
              $store.state.user.role == 'admin' ||
              $store.state.user.userId == item.reviewerId
            "
          >
            Delete <b-icon icon="trash" />
          </b-button>
        </b-button-group>

        <b-modal
          ok-variant="danger"
          ok-title="DELETE"
          cancel-title="CANCEL"
          @ok="deleteReview"
          v-bind:id="item.reviewContent"
        >
          Are you sure you want to delete This review?
        </b-modal>
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
import reviewService from "../services/ReviewService.js";
import userService from "../services/UserService.js";
export default {
  props: ["item", "hideCommentButton"],
  data() {
    return {
      options: { year: "numeric", month: "long", day: "numeric" },
      user: {},
      reviewLink: { name: 'review', params: { id: this.item.reviewId } },
    };
  },
  methods: {
    deleteReview() {
      reviewService.deleteReview(this.item.reviewId).then(() => {
        location.reload();
      });
    },
  },
  created() {
    userService
      .GetUser(this.item.reviewerId)
      .then((response) => (this.user = response.data));
  },
};
</script>

<style scoped>
</style>