import { Nullable } from 'shared/types'

export type BoardRow = [Nullable<Mark>, Nullable<Mark>, Nullable<Mark>]

export type Mark = 'Crosses' | 'Noughts'

export type StateGame = 'NotStarted' | 'InProgress' | 'NoughtsWon' | 'CrossesWon' | 'Draw'

export type Board = [BoardRow, BoardRow, BoardRow]

export interface CurrentGameInfo {
    board: Board
    mark: Mark
    opponentUsername: string | null
    state: StateGame
}
