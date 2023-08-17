<template>
  <div class="container-fluid d-flex justify-content-center">
    <div>
      <b-card-group deck class="my-2">
        <router-link class="router-link-reset" v-bind:to="{ name: 'game', params: { id: item.id } }">
        <b-card 
          class="ct-body"
          v-bind:img-src="item.imageUrl"
          style="max-width: 20rem"
          img-top
        >
          <b-card-text >
            <h3 class="mb-0 text-center title-link">
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
        </router-link>
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
      show:false,
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
  font-family:fantasy;
  color: black;
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
.router-link-reset {
  color: inherit; 
  text-decoration: none; 
}


</style>