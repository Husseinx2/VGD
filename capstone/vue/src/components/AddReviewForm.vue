<template>
  <b-form @submit.stop.prevent="onSubmit" >
    <b-form-group>
      <b-form-textarea
        placeholder="Write your review here"
        id="reviewInput"
        v-model.trim="$v.review.reviewContent.$model"
        :state="validateState('reviewContent')"
        aria-describedby="input-live-feedback"
        type="text"
        required
      >
      </b-form-textarea>
      <b-form-invalid-feedback id="input-live-feedback"
        >Enter at least 1 letter</b-form-invalid-feedback
      >
    </b-form-group>
    <b-form-group>
      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form-group>
  </b-form>
</template>

<script>
import reviewService from "../services/ReviewService.js";
import { required, minLength } from "vuelidate/lib/validators";
export default {
  data() {
    return {
      submitted: false,
      review: {
        gameId: parseInt(this.$route.params.id),
        reviewContent: "",
        reviewDateTime: new Date().toJSON(),
        reviewerId: this.$store.state.user.userId,
      },
    };
  },
  validations: {
    review: {
      reviewContent: {
        required,
        minLength: minLength(1),
      },
    },
  },
  computed: {
    reviewSuccess() {
      return true;
    },
  },
  methods: {
    // checks for validation errors
    onSubmit() {
      console.log("has reached onSUBMIT");
      this.$v.review.$touch();
      if (this.$v.review.$anyError) {
        return;
      }
      this.addReview();
    
    },
    //reactively validates review form
    validateState(state) {
      const { $dirty, $error } = this.$v.review[state];
      return $dirty ? !$error : null;
    },
    addReview() {
      if (this.review) {
        reviewService
          .addReview(this.review)
          .then(() => {
            this.$store.commit("REVIEW_ADDED", true);
         location.reload();
          })
          .catch(() => {
            console.log("error adding review");
            this.$store.commit("REVIEW_ADDED", false);
          });
      }
    },
  },
};
</script>

<style>
</style>