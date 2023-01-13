import {BaseEntity} from "./base-entity";
import {User} from "./user";
import {PlayerMark} from "./player-mark";

export interface Player extends BaseEntity {
    user: User
    mark: PlayerMark
}