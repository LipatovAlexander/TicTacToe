import { UserForm, userModel } from 'entities/user'
import { LoginForm } from 'features/login'
import React from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { Navigate } from 'react-router-dom'
import { Routes } from 'shared/paths'
import styled from 'styled-components'

const Login = () => {
    const nav = useNavigate()
    const { isAuthenticated } = userModel.useUser()
    const authenticating = userModel.useAuthenticating()

    if (isAuthenticated) {
        return <Navigate to={Routes.CHAT} />
    }

    const onSubmitForm = async (form: UserForm) => {
        const isLogin = await userModel.effects.loginFx(form)

        if (isLogin) {
            nav('')
        }
    }

    return (
        <div>
            <LoginForm onSubmit={onSubmitForm} title={'Вход'} submitBittonDisabled={authenticating} />
            <LinkToPage to={Routes.REGISTRY}>Еще нет аккаунта?</LinkToPage>
        </div>
    )
}

const LinkToPage = styled(Link)`
    font-size: 11px;
    color: blue;
`

export default Login
