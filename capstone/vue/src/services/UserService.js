import axios from "axios";

export default {
  listUsers() {
    return axios.get("/user");
  },
  listActiveUsers() {
    return axios.get("/user/active");
  },
  GetUser(id) {
    console.log("reached user",id)
    return axios.get("/user/" + id)
  },
  DeleteUser(id) {
    return axios.delete("user/" + id);
  }
};
 