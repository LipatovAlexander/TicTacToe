import { createEffect, createStore, forward } from 'effector'
import { useStore } from 'effector-react'
import jwtDecode from 'jwt-decode'
import { getToken, setToken } from 'shared/local-storage'
import { login, register } from '../api'
import { User, UserForm } from '../types'

const $user = createStore<User>({ isAuthenticated: false, username: '' })

const loginFx = createEffect(async (data: UserForm) => {
    const resp = await login(data)
    if (!resp.data.isSuccessful) {
        return false
    }

    const jwt = resp.data.data.jwt
    setToken(jwt)

    return true
})

const registerFx = createEffect(async (data: UserForm) => {
    const resp = await register(data)

    return {
        isSuccessful: resp.data.isSuccessful,
        errors: resp.data.errors,
    }
})

const loadUserFx = createEffect(() => {
    const token = getToken()
    const userInfo = token ? jwtDecode<{ unique_name: string }>(token) : null

    return {
        isAuthenticated: !!userInfo,
        username: userInfo?.unique_name ?? '',
    }
})

forward({
    from: loginFx.doneData,
    to: loadUserFx,
})

forward({
    from: loadUserFx.doneData,
    to: $user,
})

export const useUser = () => useStore($user)
export const useAuthenticating = () => useStore(loginFx.pending)
export const useRegistering = () => useStore(registerFx.pending)

export const effects = {
    loginFx,
    loadUserFx,
    registerFx,
}
