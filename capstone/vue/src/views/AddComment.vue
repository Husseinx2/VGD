<template>
  <div>
<add-comment-card class="addComment" />
 </div>
</template>

<script>
import addCommentCard from '../components/AddCommentCard.vue'
export default {
components: {addCommentCard}
}
</script>

<style scoped>
.addGame{
  width: 50%;
  margin-left:25%;
  padding-right: 10%
}
</style>