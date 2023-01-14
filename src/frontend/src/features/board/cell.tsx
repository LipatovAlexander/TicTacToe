import { gamesModel, Mark } from 'entities/game'
import React from 'react'
import styled from 'styled-components'
import { CloseOutlined } from '@ant-design/icons'
import { Nullable } from 'shared/types'

interface CellProps {
    mark: Nullable<Mark>
    x: number
    y: number
}

const Cell = ({ mark, x, y }: CellProps) => {
    const { mark: markCurrentUser, board, state } = gamesModel.currentGame.useCurrentGameInfo()

    const getMarksAmount = (markType: Mark) =>
        board.reduce((acc, value) => acc + value.reduce((acc, value) => acc + (value === markType ? 1 : 0), 0), 0)

    const isCurrentUserWalking = () => {
        const crossesAmount = getMarksAmount('Crosses')
        const noughtsAmount = getMarksAmount('Noughts')

        console.log('crossesAmount', crossesAmount)
        console.log('noughtsAmount', noughtsAmount)
        console.log('markCurrentUser', markCurrentUser)

        return (
            (crossesAmount === noughtsAmount && markCurrentUser === 'Crosses') ||
            (crossesAmount !== noughtsAmount && markCurrentUser === 'Noughts')
        )
    }

    const onClick = () => {
        console.log(state)

        if (state === 'InProgress' && isCurrentUserWalking()) {
            gamesModel.connectToGame.events.move({ x: x, y: y })
        }
    }

    return <CellBlock onClick={onClick}>{mark && (mark == 'Crosses' ? <Crosses /> : <Noughts />)}</CellBlock>
}

const CellBlock = styled.div`
    display: flex;
    width: 70px;
    justify-content: center;
    align-items: center;
    height: 70px;
    border: 1px solid black;
    border-radius: 10px;
    cursor: pointer;
`

const Crosses = styled(CloseOutlined)`
    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    font-size: 50px;
    color: red;
`

const Noughts = styled.div`
    justify-content: center;
    align-items: center;
    border-radius: 50%;
    width: 60%;
    height: 60%;
    border: 5px solid blue;
`

export default Cell
