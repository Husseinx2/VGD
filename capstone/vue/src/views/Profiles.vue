<template>
  <div>
    <h1 style="margin-left:50px; margin-top:12px; margin-bottom:10px; font-family: Georgia, 'Times New Roman', Times, serif;">Admin Only</h1>
    <b-table style="font-family: Georgia, 'Times New Roman', Times, serif; width: 50%; text-align:center; margin-left: 25%; background-color: #e9f4ff;" select-mode="single" striped selectable hover :items="users"  @row-selected="onRowSelected"> </b-table>
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
      this.$router.push({ name: `profile`, params: { id: item[0].userId } })
   },
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