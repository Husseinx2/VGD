<template>
  <div class="container text-center">
    <b-collapse id="collapseJumbotron" visible>
      <b-jumbotron header="We're Hiring!" class="jumbotron">
        <b-button
          class="close-button"
          v-b-toggle.collapseJumbotron
          variant="link"
        >
          <span aria-hidden="true">&times;</span>
        </b-button>
        <p>Looking for full stack web developers fresh out of bootcamp.</p>
        <b-button variant="primary" v-bind:to="{name:'careers'}">Apply now</b-button>
      </b-jumbotron>
    </b-collapse>
  </div>
</template>

<style scoped>
.jumbotron {
  position: relative;
  background-color: #c5e2ff;
}

.close-button {
  position: absolute;
  top: 0.5rem;
  right: 0.5rem;
  padding: 0;
  font-size: 1.5rem;
  line-height: 1;
  color: red;
}
</style>