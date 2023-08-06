<template>
      <header>
      <table>
        <td>
      <p>VGD</p>
        </td>
        <td>
     <input placeholder="Search"  type="text" v-model="search" :for="game in filteredList"
        v-bind:key="game.id"
        v-bind:item="game"/>
        </td>
        <td>
          <button type="submit">Submit</button>
        </td>
      </table>
    </header>
</template>

<script>

export default {

}
</script>

<style>

</style>