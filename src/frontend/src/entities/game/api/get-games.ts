import { api, ApiResponse } from 'shared/api'
import { Game } from '../types'

type GameResponse = Omit<Game, 'createdAt'> & {
    createdAt: string
}

export const getGames = async () => {
    return await api.get<ApiResponse<GameResponse[]>>('games')
}
