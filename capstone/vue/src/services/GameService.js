import axios from "axios";

export default {
  list() {
    return axios.get("/");
  },

  addGame(newGame) {
    return axios.post("/games", newGame);
  }
};
