<template>
  <div>
    <router-link :to="{ name: 'game', params:{id: item.id }}">{{ item.title }}</router-link>
    <h4>Description</h4>
    <p>{{ item.description }}</p>
    <h2>Release Date</h2>
    <h3>{{ new Date(item.releaseDate).toLocaleString("en", options) }}</h3>
    <span v-show="$store.state.user.role == 'admin'">
      <router-link v-bind:to="{ name: 'edit', params: { id: item.id } }">
        Edit</router-link
      >
      ||
      <router-link v-bind:to="{ name: 'delete', params: { id: item.id } }" >Delete</router-link>
    </span>
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
div {
  border: 1px solid blue;
  width: 40%;
  border-radius: 50px;
  text-align: center;
  background-color: aqua;
  margin: 10px;
  
}

div:hover {
    border: 1px solid black;
    background-color: rgb(17, 206, 206);
    cursor:pointer;
}
</style>