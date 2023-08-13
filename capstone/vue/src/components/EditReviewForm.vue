<template>
  <section>
    <b-form-group>
        <b-form-textarea 
        class="form__input"
        id="titleInput"
        v-model="$v.review.content.$model"
        :state="validateState('content')"
        type="text"
        required
        aria-describedby="input-live-feedback"
        placeholder="Write your review here">
        </b-form-textarea>
    </b-form-group>
  </section>
</template>

<script>
import { required, minLength } from "vuelidate/lib/validators";
export default {
  data(){
    return {
      review: {
        gameId: parseInt(this.$route.params.id),
        reviewContent: "",
        userId: this.$store.state.user.userId,
        reviewDateTime: new Date().toJSON(),
      }
    }
  },
   validations: {
    review: {
      reviewContent: {
        required,
        minLength: minLength(1),
      },
    }
  },

}
</script>

<style>

</style>