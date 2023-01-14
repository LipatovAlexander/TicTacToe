import { Button } from 'antd'
import { gamesModel } from 'entities/game'
import React from 'react'
import { useNavigate } from 'react-router-dom'
import { Routes } from 'shared/paths'
import styled from 'styled-components'

interface CreateGameButtonProps {
    className?: string
}

const CreateGameButton = ({ className }: CreateGameButtonProps) => {
    const navigate = useNavigate()

    const onClick = async () => {
        const resp = await gamesModel.list.effects.createGameFx()

        if (resp.isSuccessful) {
            navigate(Routes.GAME)
        }
    }

    return (
        <StyledButton className={className} onClick={onClick}>
            Создать игру
        </StyledButton>
    )
}

const StyledButton = styled(Button)`
    width: 10%;
`

export default CreateGameButton
