import { api, ApiResponse } from 'shared/api'

export const createGame = async () => {
    return await api.post<ApiResponse<{ gameId: string }>>('create')
}
