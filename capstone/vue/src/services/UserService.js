import axios from "axios";

export default {
  listUsers() {
    return axios.get("/user");
  },
};
 