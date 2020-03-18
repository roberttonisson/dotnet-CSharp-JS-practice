import { TileState } from './TileState'
import * as AI from './AI'


export class Game {

    public Board: Array<Array<TileState>>;

    public PlayerOneTurn: boolean = true;

    public PlayerOneHuman: boolean = true;

    public PlayerTwoHuman: boolean = true;

    public PlayerOnePieces: number = 0;

    public PlayerTwoPieces: number = 0;

    public GamePhase: number = 1;

    public Selected: HTMLTableCellElement | undefined;

    public RemovingAPiece: boolean = false;


    constructor(PlayerOneHuman: boolean = true, PlayerTwoHuman: boolean = true) {
        this.Board = [];
        this.PlayerOneHuman = PlayerOneHuman;
        this.PlayerTwoHuman = PlayerTwoHuman
        for (let index = 0; index < 5; index++) {
            this.Board.push(
                [
                    TileState.Empty,
                    TileState.Empty,
                    TileState.Empty,
                    TileState.Empty,
                    TileState.Empty,
                    TileState.Empty
                ]
            )
        }
        if (this.isAIMoving()) {
            AI.AIPutPiece(this);
        }
    }

    public MovePhaseOne(coord: string) {
        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));
        if (this.PlayerOneTurn) {
            this.PlayerOnePieces += 1;
            this.Board[x][y] = TileState.X;
        } else {
            this.Board[x][y] = TileState.O;
            this.PlayerTwoPieces += 1;
        }
        this.PlayerOneTurn = !this.PlayerOneTurn;
        if (this.PlayerOnePieces + this.PlayerTwoPieces >= 24) {
            this.GamePhase = 2;
        } else {
            if (this.isAIMoving()) {
                AI.AIPutPiece(this);
            }
        }
        if (this.isAIMoving() && this.GamePhase == 2) {
            AI.AIMove(this);
        }

    }

    //TODO
    public MovePhaseTwo(coord: string, handler: HTMLTableCellElement) {
        let piece = this.Selected?.dataset.coord!;
        let pieceX = Number(piece.charAt(0));
        let pieceY = Number(piece.charAt(1));
        let type = this.Board[pieceX][pieceY];
        if (this.Selected instanceof HTMLTableCellElement) {
            this.Selected.innerHTML = "";
            this.Selected.style.backgroundColor = "#a1887f";
            this.Selected = undefined;
        }
        this.Board[pieceX][pieceY] = TileState.Empty;


        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));
        if (this.countMaxInARow(x, y) == 3) {
            this.RemovingAPiece = true;
        }

        let element = document.createElement("i");
        element.style.fontSize = "40px";
        element.classList.add("material-icons");
        if (type == TileState.X) {
            let node = document.createTextNode("brightness_high");
            element.appendChild(node);
            handler.appendChild(element);
            this.Board[x][y] = TileState.X;
        } else {
            let node = document.createTextNode("brightness_low");
            element.appendChild(node);
            handler.appendChild(element);
            this.Board[x][y] = TileState.O;
        }
        handler.style.backgroundColor = "#a1887f";
        this.PlayerOneTurn = !this.PlayerOneTurn;
        if(this.PlayerOnePieces < 3 || this.PlayerTwoPieces < 3){
            this.GamePhase = 3;
            let htmlTile = document.querySelector(`.game-phase`)!;
            htmlTile.innerHTML = "Game Over!";
        }
        if (this.isAIMoving() && this.GamePhase == 2 && !this.RemovingAPiece) {
            AI.AIMove(this);
        }

    }

    public isValidMovePhaseOne(coord: string): boolean {
        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));

        if (!(this.Board[x][y] == TileState.Empty)) {
            return false;
        }
        if (this.countMaxInARow(x, y) >= 4) {
            return false;
        }
        return true;
    }

    public isValidMovePhaseTwo(coord: string): boolean {
        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));
        if (!(this.Board[x][y] == TileState.Empty)) {
            return false;
        }

        let piece = this.Selected?.dataset.coord!;
        let pieceX = Number(piece.charAt(0));
        let pieceY = Number(piece.charAt(1));
        if (!(
            (x + 1 <= 4 && (x + 1 == pieceX) && (y == pieceY))
            || x - 1 >= 0 && ((x - 1 == pieceX) && (y == pieceY))
            || y + 1 <= 5 && ((x == pieceX) && (y + 1 == pieceY))
            || y - 1 >= 0 && ((x == pieceX) && (y - 1 == pieceY))
        )) {
            return false;
        }

        if (x + 1 <= 4 && (x + 1 == pieceX) && (y == pieceY)) {
            if (!this.checkUp(pieceX, pieceY)) {
                return false;
            }
        }
        if ( x - 1 >= 0 && (x - 1 == pieceX) && (y == pieceY)) {
            if (!this.checkDown(pieceX, pieceY)) {
                return false;
            }
        }
        if ( y + 1 <= 5 && (x == pieceX) && (y + 1 == pieceY)) {
            if (!this.checkLeft(pieceX, pieceY)) {
                return false;
            }
        }
        if ( y - 1 >= 0 && (x == pieceX) && (y - 1 == pieceY)) {
            if (!this.checkRight(pieceX, pieceY)) {
                return false;
            }
        }


        return true;
    }


    public isValidPiecePhaseTwo(coord: string): boolean {
        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));

        if (this.Board[x][y] == TileState.Empty) {
            return false;
        }

        if (this.PlayerOneTurn) {
            if (!(this.Board[x][y] == TileState.X)) {
                return false;
            }

        } else {
            if (!(this.Board[x][y] == TileState.O)) {
                return false;
            }
        }

        if (this.checkUp(x, y) || this.checkDown(x, y) || this.checkLeft(x, y) || this.checkRight(x, y)) {
            return true;
        }
        return false;
    }

    public RemovePiece(coord: string, handler: HTMLTableCellElement) {

        let x = Number(coord.charAt(0));
        let y = Number(coord.charAt(1));
        if (this.Board[x][y] == TileState.X) {
            this.PlayerOnePieces -= 1;
        } else {
            this.PlayerTwoPieces -= 1;
        }
        this.Board[x][y] = TileState.Empty;

        if (handler instanceof HTMLTableCellElement) {
            handler.innerHTML = "";
            handler.style.backgroundColor = "#a1887f";
        }
        this.RemovingAPiece = false;
        if(AI.checkForMoves(this).length == 0){
            this.GamePhase = 3;
            let htmlTile = document.querySelector(`.game-phase`)!;
            htmlTile.innerHTML = "Game Over!";
        }
        if (this.isAIMoving() && this.GamePhase == 2) {
            AI.AIMove(this);
        }

    }

    public checkUp(x: number, y: number): boolean {
        if (x - 1 >= 0 && this.Board[x - 1][y] == TileState.Empty) {
            let piece = this.Board[x][y];
            this.Board[x][y] = TileState.Empty
            let max = this.countMaxInARow(x - 1, y);
            this.Board[x][y] = piece;
            if (max > 3) {
                return false;
            }
            return true;
        }
        return false;
    }
    public checkDown(x: number, y: number, isMoving: number = 0): boolean {
        if (x + 1 <= 4 && this.Board[x + 1][y] == TileState.Empty) {
            let piece = this.Board[x][y];
            this.Board[x][y] = TileState.Empty
            let max = this.countMaxInARow(x + 1, y);
            this.Board[x][y] = piece;
            if (max > 3) {
                return false;
            }
            return true;
        }
        return false;
    }
    public checkLeft(x: number, y: number): boolean {
        if (y - 1 >= 0 && this.Board[x][y - 1] == TileState.Empty) {
            let piece = this.Board[x][y];
            this.Board[x][y] = TileState.Empty
            let max = this.countMaxInARow(x, y - 1);
            this.Board[x][y] = piece;
            if (max > 3) {
                return false;
            }
            return true;
        }
        return false;
    }
    public checkRight(x: number, y: number): boolean {
        if (y + 1 <= 5 && this.Board[x][y + 1] == TileState.Empty) {
            let piece = this.Board[x][y];
            this.Board[x][y] = TileState.Empty
            let max = this.countMaxInARow(x, y + 1);
            this.Board[x][y] = piece;
            if (max > 3) {
                return false;
            }
            return true;
        }
        return false;
    }

    public isPhaseOneOver(): boolean {
        if (this.PlayerOnePieces + this.PlayerTwoPieces >= 24) {
            return true;
        }
        return false;
    }

    public isAIMoving(): boolean {
        if ((this.PlayerOneTurn && !this.PlayerOneHuman) || (!this.PlayerOneTurn && !this.PlayerTwoHuman)) {
            return true;
        }
        return false;
    }


    public countMaxInARow(x: number, y: number, checkForEnemy: boolean = false): number {
        let horizontal = 1;
        let vertical = 1;
        let tile = this.PlayerOneTurn ? TileState.X : TileState.O;
        if (checkForEnemy) {
            let tile = this.PlayerOneTurn ? TileState.O : TileState.X;
        }

        //vertical total
        for (let i = 1; i < 5; i++) {
            if (x + i <= 4 && this.Board[x + i][y] == tile) {
                vertical += 1;
                continue;
            }
            break;
        }
        for (let i = 1; i < 5; i++) {
            if (x - i >= 0 && this.Board[x - i][y] == tile) {
                vertical += 1;
                continue;
            }
            break;
        }

        //horizontal total
        for (let i = 1; i < 6; i++) {
            if (y + i <= 5 && this.Board[x][y + i] == tile) {
                horizontal += 1;
                continue;
            }
            break;
        }
        for (let i = 1; i < 6; i++) {
            if (y - i >= 0 && this.Board[x][y - i] == tile) {
                horizontal += 1;
                continue;
            }
            break;
        }

        return Math.max(vertical, horizontal);

    }

}