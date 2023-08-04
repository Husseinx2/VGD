import axios from "axios";


export default {
  list() {
    return axios.get("/games");
  },

  addGame(newGame) {
    return axios.post("/games", newGame);
  }
};
 