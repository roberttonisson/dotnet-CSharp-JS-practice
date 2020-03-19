import { Game, } from './GameLogic';
import * as Actions from './Actions';

let newGameButton = document.querySelector('#newgame');
let board = document.querySelector('#board')!;
let boardTiles = board.querySelectorAll('.board-tile');
let phaseText = document.querySelector('.game-phase')!;

let game = new Game();
createGame();

function tilePressed(this: GlobalEventHandlers, event: MouseEvent) {
    if (this instanceof HTMLTableCellElement) {
        let coord = this.dataset.coord!;
        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));
        if (game.GamePhase === 1) {
            Actions.phaseOne(game, x, y, phaseText);
        }
        if (game.GamePhase === 2) {
            Actions.phaseTwo(game, x, y, phaseText);
        }
    }

}

//before #a1887f, after #8d6e63 
function onHover(this: GlobalEventHandlers, event: MouseEvent) {
    if (this instanceof HTMLTableCellElement) {
        let coord = this.dataset.coord!;
        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));
        Actions.hoverAction(game, false, x, y);
    } else {
        console.error("Bad element type for this! - ", JSON.stringify(this));
    }

}
function onOut(this: GlobalEventHandlers, event: MouseEvent) {
    if (this instanceof HTMLTableCellElement) {
        let coord = this.dataset.coord!;
        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));
        Actions.hoverAction(game, true, x, y);
    } else {
        console.error("Bad element type for this! - ", JSON.stringify(this));
    }

}

(newGameButton as HTMLButtonElement).onclick = createGame;

for (const boardTile of boardTiles) {
    if (game.GamePhase < 3 && !game.isAIMoving()) {
        (boardTile as HTMLTableCellElement).onmouseover = onHover;
        (boardTile as HTMLTableCellElement).onmouseout = onOut;
    }
    if (game.GamePhase < 3 && !game.isAIMoving()) {
        (boardTile as HTMLTableCellElement).onclick = tilePressed;
    }

}
function createGame() {
    let mode = Number((document.querySelector('input[name="group1"]:checked') as HTMLInputElement).value)
    let starts = Number((document.querySelector('input[name="group2"]:checked') as HTMLInputElement).value)

    for (const boardTile of boardTiles) {
        (boardTile as HTMLTableCellElement).innerHTML = "";
        phaseText.innerHTML = "Phase 1 : put down your pieces."
    }
    if (mode == 1) {
        game = new Game(true, true)
    }
    else if (mode == 2) {
        if (starts == 4) {
            game = new Game(true, false)
        } else {
            game = new Game(false, true)
        }
    }
    else {
        game = new Game(false, false)
    }

}



