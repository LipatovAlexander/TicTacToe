import { api, ApiResponse } from 'shared/api'

interface Jwt {
    jwt: string
}

interface AuthRequest {
    username: string
    password: string
}

export const login = async (data: AuthRequest) => {
    return await api.post<ApiResponse<Jwt>>('/login', data)
}

export const register = async (data: AuthRequest) => {
    return await api.post<ApiResponse>('/register', data)
}
