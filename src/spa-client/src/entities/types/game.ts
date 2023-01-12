import {BaseEntity} from "./base-entity";
import {Player} from "./player";
import {GameState} from "./game-state";
import {Board} from "./board";

export interface Game extends BaseEntity {
    noughtsPlayer: Player
    crossesPlayer: Player
    state: GameState
    board: Board
}