import { UserForm, userModel } from 'entities/user'
import { LoginForm } from 'features/login'
import React, { useState } from 'react'
import { Link } from 'react-router-dom'
import { Navigate } from 'react-router-dom'
import { Routes } from 'shared/paths'
import styled from 'styled-components'

const Login = () => {
    const { isAuthenticated } = userModel.useUser()
    const authenticating = userModel.useAuthenticating()

    const [errorMessages, setErrorMessages] = useState<string[]>([])

    const onSubmitForm = async (inputData: UserForm) => {
        const isLogin = await userModel.effects.loginFx(inputData)

        if (!isLogin) {
            setErrorMessages(['Incorrect login or password'])
        }
    }

    if (isAuthenticated) {
        return <Navigate to={Routes.GAMES_LIST} replace={true} />
    }

    return (
        <div>
            <LoginForm
                onSubmit={onSubmitForm}
                title={'Вход'}
                errorMessages={errorMessages}
                submitBittonDisabled={authenticating}
                buttonName={'Войти'}
                linkToPage={<LinkToPage to={Routes.REGISTRATION}>Еще нет аккаунта?</LinkToPage>}
            />
        </div>
    )
}

const LinkToPage = styled(Link)`
    font-size: 12px;
    color: blue;
`

export default Login
