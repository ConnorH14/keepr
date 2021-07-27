<template>
  <div class="container-fluid flex-grow-1 mt-5 loading">
    <div class="row d-flex justify-content-center">
      <div class="col-lg-6 d-flex justify-content-end">
        <img :src="profile.profile.picture" alt="profile picture" class="rounded img-fluid" width="200">
      </div>
      <div class="col-lg-6">
        <h1>{{ profile.profile.name }}</h1>
        <h3>Vaults: {{ profile.vaults.length }}</h3>
        <h3>Keeps: {{ profile.keeps.length }}</h3>
        <h3>{{ account.name }}</h3>
      </div>
      <br>
      <div v-if="account.id == profile.profile.id" class="col-12 text-center mt-5">
        <span class="mdi mdi-briefcase-plus create-icon" role="button" title="create vault" data-toggle="modal" data-target="#newVault"></span>
        <span class="mdi mdi-image-plus create-icon" role="button" title="create keep" data-toggle="modal" data-target="#newKeep"></span>
      </div>
      <div class="col-12 ml-5 mt-5 mr-3">
        <h2 class="text-center">
          Vaults:
        </h2>
        <hr class="text-primary mx-5">
        <div class="row ml-5">
          <div class="col-2" v-for="vault in profile.vaults" :key="vault.id" @click="goToVault(vault.id)" role="button">
            <h1 v-if="vault.isPrivate == true" class="mdi mdi-treasure-chest vault-icon private"></h1>
            <h1 v-else class="mdi mdi-treasure-chest vault-icon"></h1>
            <sup>{{ vault.name }}</sup>
          </div>
        </div>
      </div>
      <div class="col-12 ml-5 mt-5">
        <h2 class="text-center">
          Keeps:
        </h2>
        <hr class="text-primary mx-5">
        <div v-masonry class="ml-5">
          <KeepCard v-for="keep in profile.keeps" :key="keep.id" :keep="keep" v-masonry-tile />
        </div>
      </div>
    </div>
  </div>
  <Loader />
  <NewKeep />
  <NewVault />
  <KeepModal v-for="keep in profile.keeps" :key="keep.id" :keep="keep" />
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import { useRoute, useRouter } from 'vue-router'
export default {
  name: 'Account',
  setup() {
    const route = useRoute()
    const router = useRouter()
    watchEffect(async() => {
      try {
        await profileService.getProfile(route.params.id)
      } catch (error) {
        console.log(error)
      }
    })
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      goToVault(_id) {
        router.push({ name: 'Vault', params: { id: _id } })
      }
    }
  }
}
</script>

<style scoped>
.loading{
  animation: create-show 6s ease-out;
}
.create-icon{
  font-size: 6em;
  color: #ffffff00;
  -webkit-text-stroke: 3px #000000;
  animation: create-show 3s ease-out;
  margin: 20px;
}
.vault-icon{
  font-size: 5em;
}
.private {
  color: #df3333;
  -webkit-text-stroke: 1px #000000;
}
@keyframes create-show{
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
