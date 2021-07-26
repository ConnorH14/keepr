<template>
  <div class="container-fluid flex-grow-1 ml-4 mt-5">
    <div class="row ml-4">
      <div class="col-12">
        <div v-masonry>
          <KeepCard v-for="k in keeps" :key="k.id" :keep="k" v-masonry-tile />
        </div>
      </div>
    </div>
    <div class="row">
      <div class="text-center m-5 mx-auto load-button">
        <h2>Load More</h2>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, watchEffect } from '@vue/runtime-core'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  setup() {
    watchEffect(async() => {
      try {
        await keepsService.getAllKeeps()
      } catch (error) {
        console.log(error)
      }
    })
    return {
      keeps: computed(() => AppState.activeKeeps)
    }
  }

}
</script>

<style>
.load-button {
  border: 2px solid black;
  width: fit-content;
  width: -moz-fit-content;
  padding: 10px;
  font-family: 'Secular One', sans-serif;
}

</style>
