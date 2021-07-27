import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  activeKeeps: [],
  currenPage: null,
  activeProfile: {
    profile: {},
    keeps: [],
    vaults: []
  },
  activeVault: {
    vault: {},
    keeps: []
  },
  signedIn: false,
  gotVault: false,
  accountVaults: []
})
