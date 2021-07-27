import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    AppState.currentPage = 0
    const res = await api.get('/api/keeps')
    AppState.keeps = res.data
    AppState.keeps = AppState.keeps.reverse()
    if (AppState.activeKeeps <= 50) {
      for (let i = 0; i < 50; i++) {
        AppState.activeKeeps.push(AppState.keeps[i])
      }
    }
    AppState.currenPage = AppState.activeKeeps.length
  }

  loadKeeps() {
    const newLoad = AppState.activeKeeps.length + 50
    for (let i = AppState.activeKeeps.length; i < newLoad; i++) {
      if (AppState.keeps[i]) {
        AppState.activeKeeps.push(AppState.keeps[i])
      }
    }
  }

  async selectKeep(id) {
    await api.get('api/keeps/' + id)
  }

  async createKeep(id, keepData) {
    await api.post('api/keeps', keepData)
    const keeps = await api.get(`api/profiles/${id}/keeps`)
    AppState.activeProfile.keeps = keeps.data
  }

  async deleteKeep(id) {
    await api.delete('api/keeps/' + id)
    AppState.activeKeeps = AppState.activeKeeps.filter(k => k.id !== id)
    AppState.activeProfile.keeps = AppState.activeProfile.keeps.filter(k => k.id !== id)
    AppState.activeVault.keeps = AppState.activeVault.keeps.filter(k => k.id !== id)
  }

  async addKeepToVault(kid, vid) {
    const vaultKeep = { keepId: kid, vaultId: vid }
    await api.post('api/vaultkeeps', vaultKeep)
  }
}

export const keepsService = new KeepsService()
