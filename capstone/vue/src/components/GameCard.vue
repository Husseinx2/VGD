<template>
  <div class="container-fluid d-flex justify-content-center">
    <div>
      <b-card class="my-2" footer-tag="footer">
        <div class="d-flex justify-content-center">
        <router-link v-bind:to="{ name: 'game', params: { id: item.id } }">
          <b-card-img v-bind:src="item.imageUrl" style="max-width: 20rem" />
        </router-link>
        </div>
        <section>          
          <h2 class="mb-0 text-center">
            <b-link
              class="title-link"
              v-bind:to="{ name: 'game', params: { id: item.id } }"
              >{{ item.title }}</b-link
            >
          </h2></section>

        <avg-rating-for-game v-bind:item="item" />

        <!--This is importing the average star rating-->
  
      </b-card>
    </div>
  </div>
</template>

<script>
import avgRatingForGame from "../components/AvgRatingForGame.vue";
import gameService from "../services/GameService";
export default {
  components: { avgRatingForGame },
  props: ["item"],
  data() {
    return {
      options: { year: "numeric", month: "long", day: "numeric" },
    };
  },
  methods: {
    deleteGame() {
      gameService.deleteGame(this.item.id).then(() => {
        this.$store.commit("GAME_DELETED", true);
        location.reload();
      });
    },
  },
};
</script>

<style scoped>

.title-link {
  color: black;
}
</style>