<template>
  <section>
<b-card-group class="list-reviews-card">
<b-card-header>{{}} </b-card-header> <!--Username-->
<b-card-body>{{review.review_content}}</b-card-body>
<b-card-footer>{{review.datetime}}</b-card-footer>
</b-card-group>
  </section>
</template>

<script>
import userService from "../services/UserService.js";
import reviewService from "../services/ReviewService.js";
export default {
    data(){
        return{
            reviews:[],
            usernames: []
        }
    },
    methods: {
     getUsernames(){
        userService
            .GetUser()
            .then(response => {
                this.usernames = response;
            })
     },
     loadReviews() {
      reviewService
        .getGameReviews()
        .then((response) => {
          console.log("Reached created in ListGameReviews.vue");
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