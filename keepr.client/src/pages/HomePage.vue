<template>
  <div class="container-fluid background-img">
    <div class="profile-icon">
    </div>
    <div class="row mt-3 d-flex justify-content-around">
      <div class="col-12 text-center mt-5">
        <h1 class="title-text title-header">
          Welcome to [keeps && vaults].
        </h1>
      </div>
    </div>
    <div class="row mt-5">
      <div class="col-2"></div>
      <div class="col-lg-3 text-center">
        <div class="title-text-div d-flex align-items-center justify-content-center" @click="browseClick">
          <h2 class="title-text">
            Browse.
          </h2>
        </div>
      </div>
      <div class="col-lg-2 text-center">
        <div class="title-text-center d-flex justify-content-center align-items-center">
          <div class="title-divider mt-5 shadow-lg"></div>
        </div>
      </div>
      <div class="col-lg-3 text-center">
        <div class="title-text-div d-flex align-items-center justify-content-center" @click="createClick">
          <h2 class="title-text">
            Create.
          </h2>
        </div>
      </div>
      <div class="col-2"></div>
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { useRouter } from 'vue-router'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    const router = useRouter()
    return {
      account: computed(() => AppState.account),
      browseClick() {
        router.push({ name: 'Keeps' })
      },
      async createClick() {
        if (AppState.account.id) {
          router.push({ name: 'Account', params: { id: AppState.account.id } })
        } else {
          AuthService.loginWithRedirect()
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.profile-icon {
  position: absolute;
  top: 20px;
  right: 30px;
  transition: 1s;
}

.title-text-div {
  height: 50vh;
  transition: 0.5s;
}

.title-text-div:hover {
  cursor: pointer;
  user-select: none;
  transform: rotate(3deg);
  text-shadow: 1px 1px #969393;
}

.title-text {
  font-family: 'Secular One', sans-serif;
  animation: title-text-show 4s ease-out;
  font-size: 3em;
}

.title-divider {
  width: 1px;
  height: 35vh;
  background-color: #8d8d97;
  animation: divider-show 2s ease-out;
}

.title-header {
  animation: title-slide 2s ease-out;
}

@keyframes title-slide{
  0%{
    transform: translateX(-2000px);
  }
  100%{
    transform: translateX(0);
  }
}

@keyframes divider-show{
  0%{
    opacity: 0;
  }
  50%{
    opacity: 0;
  }
  100%{
    opacity: 100;
  }
}

@keyframes title-text-show{
  0%{
    opacity: 0;
  }
  50%{
    opacity: 0;
  }
  100%{
    opacity: 100;
  }
}

@media only screen and (max-width: 992px){
    .title-divider {
      display: none;
    }
    .title-text-div {
      height: 20vh;
    }
}

</style>
