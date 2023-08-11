import axios from "axios";

export default {
  listUsers() {
    return axios.get("/user");
  },
  GetUser(id) {
    return axios.get(`/user/${id}`)
  },
  DeleteUser(id) {
    return axios.delete(`user/${id}`);
  }
};
 