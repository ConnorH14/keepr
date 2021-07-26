<template>
  <div class="loader">
    <span class="dot1 mdi mdi-square-rounded"></span>
    <span class="dot2 mdi mdi-square-rounded"></span>
    <span class="dot3 mdi mdi-square-rounded"></span>
  </div>
</template>

<script>
export default {

}
</script>

<style>
.loader {
  position: fixed;
  top: 200px;
  left: 50vw;
}
.dot1 {
  animation: loader-hide 1s ease-out;
  opacity: 0;
}
.dot2{
  animation: loader-hide 3s ease-out;
  opacity: 0;
}
.dot3{
  animation: loader-hide 5s ease-out;
  opacity: 0;
}

@keyframes loader-hide{
  0%{
    opacity: 100;
  }
  100%{
    opacity: 0;
  }
}

</style>
