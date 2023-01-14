import { gamesModel, StateGame } from 'entities/game'
import Board from 'features/board'
import BoardToolbar from 'features/board-toolbar'
import React, { useEffect } from 'react'
import styled from 'styled-components'

const GamePage = () => {
    const { state, mark: currentUserMark } = gamesModel.currentGame.useCurrentGameInfo()

    const getResultMessage = (state: StateGame) => {
        switch (state) {
            case 'Draw':
                return 'Ничья'
            case 'CrossesWon':
                return currentUserMark === 'Crosses' ? 'Вы победили' : 'Вы проиграли'
            case 'NoughtsWon':
                return currentUserMark === 'Noughts' ? 'Вы победили' : 'Вы проиграли'
            default:
                return ''
        }
    }

    useEffect(() => {
        gamesModel.currentGame.effects.loadCurrentGameFx().then((resp) => {
            console.log(resp.data)

            if (resp.isSuccessful) {
                gamesModel.connectToGame.effects.connectToGameFx()
            }
        })
        return () => {
            gamesModel.connectToGame.events.disconnect()
        }
    }, [])

    return (
        <Page>
            <BoardToolbar />
            <Board />
            <GameResult>{getResultMessage(state)}</GameResult>
        </Page>
    )
}

const GameResult = styled.div`
    margin-top: 5px;
`

const Page = styled.div`
    margin: auto;
    display: flex;
    flex-direction: column;
`

export default GamePage
