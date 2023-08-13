<template>
  <b-form @submit.stop.prevent="onSubmit">
    <b-form-group>
      <b-form-textarea
        placeholder="Enter Your Comment"
        id="commentInput"
        v-model.trim="$v.comment.commentContent.$model"
        :state="validateState('commentContent')"
        type="text"
        required
      />
    </b-form-group>
    <b-form-group>
      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form-group>
  </b-form>
</template>

<script>
import { required, minLength } from "vuelidate/lib/validators";
import CommentService from "../services/CommentService";
export default {
  data() {
    return {
      comment: {
        commentContent: "",
        commenterId: this.$store.state.user.userId,
        reviewId: parseInt(this.$route.params.id),
        CommentDateTime: new Date().toJSON(),
      },
    };
  },
  validations: {
    comment: {
      commentContent: {
        required,
        minLength: minLength(1),
      },
    },
  },
  methods: {
    validateState(state) {
      const { $dirty, $error } = this.$v.comment[state];
      return $dirty ? !$error : null;
    },
    onSubmit() {
      console.log("has reached onSUBMIT");
      this.$v.comment.$touch();
      if (this.$v.comment.$anyError) {
        return;
      }
      this.addComment();
    },
    addComment() {
      if (this.comment) {
        CommentService.addComment(this.comment)
          .then(() => {
            location.reload();
          })
          .catch(() => {
            console.log("error adding review");
          });
      }
    },
  },
};
</script>

<style>
</style>