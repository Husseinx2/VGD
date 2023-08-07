import axios from "axios";

export default {
  list() {
    return axios.get("/game");
  },

  getGame(id) {
    return axios.get(`/game/${id}`);
  },

  addGame(newGame) {
    return axios.post("/game", newGame);
  },
  editGame(game) {
    console.log(game)
   return axios.put(`/game/${game.id}`, game)
  },
  deleteGame(id) {
    return axios.delete(`/game/${id}`)
  },

};
 