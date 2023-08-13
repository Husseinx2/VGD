<template>
  <div>
    <h1>Comments Section</h1>
    <b-button @click="showAddComment = !showAddComment">
      Add Comment
    </b-button>
    <add-comment-card v-show="showAddComment"/>
    <comment-card
      v-for="comment in item"
      v-bind:key="comment.commentId"
      v-bind:item="comment"
    />
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