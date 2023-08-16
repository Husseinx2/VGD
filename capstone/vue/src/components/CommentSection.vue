<template>
  <div class= "container">
    <h1>Comments:</h1>
    <b-form-group>
      <b-button class="addBtn " @click="showAddComment = !showAddComment">
        Add Comment
      </b-button>
    </b-form-group>
    <add-comment-card v-show="showAddComment" />
    <b-form-group>
      <comment-card
        v-for="comment in item"
        v-bind:key="comment.commentId"
        v-bind:item="comment"
      />
    </b-form-group>
  </div>
</template>
<script>
import AddCommentCard from "./AddCommentCard.vue";
import CommentCard from "./CommentCard.vue";
export default {
  components: { CommentCard, AddCommentCard },
  props: ["item"],
  data() {
    return {
      showAddComment: false,
    };
  },
};
</script>

<style>
</style>