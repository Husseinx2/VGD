<template>
  <section>
    <b-form-group>
        <b-form-textarea placeholder="Write your review here"></b-form-textarea>
    </b-form-group>
  </section>
</template>

<script>
import { required, minLength } from "vuelidate/lib/validators";
export default {
  data(){
    return {
      review: {}
    }
  },
   validations: {
    game: {
      game_id: null,
      user_id: null,
      content: {
        required,
        minLength: minLength(1),
      },
    }
  },

}
</script>

<style>

</style>