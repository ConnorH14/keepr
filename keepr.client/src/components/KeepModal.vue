<template>
  <div class="modal fade"
       :id="'m' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="keepModal"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-lg" role="document">
      <div class="modal-content keep-modal">
        <div class="modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6">
                <small class="modal-exit-mobile" title="exit" data-dismiss="modal"><i class="mdi mdi-exit-to-app"></i></small>
                <img :src="keep.img" :alt="keep.name" class="img-fluid rounded">
              </div>
              <div class="col-md-6">
                <small class="modal-exit-desk" title="exit" data-dismiss="modal"><i class="mdi mdi-exit-to-app"></i></small>
                <span @click="goToAccount(keep.creator.id)" role="button" data-dismiss="modal">
                  <img :src="keep.creator.picture" alt="profile picture" class="img-fluid rounded-circle" width="50">
                  {{ keep.creator.name }}
                </span>
                <h1 class="text-center mt-4">
                  {{ keep.name }}
                </h1>
                <hr>
                <p>{{ keep.description }}</p>
                <hr>
                <div class="text-center">
                  <span class="modal-icon" title="views"><i class="mdi mdi-eye"></i> {{ keep.views }}</span>
                  <span class="modal-icon" title="keeps"><i class="mdi mdi-bank"></i> {{ keep.keeps }}</span>
                  <span class="modal-icon" title="shares"><i class="mdi mdi-share-variant-outline"></i> {{ keep.shares }}</span>
                </div>
                <div class="modal-bottom mt-5 text-center">
                  <div class="dropdown">
                    <button class="btn dropdown-toggle"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Add to vault.
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <a v-for="vault in accountVaults" :key="vault.id" class="dropdown-item" @click="addKeeptoVault(keep.id, vault.id)">{{ vault.name }}</a>
                    </div>
                  </div>
                  <br>
                  <div class="text-right mt-3">
                  </div>
                  <span v-if="account.id == keep.creator.id" class="mdi mdi-delete-circle delete-icon" title="delete" @click="deleteKeep(keep.id)"></span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import $ from 'jquery'
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    const router = useRouter()
    return {
      accountVaults: computed(() => AppState.accountVaults),
      account: computed(() => AppState.account),
      goToAccount(accountId) {
        router.push({ name: 'Account', params: { id: accountId } })
      },
      async deleteKeep(id) {
        try {
          if (confirm('Are you sure you want to delete this keep?')) {
            $('#m' + id).modal('hide')
            await keepsService.deleteKeep(id)
          }
        } catch (error) {
          console.log(error)
        }
      },
      async addKeeptoVault(kid, vid) {
        try {
          await keepsService.addKeepToVault(kid, vid)
          $('#m' + kid).modal('hide')
        } catch (error) {
          console.log(error)
        }
      }
    }
  }
}
</script>

<style>
.keep-modal {
  position: relative;
}
.modal-exit-desk {
  position: absolute;
  top: 5px;
  right: 5px;
  font-size: 2em;
}
.modal-exit-desk:hover {
  color: #d42929;
  cursor: pointer;
}
.modal-exit-mobile {
  top: 5px;
  right: 5px;
  display: none;
  text-align: right;
  font-size: 1em;
}
.modal-icon{
  padding: 5%;
  font-size: 1.5em;
}
.delete-icon{
  font-size: 2em;
}
.delete-icon:hover{
  color: #d42929;
  cursor: pointer;
}
.modal-bottom>span{
  padding-right: 10px;
}
@media only screen and (max-width: 992px){
    .modal-exit-desk {
      display: none;
    }
    .modal-exit-mobile {
      display: inherit;
    }
}
</style>
