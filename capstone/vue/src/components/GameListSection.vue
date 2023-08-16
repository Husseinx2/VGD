<template>
  <section style="border: 1px solid black">
    <b-tabs content-class="mt-3">
      <b-tab
        v-for="list in lists"
        v-bind:key="list.listId"
        v-bind:title="list.listTitle"
        
      >
      <div class="d-flex justify-content-center">
        <game-list
          class="game"
          v-for="gameId in list.gameIds"
          v-bind:key="gameId"
          v-bind:item="gameId"
        />
        </div>
      </b-tab>
    </b-tabs>
  </section>
</template>
<script>
import gameListservice from "../services/GameListService";
import GameList from "./GameList.vue";
export default {
  components: { GameList },
  data() {
    return {
      lists: {},
    };
  },
  methods: {
    getLists() {
      gameListservice
        .listGameListsByUserId(this.$route.params.id)
        .then((response) => {
          this.lists = response.data;
        });
    },
  },
  created() {
    this.getLists();
  },
};
</script>

<style scoped>
.game {
  height: 100%;
  width: 50%;
}
</style>