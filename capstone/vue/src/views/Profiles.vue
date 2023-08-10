<template>
  <div>
    <h1>Admin Only</h1>
    <b-table select-mode="single" selectable hover :items="users"  @row-selected="onRowSelected" > </b-table>
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
      console.log(item);
      this.$router.push({name:"home",  params: { id: item.id }})
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