<template>
  <div class="keep-card">
    <img :src="keep.img" :alt="keep.name">
    <h1 class="keep-title">
      {{ keep.name }}
    </h1>
  </div>
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
}
.keep-card>img {
  width: 100%;
  filter: drop-shadow(10px 10px 5px #413e3e);
  border-radius: 10px;
}
.keep-title {
  position: absolute;
  bottom: 5px;
  left: 10px;
  color: #f0ecec;
  text-shadow: 4px 4px black;
}

</style>
