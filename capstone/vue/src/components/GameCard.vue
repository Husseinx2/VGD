<template>
  <div class="container-fluid d-flex justify-content-center">
    <div>
      <b-card-group deck class="my-2">
        <b-card 
          class="ct-body"
          v-bind:img-src="item.imageUrl"
          style="max-width: 20rem"
          img-top
        >
          <b-card-text >
            <h3 class="mb-0 text-center">
              <b-link
                class="title-link"
                v-bind:to="{ name: 'game', params: { id: item.id } }"
                >{{ item.title }}</b-link
              >
            </h3>

            <avg-rating-for-game v-bind:item="item" />
          </b-card-text>
          <!--This is importing the average star rating-->
        </b-card>
      </b-card-group>
    </div>
  </div>
</template>

<script>
import AvgRatingForGame from "../components/AvgRatingForGame.vue";
import gameService from "../services/GameService";
export default {
  components: { AvgRatingForGame },
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
  font-family:fantasy
}
.ct-body{
  background-color: #d2d3db;
}
.my-2:hover{
  transform: scale(1.05, 1.05);
}
.title-link:hover{
  color:grey
}

</style>