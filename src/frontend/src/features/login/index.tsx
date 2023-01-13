import { Button, Form, Input } from 'antd'
import { UserForm } from 'entities/user'
import React from 'react'
import styled from 'styled-components'

interface LoginFormProps {
    onSubmit: (form: UserForm) => void
    submitBittonDisabled: boolean
    title: string
}

export const LoginForm = ({ onSubmit, submitBittonDisabled, title }: LoginFormProps) => {
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
                    <SubmitButton disabled={submitBittonDisabled} htmlType="submit">
                        Войти
                    </SubmitButton>
                </StyledForm>
            </FormBock>
        </Container>
    )
}

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
    width: 50%;
    margin: -10px 0;
`

const Header = styled.div`
    font-size: 1.7em;
    font-weight: 500;
    margin: 20px 0;
`

const Container = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
`
