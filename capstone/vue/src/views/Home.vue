<template>
  <div class="home">
    <h1>Home</h1>
    <p>You must be authenticated to see this</p>
    <input placeholder="Search Games"  type="text" v-model="search" />
    <select v-model="select"> 
      <option v-for="game in games" v-bind:key="game.id" >
       {{game.title}}
    </option>
    </select>
    <game-card-vue
      v-for="game in filteredList"
      v-bind:key="game.id"
      v-bind:item="game"
    />
  </div>
</template>

<script>
import GameCardVue from "../components/GameCard.vue";
export default {
  name: "home",
  data() {
    return {
      search:"",
      select: {
        id: "0",
        title: "",
        release_date: "",
        description: "",
      },
      games: [
        
         {
          id: 1,
          title: "Mario Bros",
          release_date: "09/13/1983",
          description: "a game",
        },
        {
          id: 2,
          title: "Red Dead Redemption",
          release_date: "09/13/2019",
          description: "a cowboy game",
        },
        {
          id: 3,
          title: "Grand Theft Auto V",
          release_date: "09/17/2013",
          description: "a violent game",
        },
      ],
    };
  },
  components: { GameCardVue },
  computed: {
     filteredList(){
       return this.games.filter((game) => {
         return (
            game.title.toLowerCase().includes(this.search)
         )
       })
     }
  }
};
</script>
