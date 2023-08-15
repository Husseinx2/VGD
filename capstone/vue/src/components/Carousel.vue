<template>
  <div class="container">
    <b-carousel
      id="carousel-1"
      v-model="slide"
      :interval="4000"
      controls
      indicators
      background="#ababab"
      img-width="1024"
      img-height="480"
      style="text-shadow: 1px 1px 2px #333"
      @sliding-start="onSlideStart"
      @sliding-end="onSlideEnd"
    >
      <b-carousel-slide
        caption="Fortnite"
        text="Fortnite is a third-person shooter game where up to 100 players compete to be the last person or team standing. You can compete alone or join a team of up to four."
        img-src="https://wallpapers.com/images/featured/fortnite-pictures-b5kfcchwazwiz3cs.jpg"
    
      ></b-carousel-slide>

      <b-carousel-slide
        text="Tom Clancy's Rainbow Six® Siege is an elite, tactical team-based shooter where superior planning and execution triumph."
        caption="Rainbow Six Siege"
        img-src="https://cdn-ext.fanatical.com/production/product/1280x720/f203315f-dd14-4492-8cd1-d422016820e8.jpeg"
      >
      </b-carousel-slide>

      <b-carousel-slide
        text="Experience the newest iteration of FieldSENSE™ in Madden NFL 24. More realistic character movement and smarter AI gives you control to play out your gameplay strategy with the confidence to dominate any opponent"
        caption="Madden 24"
        img-src="https://imageio.forbes.com/specials-images/imageserve/64a2f116a206ca5b7daef4a8/EA-Sports-Madden-24/960x0.jpg?height=399&width=711&fit=bounds"
      ></b-carousel-slide>
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