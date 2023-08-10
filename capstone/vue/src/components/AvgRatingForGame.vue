<template>
  <div>
    <label for="rating">Average Rating</label>
    <b-form-rating
      size="sm"
      no-border
      v-model="AvgRating"
      readonly
      color="orange"
    ></b-form-rating>
  </div>
</template>

<script>
import ratingService from "../services/RatingService.js";
export default {
  data() {
      
    return {
      ratings: [],
      user: {
        userRating: this.$store.state.user,
      },
    };
  },
  props:['item'],
  methods: {
    //temporary till we get the server side working
    getAvgRatingValue() {
      if (this.user.userRating) {
        ratingService
          .getRating(this.user.userRating)
          .then(() => {})
          .catch();
      }
    },
    GetRatings() {
      ratingService.getRatingByGameId(this.item.id).then((response) => {
        this.ratings = response.data;
      });
    },

  },
  computed: {
    
       AvgRating(){
            let sum =0;
             this.ratings.forEach((rating) => {
                
                sum += rating.ratingValue;
            });
            return sum/this.ratings.length;
       }
  },
  created() {
    this.GetRatings();
  },
};
</script>

<style>
</style>