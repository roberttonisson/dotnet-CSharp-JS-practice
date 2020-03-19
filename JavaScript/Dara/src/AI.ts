import { Game } from "./GameLogic";
import { TileState } from "./TileState";
import * as UI from "./UI";

export function checkForMoves(game: Game): Array<[[number, number], [number, number]]> {
    let moves = new Array<[[number, number], [number, number]]>();
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

export function findBestMove(game: Game): [[number, number], [number, number]] {
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

export function AIMove(game: Game) {
    let move = findBestMove(game);
    let type = game.Board[move[0][0]][move[0][1]];

    UI.RemovePiece(move[0][0], move[0][1]);

    game.Board[move[0][0]][move[0][1]] = TileState.Empty;

    if (game.countMaxInARow(move[1][0], move[1][1]) == 3) {
        game.RemovingAPiece = true;
    }

    if (type == TileState.X) {
        game.Board[move[1][0]][move[1][1]] = TileState.X;
    } else {
        game.Board[move[1][0]][move[1][1]] = TileState.O;
    }
    UI.AddPiece(move[1][0], move[1][1], type);

    game.PlayerOneTurn = !game.PlayerOneTurn;

    if (game.RemovingAPiece) {
        AIRemove(game);
    }
    if (game.isAIMoving() && game.GamePhase == 2) {
        AIMove(game);
    }

}

export function AIRemove(game: Game) {
    let tiles = new Array<[number, number]>();
    let moveWithThree: [number, number] | undefined;
    let movesWithTwo = new Array<[number, number]>();
    let movesAll = new Array<[number, number]>();


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
                } else if (game.countMaxInARow(x, y, true) == 2) {
                    movesWithTwo.push([x, y]);
                } else {
                    movesAll.push([x, y]);
                }
            }
        }
    }
    if (!(typeof moveWithThree === 'undefined')) {
        Remove(game, moveWithThree[0], moveWithThree[1]);
    } else if (movesWithTwo.length > 0) {
        let move = movesWithTwo[Math.floor(Math.random() * movesWithTwo.length)];
        Remove(game, move[0], move[1])
    } else {
        let move = movesAll[Math.floor(Math.random() * movesAll.length)];
        Remove(game, move[0], move[1])
    }
    if (game.PlayerOnePieces < 3 || game.PlayerTwoPieces < 3) {
        game.GamePhase = 3;
    }
    if (game.isAIMoving() && game.GamePhase == 2) {
        AIMove(game);
    }
}

export function Remove(game: Game, x: number, y: number) {
    if (game.Board[x][y] == TileState.X) {
        game.PlayerOnePieces -= 1;
    } else {
        game.PlayerTwoPieces -= 1;
    }
    game.Board[x][y] = TileState.Empty;
    if (game.PlayerOnePieces < 3 || game.PlayerTwoPieces < 3) {
        game.GamePhase = 3;
        UI.changeHeader(game.GamePhase);
    }

    UI.RemovePiece(x, y);
    game.RemovingAPiece = false;

}


export function AIPutPiece(game: Game) {
    let move = AIFindBestPlaceToPut(game);
    let tile = TileState.O;
    if (game.PlayerOneTurn) {
        tile = TileState.X;
        game.PlayerOnePieces += 1;
    } else {
        game.PlayerTwoPieces += 1;
    }
    if (game.PlayerOnePieces >= 12 || game.PlayerTwoPieces >= 12) {
        game.GamePhase = 2;
        UI.changeHeader(game.GamePhase)
    }
    game.Board[move[0]][move[1]] = tile;
    game.PlayerOneTurn = !game.PlayerOneTurn;

    UI.AddPiece(move[0], move[1], tile);

    if (game.isAIMoving() && game.GamePhase == 1) {
        AIPutPiece(game);
    }
    if (game.isAIMoving() && game.GamePhase === 2) {
        AIMove(game);
    }

}

export function AIFindBestPlaceToPut(game: Game): [number, number] {
    let myTile = TileState.O;
    let oppTile = TileState.X;
    let move: [number, number] | undefined;
    let allMoves = new Array<[number, number]>();
    if (game.PlayerOneTurn) {
        myTile = TileState.X;
        oppTile = TileState.O
    }
    for (let x = 0; x < 5; x++) {
        for (let y = 0; y < 6; y++) {
            if (game.Board[x][y] == TileState.Empty) {
                allMoves.push([x, y]);
                move = MoveHelper(game, myTile, x, y)
                if (!(typeof move === 'undefined')) {
                    return move;
                }
                move = MoveHelper(game, oppTile, x, y, true)
                if (!(typeof move === 'undefined')) {
                    return move;
                }
            }
        }
    }
    return allMoves[Math.floor(Math.random() * allMoves.length)];
}

export function CalcUp(game: Game, x: number, y: number, tile: TileState): number {
    let total: number = 0;
    for (let i = 1; i < 5; i++) {
        if (x + i <= 4 && game.Board[x + i][y] == tile) {
            total += 1;
            continue;
        }
        break;
    }
    return total;
}

export function CalcDown(game: Game, x: number, y: number, tile: TileState): number {
    let total: number = 0;
    for (let i = 1; i < 5; i++) {
        if (x - i >= 0 && game.Board[x - i][y] == tile) {
            total += 1;
            continue;
        }
        break;
    }
    return total;
}

export function CalcLeft(game: Game, x: number, y: number, tile: TileState): number {
    let total: number = 0;
    for (let i = 1; i < 5; i++) {
        if (y + i <= 5 && game.Board[x][y + i] == tile) {
            total += 1;
            continue;
        }
        break;
    }
    return total;
}

export function CalcRight(game: Game, x: number, y: number, tile: TileState): number {
    let total: number = 0;
    for (let i = 1; i < 5; i++) {
        if (y - i >= 0 && game.Board[x][y - 1] == tile) {
            total += 1;
            continue;
        }
        break;
    }

    return total;

}

function MoveHelper(game: Game, tile: TileState, x: number, y: number, denyOpp: boolean = false): [number, number] | undefined {
    if (CalcUp(game, x, y, tile) >= 2) {
        if (denyOpp) {
            if (game.countMaxInARow(x, y)) {
                return [x, y];
            }
        } else {
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
        } else {
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
        } else {
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
        } else {
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

export function Sleep(milliseconds: number) {
    const date = Date.now();
    let currentDate = null;
    do {
        currentDate = Date.now();
    } while (currentDate - date < milliseconds);
}