<template>
  <div>
    <h1>Comments</h1>
    <comment-card
      v-for="comment in item"
      v-bind:key="comment.commentId"
      v-bind:item="comment"
    />
      <b-button @click="showAddComment = !showAddComment">
      Add Comment
    </b-button>
    <add-comment-card v-show="showAddComment"/>
  </div>
</template>
<script>
import AddCommentCard from './AddCommentCard.vue';
import CommentCard from "./CommentCard.vue";
export default {
  components: { CommentCard , AddCommentCard},
  props: ["item"],
  data() {
    return {
      showAddComment:false
    }
  }
};
</script>

<style>
</style>