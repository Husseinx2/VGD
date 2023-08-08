<template>
    <header >
      <b-navbar  v-show="$store.state.user.role == 'user' || $store.state.user.role == 'admin'">
        <b-navbar-brand href="/">VGD</b-navbar-brand>
        <b-nav-form >
          <b-form-input v-if="!$route.meta.hideNavbar" type="search" size="sm" placeholder="Search">
            </b-form-input>
          <b-button v-if="!$route.meta.hideNavbar" size="sm" type="submit">Search</b-button>
        </b-nav-form>
        <b-navbar-nav class="ml-auto">
          <b-nav-item class="addGame" href="addGame" v-show="$store.state.user.role == 'admin'">Add a Game</b-nav-item>
        <b-nav-item class="logout" href="logout">Logout</b-nav-item>
        </b-navbar-nav>
      </b-navbar>
    </header>

</template>

<script>
export default {
  data() {
    return {
      title: '',
      description: '',
      newGame: {},
      games: [],
    };
  },
    computed: {
    filteredList() {
      return this.games.filter((game) => {
        return game.title.toLowerCase().includes(this.search.toLowerCase());
      });
    },
  },
}
</script>

<style scoped>
header{
  background-color: #c5e2ff
}

</style>