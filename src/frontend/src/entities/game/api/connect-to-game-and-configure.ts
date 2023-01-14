import * as signalR from '@microsoft/signalr'
import { getToken } from 'shared/local-storage'
import { currentGame } from '../model'
import { Mark, StateGame } from '../types'

const connectToGameAndConfigure = async () => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_API_URL}/api/game`, {
            accessTokenFactory: () => getToken() ?? '',
        })
        .build()

    connection.on('Move', (x: number, y: number, mark: Mark, state: StateGame) => {
        console.log('Произошел ход', state)

        currentGame.events.setStateGame(state)
        currentGame.events.setMarkInBoard({ x, y, mark })
    })

    connection.on('StartGame', (opponentUsername: string) => {
        console.log('Началась игра')
        currentGame.events.setOpponentUsername(opponentUsername)
    })

    connection.on('OpponentDisconnected', (state: StateGame) => {
        console.log('Чел, ливнул')
        currentGame.events.setStateGame(state)
    })

    await connection.start()

    return connection
}

export default connectToGameAndConfigure
