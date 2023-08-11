<template>
  <section class="ProfileComp">
    <h1>User: {{ user.username }}</h1>
    <button v-show="(item != $store.state.user.userId ) && (user.role !='admin')"  v-b-modal="`${user.username}`" v-bind:key="user.userId">
    <b> Delete {{user.username}}</b> <b-icon icon="trash" />
    </button>
     <b-modal
      ok-variant="danger"
      ok-title="DELETE"
      cancel-title="CANCEL"
      @ok="deleteUser"
      v-bind:id="user.username"
    >
      Are you sure you want to delete {{ user.username }}?
    </b-modal>
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
    deleteUser() {
      UserService.DeleteUser(this.item).then(() => {
           this.$router.push("/profiles");
      })
    }
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
button:hover {
  background-color:red;
}
</style>