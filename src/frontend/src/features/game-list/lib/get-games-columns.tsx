import { ColumnType } from 'antd/es/table'
import React from 'react'
import { Game } from 'entities/game'
import JoinToGameButton from '../join-to-game-button'

export const getGamesColumns = (): ColumnType<Game>[] => [
    {
        title: 'Имя соперника',
        dataIndex: 'hostUsername',
        key: 'hostUsername',
        width: '50%',
    },
    {
        title: 'Дата создания',
        dataIndex: 'createdAt',
        key: 'createdAt',
        render: (value: Date) => value.toLocaleString(),
        width: '30%',
    },
    {
        key: 'join',
        render: (_, record) => <JoinToGameButton gameId={record.id} />,
    },
]
