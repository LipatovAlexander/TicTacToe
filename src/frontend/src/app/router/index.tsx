import PageForAuthorizedUser from 'app/page'
import { EmptyPage, GamesPage, Login, Registration } from 'pages'
import GamePage from 'pages/game-page'
import React from 'react'
import { Route, Routes } from 'react-router-dom'
import { Routes as Paths } from 'shared/paths'

const AppRoutes = () => {
    return (
        <Routes>
            <Route path={Paths.REGISTRATION} element={<Registration />} />
            <Route path={Paths.LOGIN} element={<Login />} />
            <Route path="*" element={<EmptyPage />} />
            <Route element={<PageForAuthorizedUser />}>
                <Route path={Paths.GAMES_LIST} element={<GamesPage />} />
                <Route path={Paths.GAME} element={<GamePage />} />
            </Route>
        </Routes>
    )
}

export default AppRoutes
