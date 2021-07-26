import { AppState } from '../AppState'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('/api/keeps')
    AppState.keeps = res.data
    AppState.keeps.reverse()
    for (let i = 0; i < 50; i++) {
      AppState.activeKeeps.push(AppState.keeps[i])
    }
  }
}

export const keepsService = new KeepsService()
