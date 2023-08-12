<template>
  <section>
<b-card-group
        v-for="review in reviews"
        v-bind:key="review.id"
        v-bind:item="review" class="list-reviews-card">
<b-card-header>{{gameTitle}} </b-card-header> <!--game title -->
<b-card-body>{{review.review_content}}</b-card-body>
<b-card-footer>{{review.datetime}}</b-card-footer>
</b-card-group>
  </section>
</template>

<script>
import gameService from "../services/GameService.js"
import reviewService from "../services/ReviewService.js";
export default {
    data(){
        return{
            reviews:[],
            gameTitle: ""
        }
    },
    methods: {
    getGameTitle(gameId){
        gameService
            .getGame(gameId)
            .then(response => {
                this.gameTitle = response;
            })
        },
    loadReviews() {
      reviewService
        .getUserReviews()
        .then((response) => {
          console.log("Reached created in ListUserReviews.vue");
          console.log(response);
          this.reviews = response.data;
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error loading reviews: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log("Error loading reviews: unable to communicate to server");
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error loading reviews: make request");
          }
        });
    },
    },
    created() {
        this.loadReviews();
  },
}
</script>

<style>

</style>