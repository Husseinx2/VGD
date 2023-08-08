<template>
  <div class="card">
    <router-link :to="{ name: 'game', params: { id: item.id } }">
      <h1>{{ item.title }}</h1>
      <p>{{ item.description }}</p>
      <p>{{ new Date(item.releaseDate).toLocaleString("en", options) }}</p>
      <span v-show="$store.state.user.role == 'admin'">
        <div class="b-btn-toolbar">
          <b-button-group class="mx-1">
            <b-button class = "btn btn-warning" v-bind:to="{ name: 'edit', params: { id: item.id } }">
              Edit</b-button
            >
          </b-button-group>
          <b-button-group class="mx-1">
            <b-button class= "btn btn-danger" v-bind:to="{ name: 'delete', params: { id: item.id } }"
              >Delete</b-button
            >
          </b-button-group>
        </div>
      </span>
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
  border: 1px solid blue;
  width: 40%;
  border-radius: 50px;
  text-align: center;
  background-color: aqua;
  margin: 10px;
}

div.card:hover {
  border: 1px solid black;
  background-color: rgb(17, 206, 206);
  cursor: pointer;
}
</style>