import { TileState } from "./TileState";
export function checkForMoves(game) {
    let moves = new Array();
    let tile = TileState.O;
    if (game.PlayerOneTurn) {
        tile = TileState.X;
    }
    for (let x = 0; x < 5; x++) {
        for (let y = 0; y < 6; y++) {
            if (game.Board[x][y] == tile) {
                if (x + 1 <= 4 && game.Board[x + 1][y] == TileState.Empty) {
                    moves.push([[x, y], [x + 1, y]]);
                }
                if (x - 1 >= 0 && (game.Board[x - 1][y] == TileState.Empty)) {
                    moves.push([[x, y], [x - 1, y]]);
                }
                if (y + 1 <= 5 && game.Board[x][y + 1] == TileState.Empty) {
                    moves.push([[x, y], [x, y + 1]]);
                }
                if (y - 1 >= 0 && game.Board[x][y - 1] == TileState.Empty) {
                    moves.push([[x, y], [x, y - 1]]);
                }
            }
        }
    }
    return moves;
}
export function findBestMove(game) {
    let moves = checkForMoves(game);
    let tile = TileState.O;
    if (game.PlayerOneTurn) {
        tile = TileState.X;
    }
    for (const move of moves) {
        game.Board[move[0][0]][move[0][1]] = TileState.Empty;
        if (game.countMaxInARow(move[1][0], move[1][1]) == 3) {
            game.Board[move[0][0]][move[0][1]] = tile;
            return move;
        }
        game.Board[move[0][0]][move[0][1]] = tile;
    }
    for (const move of moves) {
        game.Board[move[0][0]][move[0][1]] = TileState.Empty;
        if (game.countMaxInARow(move[1][0], move[1][1], true) == 3) {
            game.Board[move[0][0]][move[0][1]] = tile;
            return move;
        }
        game.Board[move[0][0]][move[0][1]] = tile;
    }
    return moves[Math.floor(Math.random() * moves.length)];
}
export function AIMove(game) {
    let move = findBestMove(game);
    let type = game.Board[move[0][0]][move[0][1]];
    let htmlTile = (document.querySelector(`[data-coord='${move[0][0].toString() + move[0][1].toString()}']`));
    if (htmlTile instanceof HTMLTableCellElement) {
        htmlTile.innerHTML = "";
        htmlTile.style.backgroundColor = "#a1887f";
    }
    game.Board[move[0][0]][move[0][1]] = TileState.Empty;
    if (game.countMaxInARow(move[1][0], move[1][1]) == 3) {
        game.RemovingAPiece = true;
    }
    let element = document.createElement("i");
    element.style.fontSize = "40px";
    element.classList.add("material-icons");
    let handler = (document.querySelector(`[data-coord='${move[1][0].toString() + move[1][1].toString()}']`));
    if (type == TileState.X) {
        let node = document.createTextNode("brightness_high");
        element.appendChild(node);
        handler.appendChild(element);
        game.Board[move[1][0]][move[1][1]] = TileState.X;
    }
    else {
        let node = document.createTextNode("brightness_low");
        element.appendChild(node);
        handler.appendChild(element);
        game.Board[move[1][0]][move[1][1]] = TileState.O;
    }
    handler.style.backgroundColor = "#a1887f";
    game.PlayerOneTurn = !game.PlayerOneTurn;
    if (game.RemovingAPiece) {
        AIRemove(game);
    }
    if (game.isAIMoving() && game.GamePhase == 2) {
        AIMove(game);
    }
}
export function AIRemove(game) {
    let tiles = new Array();
    let moveWithThree;
    let movesWithTwo = new Array();
    let movesAll = new Array();
    let tile = TileState.O;
    if (game.PlayerOneTurn) {
        tile = TileState.X;
    }
    for (let x = 0; x < 5; x++) {
        for (let y = 0; y < 6; y++) {
            if (game.Board[x][y] == tile) {
                if (game.countMaxInARow(x, y, true) > 2) {
                    moveWithThree = [x, y];
                    break;
                }
                else if (game.countMaxInARow(x, y, true) == 2) {
                    movesWithTwo.push([x, y]);
                }
                else {
                    movesAll.push([x, y]);
                }
            }
        }
    }
    if (!(typeof moveWithThree === 'undefined')) {
        Remove(game, moveWithThree[0], moveWithThree[1]);
    }
    else if (movesWithTwo.length > 0) {
        let move = movesWithTwo[Math.floor(Math.random() * movesWithTwo.length)];
        Remove(game, move[0], move[1]);
    }
    else {
        let move = movesAll[Math.floor(Math.random() * movesAll.length)];
        Remove(game, move[0], move[1]);
    }
    if (game.PlayerOnePieces < 3 || game.PlayerTwoPieces < 3) {
        game.GamePhase = 3;
    }
    if (game.isAIMoving() && game.GamePhase == 2) {
        AIMove(game);
    }
}
export function Remove(game, x, y) {
    if (game.Board[x][y] == TileState.X) {
        game.PlayerOnePieces -= 1;
    }
    else {
        game.PlayerTwoPieces -= 1;
    }
    game.Board[x][y] = TileState.Empty;
    if (game.PlayerOnePieces < 3 || game.PlayerTwoPieces < 3) {
        game.GamePhase = 3;
        let htmlTile = document.querySelector('.game-phase');
        htmlTile.innerHTML = "Game Over!";
    }
    let handler = (document.querySelector(`[data-coord='${x.toString() + y.toString()}']`));
    if (handler instanceof HTMLTableCellElement) {
        handler.innerHTML = "";
        handler.style.backgroundColor = "#a1887f";
    }
    game.RemovingAPiece = false;
}
export function AIPutPiece(game) {
    let move = AIFindBestPlaceToPut(game);
    let tile = TileState.O;
    if (game.PlayerOneTurn) {
        tile = TileState.X;
        game.PlayerOnePieces += 1;
    }
    else {
        game.PlayerTwoPieces += 1;
    }
    if (game.PlayerOnePieces >= 12 || game.PlayerTwoPieces >= 12) {
        game.GamePhase = 2;
        let htmlTile = document.querySelector('.game-phase');
        htmlTile.innerHTML = "Move your pieces!";
    }
    game.Board[move[0]][move[1]] = tile;
    game.PlayerOneTurn = !game.PlayerOneTurn;
    let htmlTile = document.querySelector(`[data-coord='${move[0].toString() + move[1].toString()}']`);
    let element = document.createElement("i");
    element.style.fontSize = "40px";
    element.classList.add("material-icons");
    let text = 'brightness_high';
    if (game.PlayerOneTurn) {
        text = 'brightness_low';
    }
    let node = document.createTextNode(text);
    element.appendChild(node);
    htmlTile.appendChild(element);
    if (game.isAIMoving() && game.GamePhase == 1) {
        AIPutPiece(game);
    }
    if (game.isAIMoving() && game.GamePhase === 2) {
        AIMove(game);
    }
}
export function AIFindBestPlaceToPut(game) {
    let myTile = TileState.O;
    let oppTile = TileState.X;
    let move;
    let allMoves = new Array();
    if (game.PlayerOneTurn) {
        myTile = TileState.X;
        oppTile = TileState.O;
    }
    for (let x = 0; x < 5; x++) {
        for (let y = 0; y < 6; y++) {
            if (game.Board[x][y] == TileState.Empty) {
                allMoves.push([x, y]);
                move = MoveHelper(game, myTile, x, y);
                if (!(typeof move === 'undefined')) {
                    return move;
                }
                move = MoveHelper(game, oppTile, x, y, true);
                if (!(typeof move === 'undefined')) {
                    return move;
                }
            }
        }
    }
    return allMoves[Math.floor(Math.random() * allMoves.length)];
}
export function CalcUp(game, x, y, tile) {
    let total = 0;
    for (let i = 1; i < 5; i++) {
        if (x + i <= 4 && game.Board[x + i][y] == tile) {
            total += 1;
            continue;
        }
        break;
    }
    return total;
}
export function CalcDown(game, x, y, tile) {
    let total = 0;
    for (let i = 1; i < 5; i++) {
        if (x - i >= 0 && game.Board[x - i][y] == tile) {
            total += 1;
            continue;
        }
        break;
    }
    return total;
}
export function CalcLeft(game, x, y, tile) {
    let total = 0;
    for (let i = 1; i < 5; i++) {
        if (y + i <= 5 && game.Board[x][y + i] == tile) {
            total += 1;
            continue;
        }
        break;
    }
    return total;
}
export function CalcRight(game, x, y, tile) {
    let total = 0;
    for (let i = 1; i < 5; i++) {
        if (y - i >= 0 && game.Board[x][y - 1] == tile) {
            total += 1;
            continue;
        }
        break;
    }
    return total;
}
function MoveHelper(game, tile, x, y, denyOpp = false) {
    if (CalcUp(game, x, y, tile) >= 2) {
        if (denyOpp) {
            if (game.countMaxInARow(x, y)) {
                return [x, y];
            }
        }
        else {
            if (x - 1 >= 0 && game.Board[x - 1][y] == TileState.Empty) {
                if (game.countMaxInARow(x - 1, y) < 4) {
                    return [x - 1, y];
                }
            }
            if (y + 1 <= 5 && game.Board[x][y + 1] == TileState.Empty) {
                if (game.countMaxInARow(x, y + 1) < 4) {
                    return [x, y + 1];
                }
            }
            if (y - 1 >= 0 && game.Board[x][y - 1] == TileState.Empty) {
                if (game.countMaxInARow(x, y - 1) < 4) {
                    return [x, y - 1];
                }
            }
        }
    }
    if (CalcDown(game, x, y, tile) >= 2) {
        if (denyOpp) {
            if (game.countMaxInARow(x, y)) {
                return [x, y];
            }
        }
        else {
            if (x + 1 <= 4 && game.Board[x + 1][y] == TileState.Empty) {
                if (game.countMaxInARow(x + 1, y) < 4) {
                    return [x + 1, y];
                }
            }
            if (y + 1 <= 5 && game.Board[x][y + 1] == TileState.Empty) {
                if (game.countMaxInARow(x, y + 1) < 4) {
                    return [x, y + 1];
                }
            }
            if (y - 1 >= 0 && game.Board[x][y - 1] == TileState.Empty) {
                if (game.countMaxInARow(x, y - 1) < 4) {
                    return [x, y - 1];
                }
            }
        }
    }
    if (CalcLeft(game, x, y, tile) >= 2) {
        if (denyOpp) {
            if (game.countMaxInARow(x, y)) {
                return [x, y];
            }
        }
        else {
            if (x + 1 <= 4 && game.Board[x + 1][y] == TileState.Empty) {
                if (game.countMaxInARow(x + 1, y) < 4) {
                    return [x + 1, y];
                }
            }
            if (y + 1 <= 5 && game.Board[x][y + 1] == TileState.Empty) {
                if (game.countMaxInARow(x, y + 1) < 4) {
                    return [x, y + 1];
                }
            }
            if (x - 1 >= 0 && game.Board[x - 1][y] == TileState.Empty) {
                if (game.countMaxInARow(x - 1, y) < 4) {
                    return [x - 1, y];
                }
            }
        }
    }
    if (CalcRight(game, x, y, tile) >= 2) {
        if (denyOpp) {
            if (game.countMaxInARow(x, y)) {
                return [x, y];
            }
        }
        else {
            if (x + 1 <= 4 && game.Board[x + 1][y] == TileState.Empty) {
                if (game.countMaxInARow(x + 1, y) < 4) {
                    return [x + 1, y];
                }
            }
            if (y - 1 >= 0 && game.Board[x][y - 1] == TileState.Empty) {
                if (game.countMaxInARow(x, y - 1) < 4) {
                    return [x, y - 1];
                }
            }
            if (x - 1 >= 0 && game.Board[x - 1][y] == TileState.Empty) {
                if (game.countMaxInARow(x + 1, y) < 4) {
                    return [x - 1, y];
                }
            }
        }
    }
}
export function Sleep(milliseconds) {
    const date = Date.now();
    let currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < milliseconds);
}
//# sourceMappingURL=AI.js.map