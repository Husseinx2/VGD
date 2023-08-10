<template>
  <section class="ProfileComp">
       <h1> {{user.username}}</h1>
  </section>
</template>

<script>
import UserService from '../services/UserService';
export default {
    props:['item'],
  name: "profileComp",
  data() {
    return {
      user:{}
    }
  },
  methods: {
    GetUser() {
      console.log("reached")
         UserService.GetUser(this.item).then((response) => {
           this.user = response.data;
         })
    }
      
  },
  created() {
    this.GetUser();
  }
};
</script>

<style scoped>
section.Profile {
  flex-wrap: wrap;
  display: inline-block;
  height: 120px;
  min-width: 300px;
  border: 2px solid black;
  border-radius: 10px;
  margin: 20px;
}
</style>