<template>
  <div>
      <h1> 
       {{item.id}} || {{item.title}} || {{item.description}} || {{item.release_date}}
      </h1>
  </div>
</template>

<script>
export default {
    props: ['item']
}
</script>

<style>

</style>