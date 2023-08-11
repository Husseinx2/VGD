<template>
  <header>
    <b-navbar
      v-show="
        $store.state.user.role == 'user' || $store.state.user.role == 'admin'
      "
    >
      <b-navbar-brand img src="\Images\VGD.jpg" href="/">
        <img class="navbar-logo" src="./..\Images\logo1.png" />
      </b-navbar-brand>
      <b-nav-form v-on:click.prevent>
        <b-form-input
          v-if="!$route.meta.hideNavbar"
          size="sm"
          v-model="searchParameters.title"
          placeholder="Search Games"
        >
        </b-form-input>
        <b-button
          v-if="!$route.meta.hideNavbar"
          v-on:click="searchGames"
          size="sm"
          type="submit"
          ><b-icon icon="search"></b-icon>
        </b-button>
      </b-nav-form>
      <b-button
        v-if="!$route.meta.hideNavbar"
        style="margin-left: 20px"
        v-b-toggle.sidebar-right
      >
        Advanced search
      </b-button>
      <b-alert v-model="showAlert" variant="danger" dismissible>
        Not Found!
      </b-alert>

      <b-navbar-nav class="ml-auto">
        <router-link
          class="addGame"
          to="/addGame"
          v-show="$store.state.user.role == 'admin'"
        >
          <b-icon icon="plus-circle" aria-hidden="true"></b-icon>
          Add a Game
        </router-link>
        <b-dropdown right text="Settings" variant="primary">
          <b-dropdown-item v-on:click="pushToProfile">
            <b-icon icon="person" aria-hidden="true"></b-icon>
            {{ $store.state.user.username }}
          </b-dropdown-item>
          <b-dropdown-item
            to="/profiles"
            v-show="$store.state.user.role == 'admin'"
          >
            <b-icon icon="person-lines-fill" aria-hidden="true"></b-icon>
            profiles
          </b-dropdown-item>
          <b-dropdown-item to="/logout">
            <b-icon icon="power" aria-hidden="true"></b-icon> Logout
          </b-dropdown-item>
        </b-dropdown>
        <section class="sidebar-menu">
          <b-sidebar
            backdrop
            id="sidebar-right"
            title="Filter Options"
            right
            shadow
          >
            <div class="sidebar-work container">
              <b-form-group>
               
                  <label>Title</label>
                  <b-form-input
                    v-model="searchParameters.title"
                    placeholder="Enter a title"
                  >
                  </b-form-input>
    
              </b-form-group>
              <b-form-group>
                <label>ESRB</label>
                <b-form-input
                  v-model="searchParameters.esrbRating"
                  placeholder="Enter a rating"
                ></b-form-input>
              </b-form-group>
              <b-form-group>
                <label>Year</label>
                <b-form-input
                  v-model="searchParameters.year"
                  placeholder="Enter a year"
                ></b-form-input>
              </b-form-group>
              <b-form-group>
                <label>Genre</label>
                <b-form-input
                  v-model="searchParameters.genreName"
                  placeholder="Enter genre(s)"
                ></b-form-input>
              </b-form-group>
              <b-form-group>
                <label>Platform</label>
                <b-form-input
                  v-model="searchParameters.platformName"
                  placeholder="Enter platform(s)"
                ></b-form-input>
              </b-form-group>
              <b-form-group>
                <label>Developer</label>
                <b-form-input
                  v-model="searchParameters.developerName"
                  placeholder="Enter developer(s)"
                ></b-form-input>
              </b-form-group>
              <b-form-group>
                <label>Publisher</label>
                <b-form-input
                  v-model="searchParameters.publisherName"
                  placeholder="Enter publisher(s)"
                ></b-form-input>
              </b-form-group>
               <b-button
                    v-if="!$route.meta.hideNavbar"
                    v-on:click="searchGames"
                    size="sm"
                    type="submit"
                    ><b-icon icon="search"></b-icon>
                    Advanced Search
                  </b-button>
            </div>
          </b-sidebar>
        </section>
      </b-navbar-nav>
    </b-navbar>
  </header>
</template>

<script>
import GameService from "../services/GameService";
export default {
  data() {
    return {
      searchParameters: {
        title: "",
        genreName: "",
        esrbRating: "",
        year: null,
        platformName: "",
        developerName: "",
        publisherName: "",
      },

      showAlert: false,
    };
  },
  methods: {
    searchGames() {
      // Filters out null and empty string in the query object
      const filteredSearchParameters = ((obj) => {
        return Object.fromEntries(
          Object.entries(obj).filter(
            ([, value]) => value !== null && value !== ""
          )
        );
      })(this.searchParameters);
      this.$router.push({
        name: "search",
        query: filteredSearchParameters,
      });
      location.reload();
    },
    pushToProfile() {
      this.$router.push({
        name: "profile",
        params: { id: this.$store.state.user.userId },
      });
      location.reload();
    },
    //loading games
    loadGames() {
      GameService.listGames()
        .then((response) => {
          console.log("Reached created in nav");
          this.games = response.data;
        })
        .catch((error) => {
          if (error.response) {
            // error.response exists
            // Request was made, but response has error status (4xx or 5xx)
            console.log("Error loading games: ", error.response.status);
          } else if (error.request) {
            // There is no error.response, but error.request exists
            // Request was made, but no response was received
            console.log("Error loading games: unable to communicate to server");
          } else {
            // Neither error.response and error.request exist
            // Request was *not* made
            console.log("Error loading games: make request");
          }
        });
    },
  },
  created() {
    this.loadGames();
  },
};
</script>

<style scoped>
header {
  background-color: #c5e2ff;
}
.addGame {
  position: relative;
  right: 10px;
  top: 4px;
  font-size: 20px;
}
.navbar-logo {
  width: 75px;
}
</style>  