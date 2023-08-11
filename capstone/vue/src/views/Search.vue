<template>
  <div>
      <search-result v-bind:item="searchParameters"/>
  </div>
</template>

<script>
import SearchResult from '../components/SearchResult.vue';
export default {
  components: { SearchResult },
  data() {
    return {
      searchParameters: "",
    };
  },
  created() {
    this.searchParameters = this.getSearchParamsObject();
  },
  methods: {
    getSearchParamsObject() {
      const searchParams = new URLSearchParams(this.$route.fullPath.split('?')[1]);
      const searchParamsObject = {};
      for (const [key, value] of searchParams.entries()) {
        searchParamsObject[key] = value;
      }
      console.log(searchParamsObject);
      return searchParamsObject;
      
    }
  }
};
</script>

<style>
</style>