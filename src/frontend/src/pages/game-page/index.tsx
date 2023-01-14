import { gamesModel, isCurrentUserWalking, StateGame } from 'entities/game'
import Board from 'features/board'
import BoardToolbar from 'features/board-toolbar'
import React, { useEffect } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { Routes } from 'shared/paths'
import styled from 'styled-components'

const GamePage = () => {
    const navigate = useNavigate()

    const { state, mark: currentUserMark, board } = gamesModel.currentGame.useCurrentGameInfo()

    const getStatusGameMessage = (state: StateGame) => {
        switch (state) {
            case 'Draw':
                return 'Ничья'
            case 'CrossesWon':
                return currentUserMark === 'Crosses' ? 'Вы победили!' : 'Вы проиграли!'
            case 'NoughtsWon':
                return currentUserMark === 'Noughts' ? 'Вы победили!' : 'Вы проиграли!'
            case 'NotStarted':
                return 'Ожидайте соперника'
            default:
                return ''
        }
    }

    useEffect(() => {
        gamesModel.currentGame.effects.loadCurrentGameFx().then((resp) => {
            if (resp.isSuccessful) {
                gamesModel.connectToGame.effects.connectToGameFx()
                return
            }

            navigate(Routes.GAMES_LIST)
        })
        return () => {
            gamesModel.connectToGame.events.disconnect()
        }
    }, [])

    return (
        <Page>
            <BoardToolbar />
            <Board />
            <GameResult>{getStatusGameMessage(state)}</GameResult>
            <Step>
                {state === 'InProgress' &&
                    (isCurrentUserWalking(board, currentUserMark) ? 'Ваш ход' : 'Ход противника')}
            </Step>
            <Link to={Routes.GAMES_LIST}>Вернуться к списку игр</Link>
        </Page>
    )
}

const GameResult = styled.div`
    margin-top: 5px;
`

const Page = styled.div`
    width: 100%;
    align-items: center;
    justify-content: center;
    display: flex;
    flex-direction: column;
`

const Step = styled.div``

export default GamePage
