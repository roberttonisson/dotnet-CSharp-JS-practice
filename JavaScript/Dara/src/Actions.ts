import { Game, } from './GameLogic';
import { TileState } from './TileState';
import * as UI from './UI';

export function phaseOne(game: Game, x: number, y: number, phaseText: Element) {
    if (game.isValidMovePhaseOne(x, y)) {
        game.MovePhaseOne(x, y);
    } else {
        console.log("Invalid tile!")
    }
}

export function phaseTwo(game: Game, x: number, y: number, phaseText: Element) {
    if (typeof game.Selected === 'undefined') {
        if (game.RemovingAPiece) {
            let piece = TileState.O;
            if (game.PlayerOneTurn) {
                piece = TileState.X;
            }
            if (game.Board[x][y] === piece) {
                game.RemovePiece(x, y);
            }
        } else {
            phaseTwoSelect(game, x, y);
        }
    } else {
        if (game.isValidMovePhaseTwo(x, y)) {
            game.MovePhaseTwo(x, y);
        }
        else {
            UI.SelectedPieceColor(x, y, false);
            game.Selected = undefined;
        }
    }
}

function phaseTwoSelect(game: Game, x: number, y: number) {
    if (game.isValidPiecePhaseTwo(x, y)) {
        game.Selected = [x, y];
        UI.SelectedPieceColor(x, y, true)
    } else {
        console.log("Invalid tile!")
    }
}


export function hoverAction(game: Game, out: boolean, x: number, y: number) {
    if (game.GamePhase === 1) {
        if (game.isValidMovePhaseOne(x, y)) {
            UI.changeBackground(x,y,out);
        }
    }
    if (game.GamePhase === 2) {
        if (game.RemovingAPiece) {
            let piece = TileState.O;
            if (game.PlayerOneTurn) {
                piece = TileState.X;
            }
            if (game.Board[x][y] === piece) {
                UI.changeBackground(x,y,out);
            }
        }
        else {
            if (game.isValidPiecePhaseTwo(x,y) && (!(x === game.Selected?.[0]) && !(x === game.Selected?.[0]))) {
                UI.changeBackground(x,y,out);
            }
            if (!(typeof game.Selected === 'undefined') && game.isValidMovePhaseTwo(x, y)) {
                UI.changeBackground(x,y,out);
            }
        }
    }
}
