<template>
  <div>
    <h1 v-show="item.length >0">Reviews:</h1>
    <div class="container">
      <review-card
        v-for="review in item"
        v-bind:key="review.reviewId"
        v-bind:item="review"
      />
    </div>
  </div>
</template>

<script>
import ReviewCard from "./ReviewCard.vue";
export default {
  components: { ReviewCard },
  props: ["item"],
  created(){
    console.log("reached created in reviewSection", this.item)
  }
};
</script>

<style>
</style>