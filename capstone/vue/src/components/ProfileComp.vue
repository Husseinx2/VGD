<template>
  <section class="ProfileComp">
    <h1>User: {{ user.username }}</h1>
    <profile-rating-section v-bind:item="item" />
  </section>
</template>

<script>
import UserService from "../services/UserService";
import ProfileRatingSection from "./ProfileRatingSection.vue";
export default {
  components: { ProfileRatingSection },
  props: ["item"],
  name: "profileComp",
  data() {
    return {
      user: {},
    };
  },
  methods: {
    GetUser() {
      console.log("reached");
      UserService.GetUser(this.item)
        .then((response) => {
          this.user = response.data;
        })
        .catch(() => {
          this.$router.push({ name: "notFound" });
        });
    },
  },
  created() {
    this.GetUser();
  },
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