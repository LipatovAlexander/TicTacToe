import { gamesModel } from 'entities/game'
import React from 'react'
import styled from 'styled-components'
import Row from './row'

const Board = () => {
    const boardInfo = gamesModel.currentGame.useCurrentGameInfo().board

    return (
        <BoardBlock>
            {boardInfo?.map((r, i) => (
                <Row row={r} number={i} />
            ))}
        </BoardBlock>
    )
}

const BoardBlock = styled.div`
    display: flex;
    flex-direction: column;
    grid-row-gap: 10px;
    width: 240px;
`

export default Board
