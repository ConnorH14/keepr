import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultService {
  async createVault(id, vaultData) {
    await api.post('api/vaults', vaultData)
    const vaults = await api.get(`api/profiles/${id}/vaults`)
    AppState.activeProfile.vaults = vaults.data
    AppState.accountVaults = vaults.data
  }

  async getVaultById(id) {
    if (id) {
      const res = await api.get('api/vaults/' + id)
      AppState.activeVault.vault = res.data
      return true
    }
  }

  async getKeepsByVault(id) {
    if (id) {
      const res = await api.get(`api/vaults/${id}/keeps`)
      AppState.activeVault.keeps = res.data
    }
  }

  async deleteVault(id) {
    await api.delete('api/vaults/' + id)
    return true
  }
}

export const vaultService = new VaultService()
