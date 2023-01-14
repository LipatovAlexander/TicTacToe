import { Table } from 'antd'
import { Game, gamesModel } from 'entities/game'
import React, { useEffect } from 'react'
import styled from 'styled-components'
import CreateGameButton from './create-game-button'
import { getGamesColumns } from './lib/get-games-columns'

const GamesList = () => {
    const games = gamesModel.list.useGames()

    useEffect(() => {
        gamesModel.list.effects.loadGamesFx()
    }, [])

    const columns = getGamesColumns()

    return (
        <GamesBlock>
            <Toolbar>
                <TableTitle>Список игр</TableTitle>
                <CreateGameButton />
            </Toolbar>
            <GamesTable columns={columns} dataSource={games} pagination={false} />
        </GamesBlock>
    )
}

const Toolbar = styled.div`
    display: flex;
    justify-content: space-between;
    align-items: center;
`

const GamesBlock = styled.div`
    display: flex;
    flex-direction: column;
`

const GamesTable = styled(Table<Game>)``

const TableTitle = styled.h2``

export default GamesList
