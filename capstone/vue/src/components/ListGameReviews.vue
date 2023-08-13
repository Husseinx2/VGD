<template>
  <section>
    <b-card-group
      >
      <b-card class="list-reviews-card">
      <b-card-header>
      <h6 class="mb-0">{{user.username}}</h6>
      </b-card-header>
       <router-link :to="{name: 'review', params:{id: item.reviewId}}">
      <b-card-body v-show="!showEdit">{{ item.reviewContent }}</b-card-body>
       </router-link>
      <edit-review-form v-bind:item="item" v-show="showEdit" />
      <b-card-footer class="card-footer">Posted: {{ new Date(item.reviewDateTime).toLocaleString("en", options) }}</b-card-footer>
        <button @click="showEdit = !showEdit" v-show="$store.state.user.userId == item.reviewerId">
        Edit
      </button>
      </b-card>
     
    </b-card-group>
  </section>
</template>

<script>
import userService from "../services/UserService.js";
import editReviewForm from './EditReviewForm.vue';
export default {
  components: {editReviewForm  },
  props: ['item'],
  data() {
    
    return {
      showEdit: false,
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