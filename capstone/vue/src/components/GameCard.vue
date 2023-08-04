<template>
  <div>
    <h1>{{item.title}}</h1> 
    <h2>Description</h2>
    <p>{{item.description}}</p>
    <h2>Release Date</h2>
     <h3>{{item.release_date}}</h3>
  </div>
</template>

<script>
export default {
  props: ["item"],
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