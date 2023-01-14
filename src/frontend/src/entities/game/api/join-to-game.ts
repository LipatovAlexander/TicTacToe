import { api, ApiResponse } from 'shared/api'

export const joinToGame = async (gameId: string) => {
    return await api.post<ApiResponse<null>>('join', { gameId: gameId })
}
