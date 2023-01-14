import { UserForm, userModel } from 'entities/user'
import { LoginForm } from 'features/login'
import React, { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { Navigate } from 'react-router-dom'
import { Routes } from 'shared/paths'
import styled from 'styled-components'

const Registration = () => {
    const navigate = useNavigate()

    const { isAuthenticated } = userModel.useUser()
    const registering = userModel.useRegistering()

    const [errorMessages, setErrorMessages] = useState<string[]>([])

    const onSubmitForm = async (inputData: UserForm) => {
        const registerResult = await userModel.effects.registerFx(inputData)

        if (registerResult.isSuccessful) {
            navigate(Routes.LOGIN)
        }

        setErrorMessages(registerResult.errors)
    }

    if (isAuthenticated) {
        return <Navigate to={Routes.GAMES_LIST} />
    }

    return (
        <div>
            <LoginForm
                onSubmit={onSubmitForm}
                title={'Регистрация'}
                errorMessages={errorMessages}
                submitBittonDisabled={registering}
                buttonName={'Зарегистрироваться'}
                linkToPage={<LinkToPage to={Routes.LOGIN}>Уже есть аккаунт?</LinkToPage>}
            />
        </div>
    )
}

const LinkToPage = styled(Link)`
    font-size: 12px;
    color: blue;
`

export default Registration
