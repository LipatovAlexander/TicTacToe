import * as signalR from '@microsoft/signalr'
import { notificator } from 'entities/notification'
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
        currentGame.events.setStateGame(state)
        currentGame.events.setMarkInBoard({ x, y, mark })
    })

    connection.on('StartGame', (opponentUsername: string) => {
        currentGame.events.setOpponentUsername(opponentUsername)
    })

    connection.on('OpponentDisconnected', (state: StateGame) => {
        currentGame.events.setStateGame(state)
        notificator.info('Противник отключился. Автоматическая победа')
    })

    await connection.start()

    return connection
}

export default connectToGameAndConfigure
