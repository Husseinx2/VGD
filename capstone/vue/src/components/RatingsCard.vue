<template>
    <div>
        <section>
    <label class="label">How would you rate this game?</label>
    </section>
    <b-form-rating size="lg" no-border show-clear class="rating" id="rating-inline-center"  inline v-bind:value="item.ratingValue" color="orange"></b-form-rating>
  </div>
</template>

<script>
export default {
    props:['item'],
 data() {
    return {
        
    }
 },


 

}
</script>

<style scoped>
.label{
    size-adjust: 10;
}
</style>