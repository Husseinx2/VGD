<template>
  <div class="container">
    <b-card class="review-card">
      <b-card-header
        class="font-weight-bold"
        v-show="user.username != undefined"
      >
        <router-link
          :is="isCurrentUserAdmin ? 'router-link' : 'div'"
          class="profile-link"
          v-bind:to="{ name: 'profile', params: { id: item.reviewerId } }"
        >
          <table>
            <td>
              <b-avatar
                :variant="user.username == isAdmin ? 'danger' : 'primary'"
              >
              </b-avatar>
            </td>
            <td>
              <p
                class="username"
                :class="isItalics"
                :style="changeFont"
                v-text="isDeleted"
              ></p>
            </td>
          </table>
        </router-link>
      </b-card-header>
      <b-card-header
        class="font-weight-bold"
        v-show="getUsername != null && user.username == undefined"
      >
        <router-link
          :is="isCurrentUserAdmin ? 'router-link' : 'div'"
          class="profile-link"
          v-bind:to="{ name: 'profile', params: { id: item.reviewerId } }"
        >
          <table>
            <td>
              <b-avatar
                :variant="getUsername == isAdmin ? 'danger' : 'primary'"
              >
              </b-avatar>
            </td>
            <td>
              <p
                class="username"
                :class="isItalics"
                :style="changeFont"
                v-text="isDeleted2"
              ></p>
            </td>
          </table> </router-link
      ></b-card-header>
      <b-card-body class="body">
        <b-card-text v-show="!showEditForm">
          {{ item.reviewContent }}
        </b-card-text>
        <br />
        <edit-review-form v-bind:item="item" v-show="showEditForm" />
        <b-card-footer class="date-posted"
          >Posted:
          {{
            new Date(item.reviewDateTime).toLocaleString("en", options)
          }}</b-card-footer
        >
        <b-button-group class="mx-1">
          <b-button
            class="btn btn-info"
            v-show="!hideCommentButton"
            @click="setUser(user.username)"
            >{{ commentButtonLabel() }} <b-icon icon="chat" />
          </b-button>
        </b-button-group>
        <b-button-group class="mx-1">
          <b-button
            class="btn btn-warning"
            @click="showEditForm = !showEditForm"
            v-show="
              $store.state.user.role == 'admin' ||
              ($store.state.user.userId == item.reviewerId &&
                $store.state.userId == null)
            "
          >
            Edit <b-icon icon="pencil-fill" aria-hidden="true"></b-icon>
          </b-button>
        </b-button-group>

        <b-button-group class="mx-1">
          <b-button
            class="btn btn-danger"
            v-b-modal="`${item.reviewContent}`"
            v-bind:key="item.reviewId"
            v-show="
              $store.state.user.role == 'admin' ||
              $store.state.user.userId == item.reviewerId
            "
          >
            Delete <b-icon icon="trash" />
          </b-button>
        </b-button-group>

        <b-modal
          ok-variant="danger"
          ok-title="DELETE"
          cancel-title="CANCEL"
          @ok="deleteReview"
          v-bind:id="item.reviewContent"
        >
          Are you sure you want to delete this review?
        </b-modal>
      </b-card-body>
    </b-card>
  </div>
</template>

<script>
import commentService from "../services/CommentService.js";
import reviewService from "../services/ReviewService.js";
import userService from "../services/UserService.js";
import EditReviewForm from "./EditReviewForm.vue";
export default {
  components: { EditReviewForm },
  props: ["item", "hideCommentButton"],
  data() {
    return {
      isAdmin: "admin",
      options: { year: "numeric", month: "long", day: "numeric" },
      user: {},
      reviewLink: { name: "review", params: { id: this.item.reviewId } },
      showEditForm: false,
      commentCount: 0,
    };
  },
  computed: {
    getUsername() {
      return this.$store.state.userId;
    },
    changeFont() {
      if (this.user.isDeleted) {
        return "font-size:13px";
      } else {
        return "";
      }
    },
    isItalics() {
      if (this.user.isDeleted) {
        return "font-weight-normal font-italic font-size:13px";
      } else {
        return "";
      }
    },
    isDeleted2() {
      if (this.user.isDeleted) {
        return "Profile has been deactivated";
      } else {
        return this.getUsername;
      }
    },
    isDeleted() {
      if (this.user.isDeleted) {
        return "Profile has been deactivated";
      } else {
        return this.user.username;
      }
    },
    isCurrentUserAdmin() {
      return this.$store.state.user.role == 'admin';
    },
  },
  methods: {
    setUser(username) {
      this.$store.commit("USER_VARIABLE", username);
      this.$router.push(this.reviewLink);
    },
    deleteReview() {
      reviewService.deleteReview(this.item.reviewId).then(() => {
        if (this.$route.path == `/profile/${this.item.reviewerId}`) {
          location.reload();
        } else {
          this.$router.push({ name: "game", params: { id: this.item.gameId } });
        }
      });
    },
    showEditButton() {
      return this.$store.state.user.userId === this.item.reviewerId;
    },
    getCommentCount() {
      commentService
        .getReviewComments(this.item.reviewId)
        .then((response) => (this.commentCount = response.data.length));
    },
    commentButtonLabel() {
      return `${this.commentCount} ${
        this.commentCount === 1 ? "Comment" : "Comments"
      }`;
    },
  },
  mounted() {
    console.log("reached reviewCard.vue created", this.item);
    this.$nextTick(() => {
      userService
        .GetUser(this.item.reviewerId)
        .then((response) => {
          this.user = response.data;
        })
        .catch((err) => {
          console.log("reached from reviewCard", err);
        });
      this.getCommentCount();
    });
  },
};
</script>

<style scoped>
.date-posted {
  font-size: 13px;
}
.profile-link {
  color: black;
}
</style>