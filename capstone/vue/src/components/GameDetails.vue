<template>
  <div class= "container">
    <table class="table-secondary">
      <tbody style="background-color: white">
        <tr>
          <th scope="row">Release Date</th>
          <td>{{ new Date(item.releaseDate).toLocaleString("en", options)  }}</td>
        </tr>
         <tr>
          <th scope="row">ESRB Rating</th>
          <td>{{ item.esrbRating }}</td>
        </tr>
        <tr>
          <th scope="row">Genre(s)</th>
          <td>{{ item.genres.join(", ") }}</td>
        </tr>
        <tr>
          <th scope="row">Platform(s)</th>
          <td>{{ item.platforms.join(", ") }}</td>
        </tr>
        <tr>
          <th scope="row">Developer(s)</th>
          <td>{{ item.developers.join(", ") }}</td>
        </tr>
             <tr>
          <th scope="row">Publisher(s)</th>
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
.table-secondary{
  background-color: white;
}
</style>