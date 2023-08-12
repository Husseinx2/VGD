<template>
  <section>
    <b-card-group
      >
      <b-card class="list-reviews-card">
      <b-card-header>
      <h6 class="mb-0">{{user.username}}</h6>
      </b-card-header>
      <b-card-body>{{ item.reviewContent }}</b-card-body>
      <b-card-footer class="card-footer">Posted: {{ new Date(item.reviewDateTime).toLocaleString("en", options) }}</b-card-footer>
        <button v-show="$store.state.user.userId == item.reviewerId">
        Edit
      </button>
      </b-card>
     
    </b-card-group>
  </section>
</template>

<script>
import userService from "../services/UserService.js";
export default {
  props: ['item'],
  data() {
    
    return {
      options: { year: "numeric", month: "long", day: "numeric" },
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

<style scoped>
.card-footer{
    font-size:10px
}
.list-reviews-card{
    width: 50%;    
}
</style>