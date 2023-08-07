<template>
  <div class="container">
    <h1>{{ item.title }}</h1>
    Description:
    <p>{{ item.description }}</p>

    <ul>
      Platforms:
      <li v-for="platform in item.platforms" v-bind:key="platform">
        {{ platform }}
      </li>
    </ul>
    <ul>
      Developers:
      <li v-for="developer in item.developers" v-bind:key="developer">
        {{ developer }}
      </li>
    </ul>
    <ul>
      Publishers:
      <li v-for="publisher in item.publishers" v-bind:key="publisher">
        {{ publisher }}
      </li>
    </ul>
    <ul>
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

<style>
.container {
  position: relative;
  top: 250px;
  display: grid;
  grid-template: 1fr 1fr 1fr;
  grid-template-areas:
    ". title ."
    "description description description"
    "genre . ."
    "platforms . ."
    "developers . ."
    "publisher . ."
}
.title {
  grid-area: "title";
}
.description {

}
P,
h1,
ul,
li {
  border: 1px solid black;
}
</style>