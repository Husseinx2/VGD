<template>
  <div class="container ">
    <table class="table">
      <tbody>
        <tr>
          <th scope="row">Release Date</th>
          <td>{{ new Date(item.releaseDate).toLocaleString("en", options)  }}</td>
        </tr>
        <tr>
          <th scope="row">Genre</th>
          <td>{{ item.genres.join(", ") }}</td>
        </tr>
        <tr>
          <th scope="row">Platform</th>
          <td>{{ item.platforms.join(", ") }}</td>
        </tr>
        <tr>
          <th scope="row">Developer</th>
          <td>{{ item.developers.join(", ") }}</td>
        </tr>
             <tr>
          <th scope="row">Publisher</th>
          <td>{{ item.publishers.join(", ") }}</td>
        </tr>
      </tbody>
    </table>
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

</style>