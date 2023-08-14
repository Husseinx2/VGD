<template>
  <section >
    <b-card  class="comments-card">
      <b-card-header>Commenter: {{ commenter.username }} </b-card-header>
      <!--game title -->
      <b-card-body class="body">
        <b-card-text
        v-show="!showEdit"
        > {{ item.commentContent }} 
        </b-card-text>
        <b-form @submit.stop.prevent="onSubmit">
        <b-form-textarea
        type="text"
        aria-describedby="input-live-feedback"
        v-model="$v.comment.commentContent.$model"
        :state="validateState('commentContent')"
        v-show="showEdit"
        required
      > 
        </b-form-textarea>
        <b-form-invalid-feedback id="input-live-feedback"
        >Enter at least 1 letter</b-form-invalid-feedback
      >    
      <b-button v-show="showEdit" type="submit" variant="primary">Submit</b-button>
      <br/> 
        </b-form>
        <br />
        <b-card-footer>
          Posted:
          {{ new Date(item.commentDateTime).toLocaleString("en", options) }}
        </b-card-footer>
        <b-button-group>
        <b-button
          class="btn btn-warning"
          v-bind:key="item.commentId"
          @click="showEdit = !showEdit"
          v-show="$store.state.user.role == 'admin' || $store.state.user.userId == item.commenterId"
        >
          Edit <b-icon icon="pencil-fill" />
        </b-button>
        </b-button-group>
        <b-button-group>
        <b-button
          class="btn btn-danger"
          v-b-modal="`${item.commentContent}`"
          v-bind:key="item.commentId"
          v-show="$store.state.user.role == 'admin' || $store.state.user.userId == item.commenterId"
        >
          Delete <b-icon icon="trash" />
        </b-button>
        </b-button-group>

        <b-modal
          ok-variant="danger"
          ok-title="DELETE"
          cancel-title="CANCEL"
          @ok="deleteComment"
          v-bind:id="item.commentContent"
        >
          Are you sure you want to delete This comment?
        </b-modal>
      </b-card-body>
    </b-card>
  </section>
</template>

<script>
import { required, minLength } from "vuelidate/lib/validators";
import userService from "../services/UserService";
import commentService from "../services/CommentService";
export default {
  props: ["item"],
  data() {
    return {
      currentGame:{},
      showEdit: false,
      comment: {
        commentId: this.item.commentId,
        commentContent: this.item.commentContent,
        commenterId: this.item.commenterId,
        reviewId: this.item.reviewId,
        commentDateTime: new Date().toJSON(),
      },
      options: { year: "numeric", month: "long", day: "numeric" },
      commenter: null,
    };
  },
  mounted() {
    console.log("reach mounted in Comment Card",this.item.commenterId)
    userService
      .GetUser(this.item.commenterId)
      .then((response) => (this.commenter = response.data));

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
    deleteComment() {
      console.log("reached");
      commentService.deleteComment(this.item.commentId).then(() => {
        location.reload();
      });
    },
    editComment() {
      if (this.comment) {
        commentService
          .editComment(this.comment)
          .then(() => {
            location.reload();
          })
          .catch(() => {
            console.log("error editing comment");
          });
      }
    },
    // checks for validation errors
    onSubmit() {
      this.$v.comment.$touch();
      if (this.$v.comment.$anyError) {
        return;
      }
      this.editComment();
    },
    //reactively validates review form
    validateState(state) {
      const { $dirty, $error } = this.$v.comment[state];
      return $dirty ? !$error : null;
    },
  }
}
</script>

<style scoped>
</style>