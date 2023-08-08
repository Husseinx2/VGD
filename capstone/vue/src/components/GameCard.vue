<template>
  <div>
    <router-link
      class="d-flex justify-content-center"
      v-bind:to="{ name: 'game', params: { id: item.id } }"
    >
      <b-card footer-tag="footer">
        <template #header >
          <h2 class="mb-0">{{ item.title }}</h2>
        </template>
        <b-card-img v-bind:src="item.imageUrl" style="max-width: 20rem" />
        <b-card-text>{{ item.description }}</b-card-text>
        <b-button-group class="mx-1">
          <b-button
            class="btn btn-warning"
            v-bind:to="{ name: 'edit', params: { id: item.id } }"
          >
            Edit</b-button
          >
        </b-button-group>
        <b-button-group class="mx-1">
          <b-button
            class="btn btn-danger"
            v-bind:to="{ name: 'delete', params: { id: item.id } }"
            >Delete</b-button
          >
        </b-button-group>
        <template #footer>
          <h3>
            {{ new Date(item.releaseDate).toLocaleString("en", options) }}
          </h3>
        </template>
      </b-card>
    </router-link>

    <!-- <div class="card">
      <router-link :to="{ name: 'game', params: { id: item.id } }">
        <b-img :src="item.imageUrl" fluid alt=""></b-img>
        <h1>{{ item.title }}</h1>
        <p>{{ item.description }}</p>
        <p>{{ new Date(item.releaseDate).toLocaleString("en", options) }}</p>
        <span v-show="$store.state.user.role == 'admin'">
          <div class="b-btn-toolbar">
            <b-button-group class="mx-1">
              <b-button
                class="btn btn-warning"
                v-bind:to="{ name: 'edit', params: { id: item.id } }"
              >
                Edit</b-button
              >
            </b-button-group>
            <b-button-group class="mx-1">
              <b-button
                class="btn btn-danger"
                v-bind:to="{ name: 'delete', params: { id: item.id } }"
                >Delete</b-button
              >
            </b-button-group>
          </div>
        </span>
      </router-link> -->
    <!-- </div> -->
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
  border: 1px solid rgb(187, 187, 206);
  width: 40%;
  /* border-radius: 50px; */
  text-align: center;
  /* margin: 10px; */
}

div.card:hover {
  border: 1px solid black;
  background-color: rgb(195, 202, 202);
  cursor: pointer;
}

</style>