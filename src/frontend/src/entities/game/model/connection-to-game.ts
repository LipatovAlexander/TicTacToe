import { createEffect, createEvent, createStore, forward, sample } from 'effector'
import connectToGameAndConfigure from '../api/connect-to-game-and-configure'

const $connectionToGame = createStore<signalR.HubConnection | null>(null)

const connectToGameFx = createEffect(async () => {
    return await connectToGameAndConfigure()
})

const disconnectFx = createEffect(async (con: signalR.HubConnection) => {
    return await con.stop()
})

const move = createEvent<{ x: number; y: number }>()

const moveFx = createEffect(
    async ({ connection, data }: { connection: signalR.HubConnection; data: { x: number; y: number } }) => {
        await connection.invoke('Move', data.x, data.y)
    },
)

const disconnect = createEvent()

sample({
    clock: disconnect,
    source: $connectionToGame,
    filter: (connection) => !!connection,
    fn: (connection) => connection!,
    target: disconnectFx,
})

sample({
    clock: move,
    source: $connectionToGame,
    filter: (connection) => !!connection,
    fn: (connection, data) => ({
        connection: connection!,
        data,
    }),
    target: moveFx,
})

forward({
    from: connectToGameFx.doneData,
    to: $connectionToGame,
})

forward({
    from: connectToGameFx.doneData,
    to: $connectionToGame,
})

export const effects = {
    connectToGameFx,
}

export const events = {
    move,
    disconnect,
}
