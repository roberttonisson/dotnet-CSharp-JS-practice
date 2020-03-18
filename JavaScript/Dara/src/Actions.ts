import { Game, } from './GameLogic';
import { TileState } from './TileState';

export function phaseOne(game: Game, coord: string, handler: HTMLTableCellElement, phaseText: Element) {
    if (game.isValidMovePhaseOne(coord)) {
        let element = document.createElement("i");
        element.style.fontSize = "40px";
        element.classList.add("material-icons");
        if (game.PlayerOneTurn) {
            let node = document.createTextNode("brightness_high");
            element.appendChild(node);
            handler.appendChild(element);
            game.MovePhaseOne(coord);
        } else {
            let node = document.createTextNode("brightness_low");
            element.appendChild(node);
            handler.appendChild(element);
            game.MovePhaseOne(coord);
        }
        handler.style.backgroundColor = "#a1887f";
    } else {
        console.log("Invalid tile!")
    }
    if (game.isPhaseOneOver()) {
        phaseText.innerHTML = "Phase 2 : start moving."
        game.GamePhase = 2;
    }
}

export function phaseTwo(game: Game, coord: string, handler: HTMLTableCellElement, phaseText: Element) {
    if (typeof game.Selected === 'undefined') {
        if (game.RemovingAPiece) {
            let piece: TileState;
            if (game.PlayerOneTurn) {
                piece = TileState.X;
            }
            else {
                piece = TileState.O;
            }
            if (game.Board[Number(coord.charAt(0))][Number(coord.charAt(1))] === piece) {
                game.RemovePiece(coord, handler);
                if(game.PlayerOnePieces<3 || game.PlayerTwoPieces<3){
                    phaseText.innerHTML = "Game Over!"
                    game.GamePhase = 3;
                }
            }
        } else { phaseTwoSelect(game, coord, handler); }
    } else {
        if (game.isValidMovePhaseTwo(coord)) {
            game.MovePhaseTwo(coord, handler);
        }
        else {
            game.Selected.style.backgroundColor = "#a1887f";
            game.Selected = undefined;
        }

    }
}

function phaseTwoSelect(game: Game, coord: string, handler: HTMLTableCellElement) {
    if (game.isValidPiecePhaseTwo(coord)) {
        game.Selected = handler;
        handler.style.backgroundColor = "#ff8000"
    } else {
        console.log("Invalid tile!")
    }
}
function phaseTwoMove(game: Game, coord: string, handler: HTMLTableCellElement, phaseText: Element) {
    if (game.isValidMovePhaseOne(coord)) {
        let element = document.createElement("i");
        element.style.fontSize = "40px";
        element.classList.add("material-icons");
        if (game.PlayerOneTurn) {
            let node = document.createTextNode("brightness_high");
            element.appendChild(node);
            handler.appendChild(element);
            game.MovePhaseOne(coord);
        } else {
            let node = document.createTextNode("brightness_low");
            element.appendChild(node);
            handler.appendChild(element);
            game.MovePhaseOne(coord);
        }
    } else {
        console.log("Invalid tile!")
    }

}


export function hoverAction(handler: HTMLTableCellElement, game: Game, out: boolean) {
    let color1 = "#a1887f";
    let color2 = "#a1887f";
    if (out) {
        color2 = "#8d6e63"
    } else {
        color1 = "#8d6e63"
    }
    let coord = handler.dataset.coord!;
    if (game.GamePhase === 1) {
        if (game.isValidMovePhaseOne(coord)) {
            handler.style.backgroundColor = color1;
        }
    }
    if (game.GamePhase === 2) {
        if (game.RemovingAPiece) {
            let piece: TileState;
            if (game.PlayerOneTurn) {
                piece = TileState.X;
            }
            else {
                piece = TileState.O;
            }
            if (game.Board[Number(coord.charAt(0))][Number(coord.charAt(1))] === piece) {
                handler.style.backgroundColor = color1;
            }

        }
        else {
            if (game.isValidPiecePhaseTwo(coord) && !(handler === game.Selected)) {
                handler.style.backgroundColor = color1;
            }
            if (!(typeof game.Selected === 'undefined') && game.isValidMovePhaseTwo(coord)) {
                handler.style.backgroundColor = color1;
            }
        }

    }
}
