<template>
  <div>
    <router-link :to="{ name: 'game', params:{id: item.id }}">{{ item.title }}</router-link>
    <h4>Description</h4>
    <p>{{ item.description }}</p>
    <h4>Release Date:</h4>
    <p>{{ new Date(item.releaseDate).toLocaleString("en", options) }}</p>
   <span v-show="$store.state.user.role == 'admin'" >
    <router-link
    
      v-bind:to="{ name: 'edit', params: { id: item.id } }"
    >
      Edit</router-link
    >
    ||
    <router-link to="/" 
      >Delete</router-link
    >
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
}
</style>