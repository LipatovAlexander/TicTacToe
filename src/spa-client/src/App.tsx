import * as React from "react";
import { Routes, Route } from "react-router-dom";
import { Game, NoMatch } from "./pages";

export default function App() {
  return (
        <Routes>
            <Route path="/" element={<Game />} />

            <Route path="*" element={<NoMatch />} />
        </Routes>
  );
}