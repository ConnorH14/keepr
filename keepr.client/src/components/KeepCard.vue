<template>
  <div class="keep-card" data-toggle="modal" :data-target="'#m'+keep.id">
    <img :src="keep.img" :alt="keep.name">
    <div class="keep-title-div">
      <h1 class="keep-title">
        {{ keep.name }}
      </h1>
      <img :src="keep.creator.picture" :alt="keep.name" class="keep-creator-picture">
    </div>
  </div>
  <!-- <KeepModal :keep="keep" /> -->
</template>

<script>
export default {
  props: { keep: { type: Object, required: true } }
}
</script>

<style>
.keep-card {
  margin: 10px;
  width: 250px;
  position: relative;
  cursor: pointer;
  font-family: 'Secular One', sans-serif;
  animation: card-show 6s ease-out;
}
.keep-card>img {
  width: 100%;
  filter: drop-shadow(10px 10px 5px #413e3e);
  border-radius: 10px;
}
.keep-title-div {
  position: absolute;
  bottom: 0px;
  left: 0px;
  width: 100%;
  color: #f0ecec;
  box-shadow: 0px -5px 20px 2px #373738 inset;
  border-radius: 0 0 10px 10px;
}
.keep-title {
  color: #f0ecec;
  text-shadow: 2px 2px black;
  font-size: 1.5em;
  margin-left: 10px;
}
.keep-creator-picture{
  width: 30px;
  height: 30px;
  position: absolute;
  right: 10px;
  top: 5px;
  border-radius: 50%;
  border: 1px solid black;
}

@keyframes card-show{
  0%{
    opacity: 0;
  }
  50%{
    opacity: 0;
  }
  90%{
    opacity: 0;
  }
  100%{
    opacity: 100;
  }
}

</style>
