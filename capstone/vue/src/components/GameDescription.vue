<template>
  <div class="card container my-5">
    <b-alert :show="addReview" variant="success" dismissible role="alert">
      Review Successfully Added
    </b-alert>
    <div class="container mt-2">
      <span v-show="$store.state.user.role == 'admin'">
        <div class="b-btn-toolbar float-right">
          <b-button-group class="mx-1">
            <b-button
              class="btn btn-warning"
              v-bind:to="{ name: 'edit', params: { id: item.id } }"
            >
              Edit <b-icon icon="pencil-fill" aria-hidden="true"></b-icon>
            </b-button>
          </b-button-group>
          <b-button-group class="mx-1">
            <b-button class="btn btn-danger" v-b-modal.my-modal
              >Delete <b-icon icon="trash" aria-hidden="true"></b-icon
            ></b-button>
          </b-button-group>
          <b-modal
            title="Warning"
            ok-variant="danger"
            ok-title="DELETE"
            cancel-title="CANCEL"
            @ok="deleteGame"
            id="my-modal"
          >
            Are you sure you want to delete {{ item.title }}?
          </b-modal>
        </div>
      </span>
    </div>
    <div class="p-5 text-center bg-body-tertiary rounded-3">
      <b-img :src="item.imageUrl" fluid alt=""></b-img>
      <h1>{{ item.title }}</h1>
      <p>{{ item.description }}</p>
    </div>
    <table class="table">
      <tbody>
        <td>
          <section class="detail">
            <game-details v-bind:item="game" />
          </section>
        </td>
        <td>
          <b-button @click="showAddReview = !showAddReview"
            >Add Review</b-button
          >
          <b-form-group v-show="showAddReview">
            <add-review-form />
            <b-button type="submit">Submit</b-button>
          </b-form-group>
        </td>
        <td>
          <section class="rating">
            <ratings-card v-bind:item="id" />
          </section>
        </td>
      </tbody>
    </table>
  </div>
</template>

<script>
import reviewService from "../services/ReviewService.js";
import addReviewForm from "../components/AddReviewForm.vue";
import RatingsCard from "../components/RatingsCard.vue";
import gameService from "../services/GameService";
import GameDetails from "../components/GameDetails.vue";
export default {
  components: { GameDetails, RatingsCard, addReviewForm },
  props: ["item"],
  data() {
    return {
      showAddReview: false,
      id: 0,
      game: {},
    };
  },
  methods: {
    push() {
      this.$router.push("/");
    },
    deleteGame() {
      gameService.deleteGame(this.game.id).then(() => {
        this.$store.commit("GAME_DELETED", true);
        this.$router.push("/");
      });
    },
    addReview() {
      if (this.review) {
        reviewService
          .addReview(this.review)
          .then(() => {
            this.review = {};
            this.$store.commit("REVIEW_ADDED", true);
          })
          .catch(() => {
            console.log("error adding review");
          });
      }
    },
  },
  created() {
    this.id = Number.parseInt(this.$route.params.id);
    gameService
      .getGame(this.id)
      .then((response) => {
        this.game = response.data;
        if (!this.game) {
          this.$router.push({ name: "notFound" });
        }
      })
      .catch((error) => {
        if (error.response) {
          // error.response exists
          // Request was made, but response has error status (4xx or 5xx)
          console.log("Error getting game: ", error.response.status);
        } else if (error.request) {
          // There is no error.response, but error.request exists
          // Request was made, but no response was received
          console.log("Error getting game: unable to communicate to server");
        } else {
          // Neither error.response and error.request exist
          // Request was *not* made
          console.log("Error getting game: make request");
        }
        this.$router.push("/*");
      });
  },
};
</script>

<style scoped>
</style>