<template>
  <section >
    <b-card  class="comments-card">
      <b-card-header class="font-weight-bold">
        <table>
          <td>
        <b-avatar  :variant="commenter.username == isAdmin ? 'danger' : 'primary'">
          </b-avatar> 
          </td>
          <td>
          <p class="username" :class="isItalics" :style="changeFont" v-text="isDeleted" ></p>
          </td>
        </table>
          </b-card-header>
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
        <b-card-footer class="footer">
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
      isAdmin: 'admin',
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
  computed:{
    changeFont(){
        if(this.commenter.isDeleted){
         return "font-size:13px"
      }
      else{
        return ""
      }
    },
    isItalics(){
      if(this.commenter.isDeleted){
        return "font-weight-normal font-italic font-size:13px"
      }
      else{
        return ""
      }
    },
    isDeleted(){
      if(this.commenter.isDeleted){
        return "Profile has been deactivated"
      }
      else{
        return this.commenter.username
      }
    }
  },
  mounted() {
    console.log("reach mounted in Comment Card",this.item.commenterId)
    userService
      .GetUser(this.item.commenterId)
      .then((response) => {
        this.commenter = response.data})
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
    Users(){
      userService.listUsers()
      .then((response) => {
        this.users = response.data
      })
    },
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
.footer{
  font-size: 13px;
}
</style>