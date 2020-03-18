import { Game, } from './GameLogic';
import * as Actions from './Actions';
let newGameButton = document.querySelector('#newgame');
let board = document.querySelector('#board');
let boardTiles = board.querySelectorAll('.board-tile');
let phaseText = document.querySelector('.game-phase');
let game = new Game();
createGame();
function tilePressed(event) {
    if (this instanceof HTMLTableCellElement) {
        let coord = this.dataset.coord;
        if (game.GamePhase === 1) {
            Actions.phaseOne(game, coord, this, phaseText);
        }
        if (game.GamePhase === 2) {
            Actions.phaseTwo(game, coord, this, phaseText);
        }
    }
}
function onHover(event) {
    if (this instanceof HTMLTableCellElement) {
        Actions.hoverAction(this, game, false);
    }
    else {
        console.error("Bad element type for this! - ", JSON.stringify(this));
    }
}
function onOut(event) {
    if (this instanceof HTMLTableCellElement) {
        Actions.hoverAction(this, game, true);
    }
    else {
        console.error("Bad element type for this! - ", JSON.stringify(this));
    }
}
newGameButton.onclick = createGame;
for (const boardTile of boardTiles) {
    if (game.GamePhase < 3 && !game.isAIMoving()) {
        boardTile.onmouseover = onHover;
        boardTile.onmouseout = onOut;
    }
    if (game.GamePhase < 3 && !game.isAIMoving()) {
        boardTile.onclick = tilePressed;
    }
}
function createGame() {
    let mode = Number(document.querySelector('input[name="group1"]:checked').value);
    let starts = Number(document.querySelector('input[name="group2"]:checked').value);
    for (const boardTile of boardTiles) {
        boardTile.innerHTML = "";
        phaseText.innerHTML = "Phase 1 : put down your pieces.";
    }
    if (mode == 1) {
        game = new Game(true, true);
    }
    else if (mode == 2) {
        if (starts == 4) {
            game = new Game(true, false);
        }
        else {
            game = new Game(false, true);
        }
    }
    else {
        game = new Game(false, false);
    }
}
//# sourceMappingURL=index.js.map