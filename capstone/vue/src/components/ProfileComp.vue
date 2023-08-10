<template>
    <section class="ProfileComp">
        <p>Id: {{ item.id }}</p>
        <p>Username: {{ item.usernme }}</p>
    </section>
</template>

<script>
export default {
    name: "profileComp",
    props: ["item"],
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