<template>
  <div>
    <div class="d-flex justify-content-center">
      <b-card class="my-2" footer-tag="footer">
        <template #header>
          <h2 class="mb-0">
            <b-link
              class="title-link"
              v-bind:to="{ name: 'game', params: { id: item.id } }"
              >{{ item.title }}</b-link
            >
          </h2>
        </template>
        <router-link v-bind:to="{ name: 'game', params: { id: item.id } }">
          <b-card-img v-bind:src="item.imageUrl" style="max-width: 20rem" />
        </router-link>
        <b-card-text class="description">{{ item.description }}</b-card-text>
        <avg-rating-for-game v-bind:item="item" />
        <!--This is importing the average star rating-->
        <b-button-group class="mx-1" v-show="$store.state.user.role == 'admin'">
          <b-button
            class="btn btn-warning"
            v-bind:to="{ name: 'edit', params: { id: item.id } }"
          >
            Edit <b-icon icon="pencil-fill" aria-hidden="true"></b-icon>
          </b-button>
        </b-button-group>
        <b-button-group class="mx-1" v-show="$store.state.user.role == 'admin'">
          <b-button
            class="btn btn-danger"
            v-bind:to="{ name: 'delete', params: { id: item.id } }"
            >Delete <b-icon icon="trash" aria-hidden="true"> </b-icon>
          </b-button>
          <b-modal
            ok-variant="danger"
            ok-title="DELETE"
            cancel-title="CANCEL"
            @ok="deleteGame"
            id="my-modal"
          >
            Are you sure you want to delete {{ item.title }}?
          </b-modal>
        </b-button-group>
        <template #footer>
          <h3>
            {{ new Date(item.releaseDate).toLocaleString("en", options) }}
          </h3>
        </template>
      </b-card>
    </div>
  </div>
</template>

<script>
import avgRatingForGame from "../components/AvgRatingForGame.vue";
export default {
  components: { avgRatingForGame },
  props: ["item"],
  data() {
    return {
      options: { year: "numeric", month: "long", day: "numeric" },
    };
  },
};
</script>

<style scoped>
div.card {
  border: 1px solid rgb(187, 187, 206);
  width: 40%;
  text-align: center;
  color: black;
}
.title-link {
  color: black;
}
</style>