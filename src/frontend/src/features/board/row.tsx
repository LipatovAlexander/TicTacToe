import { BoardRow } from 'entities/game'
import React from 'react'
import styled from 'styled-components'
import Cell from './cell'

interface RowProps {
    row: BoardRow
    number: number
}

const Row = ({ row, number }: RowProps) => {
    return (
        <RowBlock>
            {row.map((m, i) => (
                <Cell mark={m} x={number} y={i} />
            ))}
        </RowBlock>
    )
}

const RowBlock = styled.div`
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-column-gap: 10px;
`

export default Row
