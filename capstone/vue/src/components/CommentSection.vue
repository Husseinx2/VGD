<template>
  <div>
    <p>Comments Section {{ item }}</p>
    <comment-card
      v-for="comment in item"
      v-bind:key="comment.commentId"
      v-bind:item="comment"
    />
  </div>
</template>

<script>
import CommentCard from "./CommentCard.vue";
export default {
  components: { CommentCard },
  props: ["item"],
};
</script>

<style>
</style>