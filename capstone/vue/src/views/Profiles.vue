<template>
  <div>
    <h1>Admin Only</h1>
    <b-table striped hover :items="users"> </b-table>
  </div>
</template>

<script>
import UserService from "../services/UserService";
export default {
  data() {
    return {
      users: [],
    };
  },
  methods: {
    sortUsers() {},
    getUsers() {
      UserService.listUsers()
        .then((response) => {
          this.users = response.data;
          
        })
        .catch(() => {
          console.log("error with server");
        });
    },
  },
  created() {
    this.getUsers();
  },
};
</script>

<style>
</style>