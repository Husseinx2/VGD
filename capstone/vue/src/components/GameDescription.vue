<template>
  <div class="card container my-5">
    <div class="container mt-2">
      <span v-show="$store.state.user.role == 'admin'">
        <div class="b-btn-toolbar float-right">
          <b-button-group class="mx-1">
            <b-button
              class="btn btn-warning"
              v-bind:to="{ name: 'edit', params: { id: item.id } }"
            >
              Edit <b-icon icon="pencil-fill" aria-hidden="true"></b-icon>
            </b-button>
          </b-button-group>
          <b-button-group class="mx-1">
            <b-button
              class="btn btn-danger"
              v-bind:to="{ name: 'delete', params: { id: item.id } }"
              >Delete <b-icon icon="trash" aria-hidden="true"></b-icon
            ></b-button>
          </b-button-group>
        </div>
      </span>
    </div>
    <div class="p-5 text-center bg-body-tertiary rounded-3">
      <b-img :src="item.imageUrl" fluid alt=""></b-img>
      <h1>{{ item.title }}</h1>
      <p>{{ item.description }}</p>
    </div>
  </div>
</template>

<script>
export default {
  props: ["item"],
};
</script>

<style scoped>
</style>