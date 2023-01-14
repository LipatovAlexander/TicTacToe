import { api, ApiResponse } from 'shared/api'
import { CurrentGameInfo } from '../types/current-game-info'

export const getCurrentGameInfo = async () => {
    return api.get<ApiResponse<CurrentGameInfo>>('currentGame')
}
