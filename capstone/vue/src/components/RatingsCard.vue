<template>
    <div>
    <label for="rating-inline-center">How would you rate this game?</label>
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
.rating{
    align-content: center;
    vertical-align: middle;
    
    
}
</style>