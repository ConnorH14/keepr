import { AppState } from '../AppState'
import { api } from './AxiosService'

class ProfileService {
  async getProfile(id) {
    const profile = await api.get('api/profiles/' + id)
    const keeps = await api.get(`api/profiles/${id}/keeps`)
    const vaults = await api.get(`api/profiles/${id}/vaults`)
    AppState.activeProfile.profile = profile.data
    AppState.activeProfile.keeps = keeps.data
    AppState.activeProfile.vaults = vaults.data
  }

  async getProfileVaults(id) {
    const vaults = await api.get(`api/profiles/${id}/vaults`)
    AppState.accountVaults = vaults.data
    console.log(AppState.accountVaults)
  }
}

export const profileService = new ProfileService()
