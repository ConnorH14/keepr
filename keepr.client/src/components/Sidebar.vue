<template>
  <div class="sidebar">
    <span class="mdi mdi-home-variant sidebar-icon" title="Home" @click="goHome"></span>
    <br>
    <span class="mdi mdi-camera-burst sidebar-icon" title="Keeps" @click="goKeeps"></span>
    <br>
    <span v-if="!account.id" class="mdi mdi-account-question sidebar-icon" title="Login" @click="login"></span>
    <br>
    <span v-if="account.id" class="mdi mdi-bank sidebar-icon" title="Vaults" @click="goVaults"></span>
    <br>
    <span v-if="account.id" class="mdi mdi-account sidebar-icon" title="Account" @click="goAccount"></span>
    <br>
    <span v-if="account.id" class="mdi mdi-exit-to-app sidebar-icon" title="Logout" @click="logout"></span>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'

export default {
  setup() {
    const router = useRouter()
    return {
      account: computed(() => AppState.account),
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
        router.push({ name: 'Account', params: { id: AppState.account.id } })
      },
      async logout() {
        if (await confirm('Are you sure you would like to logout?')) {
          AuthService.logout({ returnTo: window.location.origin })
        }
      },
      login() {
        AuthService.loginWithRedirect()
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
  z-index: 10;
}

.sidebar-icon {
  font-size: 3em;
  cursor: pointer;
}

</style>
