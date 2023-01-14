import { Button } from 'antd'
import { gamesModel } from 'entities/game'
import { notificator } from 'entities/notification'
import React from 'react'
import { useNavigate } from 'react-router-dom'
import { Routes } from 'shared/paths'

interface JoinToGameButtonProps {
    gameId: string
}

const JoinToGameButton = ({ gameId }: JoinToGameButtonProps) => {
    const navigate = useNavigate()

    const onJoinGroup = async () => {
        const response = await gamesModel.list.effects.joinToGameFx(gameId)

        if (response.isSuccessful) {
            navigate(Routes.GAME)
            return
        }

        const firstError = response.errors.at(0)
        if (firstError) {
            notificator.error(firstError)
            return
        }

        notificator.error('Неизвестная ошибка при присоединении к игре')
    }

    return <Button onClick={onJoinGroup}>Присоединиться</Button>
}

export default JoinToGameButton
