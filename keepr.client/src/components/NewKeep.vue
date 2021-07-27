<template>
  <div class="modal fade"
       id="newKeep"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <small class="modal-exit-desk" title="exit" data-dismiss="modal"><i class="mdi mdi-exit-to-app"></i></small>
          <h3>New Keep</h3>
          <form @submit.prevent="createKeep">
            <div class="form-group">
              <label for="keepName" class="sr-only">Keep Name</label>
              <input type="text"
                     class="form-control"
                     id="keepName"
                     placeholder="Name your keep..."
                     v-model="state.keepData.name"
                     minlength="3"
                     maxlength="10"
                     required
              >
            </div>
            <div class="form-group">
              <label for="keepDescription" class="sr-only">Keep Description</label>
              <input type="text"
                     class="form-control"
                     id="keepDescription"
                     placeholder="Tell us about your keep..."
                     v-model="state.keepData.description"
                     minlength="3"
                     maxlength="255"
                     required
              >
            </div>
            <div class="form-group">
              <label for="keepImg" class="sr-only">Keep Image</label>
              <input type="text"
                     class="form-control"
                     id="keepImg"
                     placeholder="Link your keep image..."
                     v-model="state.keepData.img"
                     required
              >
            </div>
            <button type="submit" class="btn">
              Submit
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
import { reactive } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
  setup() {
    const state = reactive({
      keepData: {}
    })
    return {
      state,
      async createKeep(event) {
        try {
          await keepsService.createKeep(AppState.account.id, state.keepData)
          event.target.reset()
          $('#newKeep').modal('hide')
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
