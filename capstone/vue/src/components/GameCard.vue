<template>
  <div>
    <router-link
      class="d-flex justify-content-center"
      v-bind:to="{ name: 'game', params: { id: item.id } }"
    >
      <b-card footer-tag="footer">
        <template #header>
          <h2 class="mb-0">{{ item.title }}</h2>
        </template>
        <b-card-img v-bind:src="item.imageUrl" style="max-width: 20rem" />
        <b-card-text class="description">{{ item.description }}</b-card-text>
        <b-button-group class="mx-1" v-show="$store.state.user.role == 'admin'">
          <b-button
            class="btn btn-warning"
            v-bind:to="{ name: 'edit', params: { id: item.id } }"
          >
            Edit</b-button
          >
        </b-button-group>
        <b-button-group class="mx-1" v-show="$store.state.user.role == 'admin'">
          <b-button
            class="btn btn-danger"
            v-bind:to="{ name: 'delete', params: { id: item.id } }"
            >Delete</b-button
          >
        </b-button-group>
        <template #footer>
          <h3>
            {{ new Date(item.releaseDate).toLocaleString("en", options) }}
          </h3>
        </template>
      </b-card>
    </router-link>
  </div>
</template>

<script>
export default {
  props: ["item"],
  data() {
    return {
      options: { year: "numeric", month: "long", day: "numeric" },
    };
  },
};
</script>

<style scoped>
div.card {
  border: 1px solid rgb(187, 187, 206);
  width: 40%;
  text-align: center;
  color: black;
}

div.card:hover {
  border: 1px solid black;
  background-color: rgb(235, 243, 243);
  cursor: pointer;
}
</style>