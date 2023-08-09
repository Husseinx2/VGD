<template>
    <div>
    <label for="rating">Average Rating</label>
    <b-form-rating size="sm" no-border v-model="value" readonly></b-form-rating>
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
 methods: {//temporary till we get the server side working
    getAvgRatingValue(){
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

<style>

</style>