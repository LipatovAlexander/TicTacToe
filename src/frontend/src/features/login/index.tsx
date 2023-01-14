import { Button, Form, Input } from 'antd'
import { UserForm } from 'entities/user'
import React from 'react'
import styled from 'styled-components'

interface LoginFormProps {
    onSubmit: (form: UserForm) => void
    errorMessages: string[]
    submitBittonDisabled: boolean
    title: string
    buttonName: string
    linkToPage: React.ReactNode
}

export const LoginForm = ({
    onSubmit,
    errorMessages,
    submitBittonDisabled,
    title,
    buttonName,
    linkToPage,
}: LoginFormProps) => {
    const onFinish = (form: UserForm) => {
        onSubmit(form)
    }

    return (
        <Container>
            <FormBock>
                <Header>{title}</Header>
                <StyledForm onFinish={onFinish}>
                    <Form.Item name="username" key="username" id="username" label="Логин">
                        <Input />
                    </Form.Item>
                    <Form.Item name="password" key="password" id="password" label="Пароль">
                        <Input />
                    </Form.Item>
                    <Errors>
                        {errorMessages.map((message, i) => (
                            <Error key={i}>{message}</Error>
                        ))}
                    </Errors>
                    <SubmitButton disabled={submitBittonDisabled} htmlType="submit">
                        {buttonName}
                    </SubmitButton>
                </StyledForm>
            </FormBock>
            <LinkUnderForm>{linkToPage}</LinkUnderForm>
        </Container>
    )
}

const LinkUnderForm = styled.div`
    margin-top: 15px;
`

const StyledForm = styled(Form<UserForm>)`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`

const FormBock = styled.div`
    display: flex;
    flex-direction: column;
    align-items: center;
`

const SubmitButton = styled(Button)`
    width: 100%;
    margin-top: 10px;
`

const Header = styled.div`
    font-size: 1.7em;
    font-weight: 500;
    margin: 20px 0;
`

const Container = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 100vh;
`

const Errors = styled.div`
    display: flex;
    margin-top: -20px;
    flex-direction: column;
`

const Error = styled.div`
    font-size: 12px;
    color: red;
    text-align: center;
`
