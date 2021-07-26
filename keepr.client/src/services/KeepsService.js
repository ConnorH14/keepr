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
}

export const keepsService = new KeepsService()
