<template>
  <div class="container">
    <h1 class="title">{{ item.title }}</h1>
    <img v-bind:src="item.imageUrl" />

    Description:
    <p class="description">{{ item.description }}</p>

    <ul class="platforms">
      Platforms:
      <li v-for="platform in item.platforms" v-bind:key="platform">
        {{ platform }}
      </li>
    </ul>
    <ul class="developers">
      Developers:
      <li v-for="developer in item.developers" v-bind:key="developer">
        {{ developer }}
      </li>
    </ul>
    <ul class="publishers">
      Publishers:
      <li v-for="publisher in item.publishers" v-bind:key="publisher">
        {{ publisher }}
      </li>
    </ul>
    <ul class="genres">
      Genres:
      <li v-for="genre in item.genres" v-bind:key="genre">
        {{ genre }}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  props: ["item"],
};
</script>

<style scoped>
img {
  justify-self: center;
}
.container {
  display: grid;
  grid-template: 1fr 1fr;
  grid-template-areas:
    ". title "
    "description"
    "genre "
    "platforms "
    "developers "
    "publishers ";
}
.title {
  grid-area: "title";
}
.description {
  grid-area: "description";
}
.genre {
  grid-area: genre;
}
.platforms {
  grid-area: "platforms";
}
.developers {
  grid-area: "developers";
}
.publishers {
  grid-area: "publishers";
}
P,
h1,
ul,
li {
  border: 1px solid black;
}
</style>