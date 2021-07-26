<template>
  <div class="sidebar">
    <span class="mdi mdi-home-variant sidebar-icon" title="Home" @click="goHome"></span>
    <br>
    <span class="mdi mdi-camera-burst sidebar-icon" title="Keeps" @click="goKeeps"></span>
    <br>
    <span class="mdi mdi-bank sidebar-icon" title="Vaults" @click="goVaults"></span>
    <br>
    <span class="mdi mdi-account sidebar-icon" title="Account" @click="goAccount"></span>
  </div>
</template>

<script>
import { useRouter } from 'vue-router'
export default {
  setup() {
    const router = useRouter()
    return {
      goHome() {
        router.push({ name: 'Home' })
      },
      goKeeps() {
        router.push({ name: 'Keeps' })
      },
      goVaults() {
        router.push({ name: 'Keeps' })
      },
      goAccount() {
        router.push({ name: 'Account' })
      }
    }
  }

}
</script>

<style>
.sidebar {
  position: fixed;
  top: 50px;
  left: 20px;
}

.sidebar-icon {
  font-size: 3em;
  cursor: pointer;
}

</style>
