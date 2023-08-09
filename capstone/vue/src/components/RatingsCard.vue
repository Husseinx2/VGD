<template>
    <div>
    <label for="rating-inline-center">How would you rate this game?</label>
    <b-form-rating size="lg" no-border show-clear class="rating" id="rating-inline-center" inline value="0" color="orange"></b-form-rating>
  </div>
</template>

<script>
import ratingService from "../services/RatingService.js";
export default {
 data() {
    return {
        user: {
            userRating: null,
        }
    }
 },
 methods: {
    createRatingValue(){
        if(this.user.userRating){
            ratingService
            .getRating(this.user.userRating)
            .then(() => {
            })
            .catch()
        }
    }
 }
}
</script>

<style scoped>
.rating{
    align-content: center;
    vertical-align: middle;
    
    
}
</style>