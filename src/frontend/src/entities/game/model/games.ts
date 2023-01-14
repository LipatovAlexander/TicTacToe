import { createEffect, createStore, sample } from 'effector'
import { useStore } from 'effector-react'
import { notificator } from 'entities/notification'
import { createGame } from '../api/create-game'
import { getGames } from '../api/get-games'
import { joinToGame } from '../api/join-to-game'
import { Game } from '../types'

const $games = createStore<Game[]>([])

const loadGamesFx = createEffect(async () => {
    const resp = await getGames()

    return resp.data
})

const joinToGameFx = createEffect(async (gameId: string) => {
    const resp = await joinToGame(gameId)

    return resp.data
})

const createGameFx = createEffect(async () => {
    const resp = await createGame()

    return resp.data
})

sample({
    clock: loadGamesFx.doneData,
    filter: (data) => data.isSuccessful,
    fn: (data) => data.data.map((g) => ({ ...g, createdAt: new Date(g.createdAt) })),
    target: $games,
})

sample({
    clock: createGameFx.doneData,
    filter: (data) => data.isSuccessful,
    fn: () => 'Игра успешно создана',
    target: notificator.success,
})

sample({
    clock: createGameFx.doneData,
    filter: (data) => !data.isSuccessful,
    fn: (data) => data.errors?.at(0) ?? 'Неизвестная ошибка при создании игры',
    target: notificator.error,
})

export const useGames = () => useStore($games)

export const effects = {
    loadGamesFx,
    joinToGameFx,
    createGameFx,
}
