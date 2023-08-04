<template>
  <div>
    <h1>{{item.title}}</h1> 
    <h2>Description</h2>
    <p>{{item.description}}</p>
    <h2>Release Date</h2>
     <h3>{{new Date(item.releaseDate).toLocaleString('en', options)}}</h3>

     <router-link v-show="$store.state.user.role == 'admin'"  >  Edit</router-link>       <router-link to="/" v-show="$store.state.user.role == 'admin'">Delete</router-link> 

  </div>
</template>

<script>
export default {
  props: ["item"],
  data () {
    return {
      options: { year: 'numeric', month: 'long', day: 'numeric'}
    }
  }
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