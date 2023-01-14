import { createEffect, createEvent, createStore, sample } from 'effector'
import { useStore } from 'effector-react'
import { notificator } from 'entities/notification'
import { getCurrentGameInfo } from '../api/get-current-game-info'
import { CurrentGameInfo, Mark, StateGame } from '../types/current-game-info'

const $currentGameInfo = createStore<CurrentGameInfo>({
    board: [
        [null, null, null],
        [null, null, null],
        [null, null, null],
    ],
    mark: 'Crosses',
    opponentUsername: '',
    state: 'CrossesWon',
})

const loadCurrentGameFx = createEffect(async () => {
    return (await getCurrentGameInfo()).data
})

const setMarkInBoard = createEvent<{ x: number; y: number; mark: Mark }>()

sample({
    clock: setMarkInBoard,
    source: $currentGameInfo,
    fn: (game, newMark) => {
        const newInfo = { ...game }
        newInfo.board[newMark.x][newMark.y] = newMark.mark

        return newInfo
    },
    target: $currentGameInfo,
})

const setStateGame = createEvent<StateGame>()

sample({
    clock: setStateGame,
    source: $currentGameInfo,
    filter: (gameInfo, newState) => gameInfo.state != newState,
    fn: (game, newState) => {
        const newInfo = { ...game, state: newState }
        return newInfo
    },
    target: $currentGameInfo,
})

const setOpponentUsername = createEvent<string>()

sample({
    clock: setOpponentUsername,
    source: $currentGameInfo,
    fn: (game, newOpponent) => {
        const newInfo: CurrentGameInfo = { ...game, opponentUsername: newOpponent, state: 'InProgress' }
        return newInfo
    },
    target: $currentGameInfo,
})

sample({
    clock: setOpponentUsername,
    fn: () => 'Подключился игрок',
    target: notificator.success,
})

sample({
    clock: setStateGame,
    source: $currentGameInfo,
    filter: (gameInfo, newState) => gameInfo.state != newState,
    fn: (game, newState) => {
        const newInfo = { ...game, state: newState }
        return newInfo
    },
    target: $currentGameInfo,
})

sample({
    clock: loadCurrentGameFx.doneData,
    filter: (data) => !data.isSuccessful,
    fn: (data) => data.errors.at(0) ?? 'Неизвестная ошибка при получении информации о текущей игре',
    target: notificator.error,
})

sample({
    clock: loadCurrentGameFx.doneData,
    filter: (data) => data.isSuccessful,
    fn: (data) => data.data,
    target: $currentGameInfo,
})

export const useCurrentGameInfo = () => useStore($currentGameInfo)

export const effects = {
    loadCurrentGameFx,
}

export const events = {
    setMarkInBoard,
    setStateGame,
    setOpponentUsername,
}
