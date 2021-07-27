<template>
  <div class="container-fluid flex-grow-1">
    <div class="row ml-5 mt-5">
      <div class="col-6 text-center">
        <h1>{{ vault.name }}</h1>
      </div>
      <div class="col-5">
        <h2>
          <em>About this vault:</em>
        </h2>
        <h3>{{ vault.description }}</h3>
      </div>
      <div class="col-1" v-if="(logged == true)&&(gotVault == true)">
        <h1 class="mdi mdi-trash-can vault-delete"
            role="button"
            @click="deleteVault(vault.id)"
            v-if="account.id == vault.creator.id"
        ></h1>
      </div>
    </div>
    <div class="row mx-5">
      <div class="col-12">
        <hr class="text-primary mx-5">
      </div>
    </div>
    <div class="row">
      <div class="col-12 ml-5">
        <div v-masonry class="ml-5">
          <KeepCard v-for="keep in keeps" :key="keep.id" :keep="keep" v-masonry-tile />
        </div>
      </div>
    </div>
  </div>
  <Loader />
  <KeepModal v-for="keep in keeps" :key="keep.id" :keep="keep" />
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import { vaultService } from '../services/VaultService'
import { useRoute, useRouter } from 'vue-router'
import { AppState } from '../AppState'
export default {
  setup() {
    const router = useRouter()
    const route = useRoute()
    onMounted(async() => {
      try {
        const gotVault = await vaultService.getVaultById(route.params.id)
        await vaultService.getKeepsByVault(route.params.id)
        AppState.gotVault = gotVault
      } catch (error) {
        console.log(error)
        router.push({ name: 'Home' })
      }
    })
    return {
      logged: computed(() => AppState.signedIn),
      gotVault: computed(() => AppState.gotVault),
      account: computed(() => AppState.account),
      vault: computed(() => AppState.activeVault.vault),
      keeps: computed(() => AppState.activeVault.keeps),
      async deleteVault(id) {
        try {
          if (confirm('Are you sure you want to delete this vault?')) {
            const deleted = await vaultService.deleteVault(id)
            if (deleted) {
              router.push({ name: 'Account', params: { id: AppState.account.id } })
            }
          }
        } catch (error) {
          console.log(error)
        }
      }
    }
  }
}
</script>

<style>
.vault-delete:hover{
  color: #d42929;
}

</style>
