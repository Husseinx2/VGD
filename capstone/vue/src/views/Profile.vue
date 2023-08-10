<template>
  <div class="Profile">
      <main>
      <h1>
          Profile
      </h1>
      <profile-comp />
      <profile-rating />
      </main>
  </div>
</template>

<script>
import ProfileComp from '../components/ProfileComp.vue';
import ProfileRating from '../components/ProfileRating.vue';
import RatingService from "../services/RatingService";

export default {
components: {ProfileComp, ProfileRating},

 methods: {
    push() {
      this.$router.push("/");
    },
    ListRatingsByUserId() {
      RatingService.ListRatingsByUserId(this.game.id).then(() => {
        this.$router.push("/");
      });
    },
  },
} 
</script>

<style>
div {
  background-color:whitesmoke;
}

</style>