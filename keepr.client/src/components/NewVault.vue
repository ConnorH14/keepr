<template>
  <div class="modal fade"
       id="newVault"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <small class="modal-exit-desk" title="exit" data-dismiss="modal"><i class="mdi mdi-exit-to-app"></i></small>
          <h3>New Vault</h3>
          <form @submit.prevent="createVault">
            <div class="form-group">
              <label for="vaultName" class="sr-only">Vault Name</label>
              <input type="text"
                     class="form-control"
                     id="vaultName"
                     placeholder="Name your vault..."
                     v-model="state.vaultData.name"
                     required
                     minlength="3"
                     maxlength="10"
              >
            </div>
            <div class="form-group">
              <label for="vaultDescription" class="sr-only">Vault Description</label>
              <input type="text"
                     class="form-control"
                     id="vaultDescription"
                     placeholder="Tell us about your vault..."
                     v-model="state.vaultData.description"
                     required
                     minlength="3"
                     maxlength="255"
              >
            </div>
            <div class="form-check">
              <input type="checkbox" class="form-check-input" id="isPrivate" v-model="state.vaultData.isPrivate">
              <label class="form-check-label" for="isPrivate">Is this vault private?</label>
            </div>
            <button type="submit" class="btn btn mt-2">
              Submit
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { vaultService } from '../services/VaultService'
import $ from 'jquery'
import { AppState } from '../AppState'
export default {
  setup() {
    const state = reactive({
      vaultData: {}
    })
    return {
      state,
      async createVault(event) {
        try {
          await vaultService.createVault(AppState.account.id, state.vaultData)
          event.target.reset()
          $('#newVault').modal('hide')
        } catch (error) {
          console.log(error)
        }
      }
    }
  }
}
</script>

<style>

</style>
