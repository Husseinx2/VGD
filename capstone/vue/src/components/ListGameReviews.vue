<template>
  <section>
    <b-card-group
      class="list-reviews-card"
      >
      <b-card-header>{{user.username}}:</b-card-header>
      <b-card-body>{{ item.reviewContent }}</b-card-body>
      <b-card-footer>{{ item.reviewDateTime }}</b-card-footer>
    </b-card-group>
  </section>
</template>

<script>
import userService from "../services/UserService.js";
export default {
  props: ['item'],
  data() {
    
    return {
 
      user: "",
    };
  },
  methods: {
    getUsernames(userId) {
      userService.GetUser(userId).then((response) => {
        this.user = response.data;
      });
    },
  },
  created() {
    this.getUsernames(this.item.reviewerId)
  },
};
</script>

<style>
</style>