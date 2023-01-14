import { Board, Mark } from '../types'

const getMarksAmount = (markType: Mark, board: Board) =>
    board.reduce((acc, value) => acc + value.reduce((acc, value) => acc + (value === markType ? 1 : 0), 0), 0)

export const isCurrentUserWalking = (board: Board, markCurrentUser: Mark) => {
    const crossesAmount = getMarksAmount('Crosses', board)
    const noughtsAmount = getMarksAmount('Noughts', board)

    return (
        (crossesAmount === noughtsAmount && markCurrentUser === 'Crosses') ||
        (crossesAmount !== noughtsAmount && markCurrentUser === 'Noughts')
    )
}
