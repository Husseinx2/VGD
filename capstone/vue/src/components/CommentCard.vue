<template>
  <section>
    <b-card class="comments-card">
      <b-card-header>Commenter: {{ commenter.username }} </b-card-header>
      <!--game title -->
      <b-card-body class="body">
        <b-card-text> {{ item.commentContent }} </b-card-text>
        <br />
        <b-card-text>
          Date:
          {{ new Date(item.commentDateTime).toLocaleString("en", options) }}
        </b-card-text>
        <button
          class="btn btn-danger"
          v-b-modal="`${item.commentContent}`"
          v-bind:key="item.commentId"
          v-show="$store.state.user.role == 'admin' || $store.state.user.userId == item.commenterId"
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
          Are you sure you want to delete This comment?
        </b-modal>
      </b-card-body>
    </b-card>
  </section>
</template>

<script>
import userService from "../services/UserService";
import commentService from "../services/CommentService";
export default {
  props: ["item"],
  data() {
    return {
      options: { year: "numeric", month: "long", day: "numeric" },
      commenter: null,
    };
  },
  created() {
    userService
      .GetUser(this.item.commenterId)
      .then((response) => (this.commenter = response.data));
  },
  methods: {
    deleteComment() {
      console.log("reached");
      commentService.deleteComment(this.item.commentId).then(() => {
        location.reload();
      });
    },
  },
};
</script>

<style scoped>
</style>