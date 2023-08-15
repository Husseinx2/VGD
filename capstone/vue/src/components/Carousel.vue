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
      <!-- Text slides with image -->
      <b-carousel-slide
        caption="Fortnite"
        text="Fortnite is a third-person shooter game where up to 100 players compete to be the last person or team standing. You can compete alone or join a team of up to four."
        img-src="https://wallpapers.com/images/featured/fortnite-pictures-b5kfcchwazwiz3cs.jpg"
    
      ></b-carousel-slide>

      <!-- Slides with custom text -->
      <b-carousel-slide
        text="Tom Clancy's Rainbow Six® Siege is an elite, tactical team-based shooter where superior planning and execution triumph."
        caption="Rainbow Six Siege"
        img-src="https://cdn.akamai.steamstatic.com/steam/apps/359550/capsule_616x353.jpg?t=1690498575"
      >
      </b-carousel-slide>

      <!-- Slides with image only -->
      <b-carousel-slide
        text="Experience the newest iteration of FieldSENSE™ in Madden NFL 24. More realistic character movement and smarter AI gives you control to play out your gameplay strategy with the confidence to dominate any opponent"
        caption="Madden 24"
        img-src="https://imageio.forbes.com/specials-images/imageserve/64a2f116a206ca5b7daef4a8/EA-Sports-Madden-24/960x0.jpg?height=399&width=711&fit=bounds"
      ></b-carousel-slide>
    </b-carousel>

    <p class="mt-4">
      Slide #: {{ slide }}<br />
      Sliding: {{ sliding }}
    </p>
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