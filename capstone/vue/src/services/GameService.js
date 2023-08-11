import axios from "axios";

export default {
  listGames() {
    return axios.get("/game");
  },

  getGame(id) {
    return axios.get(`/game/${id}`);
  },

  addGame(game) {
    console.log(game)
    return axios.post("/game", game);
  },
  editGame(game) {
    console.log(game)
   return axios.put(`/game/${game.id}`, game)
  },
  deleteGame(id) {
    return axios.delete(`/game/${id}`)
  },
  search(searchParameters) {
    return axios.post("/game/search", searchParameters);
  },

};
 