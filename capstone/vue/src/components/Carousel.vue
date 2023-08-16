<template>
  <div class="container">
    <b-carousel
      id="carousel-1"
      v-model="slide"
      :interval="4000"
      controls
      indicators
      background="#ababab"
      style="text-shadow: 1px 1px 2px #333;"
      @sliding-start="onSlideStart"
      @sliding-end="onSlideEnd"
    >
      <b-carousel-slide
        img-src="https://wallpapers.com/images/featured/fortnite-pictures-b5kfcchwazwiz3cs.jpg"
    
      >Fortnite has been voted by the VGD staff as the greatest video game of all time.</b-carousel-slide>

      <b-carousel-slide
        img-src="https://cdn-ext.fanatical.com/production/product/1280x720/f203315f-dd14-4492-8cd1-d422016820e8.jpeg"
      >Tom Clancy's Rainbow Six® Siege is an elite, tactical team-based shooter where superior planning and execution triumph.
      </b-carousel-slide>

      <b-carousel-slide
        img-src="https://www.charlieintel.com/cdn-cgi/image/width=3840,quality=75,format=auto/https://editors.charlieintel.com/wp-content/uploads/2023/06/13/Is-Madden-24-crossplay.jpg"
      >Experience the newest iteration of FieldSENSE™ in Madden NFL 24. More realistic character movement and smarter AI gives you control to play out your gameplay strategy with the confidence to dominate any opponent</b-carousel-slide>

      <b-carousel-slide
        img-src="https://image.api.playstation.com/vulcan/ap/rnd/202009/2903/R0n896hszVzJ6ZziHFKAq7Ev.jpg"
      >Prey - Rated M for Masterpiece!
      </b-carousel-slide>
    </b-carousel>
  </div>
</template>

<script>
export default {
  data() {
    return {
      slide: 0,
      sliding: null,
    };
  },
  methods: {
    onSlideStart(slide) {
      console.log(slide);
      this.sliding = true;
    },
    onSlideEnd(slide) {
      console.log(slide);
      this.sliding = false;
    },
  },
};
</script>
<style scoped>
.container {
  max-width: 70%;
  max-height: 50%;
}
</style>