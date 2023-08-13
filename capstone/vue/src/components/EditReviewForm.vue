<template>
  <b-form @submit.stop.prevent="onSubmit">
    <b-form-group>
      <b-form-textarea
        placeholder="Edit your review here"
        id="reviewInput"
        v-model="$v.review.reviewContent.$model"
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
  props: ["item"],
  data() {
    return {
      reviews: {},
      review: {
        gameId: this.item.gameId,
        reviewContent: this.item.reviewContent,
        reviewerId: this.item.reviewerId,
        reviewId: this.item.reviewId,
        reviewDateTime: new Date().toJSON(),
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
  methods: {
    // checks for validation errors
    onSubmit() {
      this.$v.review.$touch();
      if (this.$v.review.$anyError) {
        return;
      }
      this.editReview();
    },
    //reactively validates review form
    validateState(state) {
      const { $dirty, $error } = this.$v.review[state];
      return $dirty ? !$error : null;
    },
    editReview() {
      if (this.review) {
        reviewService
          .editReview(this.review)
          .then(() => {
            this.$store.commit("REVIEW_EDITED", true);
            console.log("before reload");
            location.reload();
          })
          .catch(() => {
            console.log("error editing review");
            this.$store.commit("REVIEW-EDITED", false);
          });
      }
    },
  },
};
</script>

<style>
</style>