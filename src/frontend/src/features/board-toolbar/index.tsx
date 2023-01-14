import { CloseOutlined } from '@ant-design/icons'
import { gamesModel } from 'entities/game'
import { userModel } from 'entities/user'
import React from 'react'
import styled from 'styled-components'

const BoardToolbar = () => {
    const gameInfo = gamesModel.currentGame.useCurrentGameInfo()
    const user = userModel.useUser()

    return (
        <BoardToolbarBlock>
            <PlayersInfo>
                <Player>
                    <PlayerName>{user.username}:</PlayerName>
                    {gameInfo.mark && (gameInfo.mark == 'Crosses' ? <Crosses /> : <Noughts />)}
                </Player>
                <Player>
                    <PlayerName>{gameInfo.opponentUsername}:</PlayerName>
                    {gameInfo.mark && (gameInfo.mark != 'Crosses' ? <Crosses /> : <Noughts />)}
                </Player>
            </PlayersInfo>
        </BoardToolbarBlock>
    )
}

const PlayersInfo = styled.div`
    display: flex;
    flex-direction: column;
`

const BoardToolbarBlock = styled.div`
    display: flex;
    justify-content: center;
    align-items: center;
    justify-content: space-between;
    margin-bottom: 10px;
`

const PlayerName = styled.span`
    font-size: 20px;
`

const Player = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
    justify-content: center;
    width: 100px;
`

const Crosses = styled(CloseOutlined)`
    width: 100%;
    height: 100%;
    width: 30px;
    color: red;
`

const Noughts = styled.div`
    border-radius: 50%;
    width: 15px;
    height: 15px;
    border: 1px solid blue;
`

export default BoardToolbar
