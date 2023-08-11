<template>
  <div>
    <h1
      style="
        color: #3e75e2;
        margin-left: 20px;
        margin-top: 12px;
        margin-bottom: 10px;
        font-family: Georgia, 'Times New Roman', Times, serif;
        fontweight: 600;
      "
    >
      Admin Only
    </h1>
    <b-table
      style="
        font-family: Georgia, 'Times New Roman', Times, serif;
        width: 50%;
        text-align: center;
        margin-left: 25%;
        background-color: #e9f4ff;
      "
      select-mode="single"
      striped
      selectable
      hover
      :items="users"
      @row-selected="onRowSelected"
    >
    </b-table>
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
    onRowSelected(item) {
      console.log(item[0].userId);
      this.$router.push({ name: `profile`, params: { id: item[0].userId } });
    },
    getUsers() {
      UserService.listUsers()
        .then((response) => {
          this.users = response.data.filter((user) => {
            return user.userId != this.$store.state.user.userId;
          });
        })
        .catch((error) => {
          console.log("error with server at table" + error);
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