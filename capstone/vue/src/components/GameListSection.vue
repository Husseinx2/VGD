<template>
  <div>
    <b-tabs content-class="mt-3">
      <b-tab v-for="type in gameListTypes" :key="type" :title="type">
        <b-card>
          <game-list :item="gameLists[type]" />
        </b-card>
      </b-tab>
    </b-tabs>
  </div>
</template>
<script>
import gameListService from "../services/GameListService";
import GameList from "./GameList.vue";
export default {
  components: { GameList },
  data() {
    return {
      userId: 0,
      gameListTypes: ["Played", "Currently Playing", "Want To Play"],
      gameLists: {},
      games: [],
    };
  },
  methods: {
    getLists() {
      this.gameListTypes.forEach((type) => {
        gameListService
          .getGameList(this.userId, type)
          .then((response) => {
            const gameList = response.data;
            this.gameLists[type] = gameList;
            console.log("fsa");
            console.log(gameList);
          })
          .catch((error) => {
            console.error(error);
            this.gameLists[type] = [];
          });
      });
    },
    logGameIds(){
      this.gameListTypes.forEach((type)=> {
        console.log("reached loggames")
        console.log(this.gameLists[type]);

      })
    }
  },
  created() {
    this.userId = this.$route.params.id;
    this.getLists();
    this.logGameIds();
  },
};
</script>

<style scoped>
.game {
  height: 100%;
  width: 50%;
}
</style>