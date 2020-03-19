import { TileState } from './TileState'
import * as AI from './AI'
import * as UI from './UI'


export class Game {

    public Board: Array<Array<TileState>>;

    public PlayerOneTurn: boolean = true;

    public PlayerOneHuman: boolean = true;

    public PlayerTwoHuman: boolean = true;

    public PlayerOnePieces: number = 0;

    public PlayerTwoPieces: number = 0;

    public GamePhase: number = 1;

    public Selected: [number, number] | undefined;

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

    public MovePhaseOne(x: number, y:number) {
        let type = TileState.O
        if (this.PlayerOneTurn) {
            this.PlayerOnePieces += 1;
            this.Board[x][y] = TileState.X;
            type = TileState.X;
        } else {
            this.Board[x][y] = TileState.O;
            this.PlayerTwoPieces += 1;
        }
        UI.AddPiece(x, y, type);

        this.PlayerOneTurn = !this.PlayerOneTurn;
        if (this.PlayerOnePieces + this.PlayerTwoPieces >= 24) {
            this.GamePhase = 2;
            UI.changeHeader(this.GamePhase)
        } else {
            if (this.isAIMoving()) {
                AI.AIPutPiece(this);
            }
        }
        if (this.isAIMoving() && this.GamePhase == 2) {
            AI.AIMove(this);
        }

    }

    public MovePhaseTwo(x: number, y:number) {
        let pieceX = this.Selected![0];
        let pieceY = this.Selected![1];
        let type = this.Board[pieceX][pieceY];

        UI.RemovePiece(pieceX, pieceY);
        this.Board[pieceX][pieceY] = TileState.Empty;
        
        if (this.countMaxInARow(x, y) == 3) {
            this.RemovingAPiece = true;
        }
        if (type == TileState.X) {
            this.Board[x][y] = TileState.X;
        } else {
            this.Board[x][y] = TileState.O;
        }
        UI.AddPiece(x, y, type)
        this.Selected = undefined;
        this.PlayerOneTurn = !this.PlayerOneTurn;

        if (this.PlayerOnePieces < 3 || this.PlayerTwoPieces < 3) {
            this.GamePhase = 3;
            UI.changeHeader(this.GamePhase);
        }
        if (this.isAIMoving() && this.GamePhase == 2 && !this.RemovingAPiece) {
            AI.AIMove(this);
        }

    }

    public isValidMovePhaseOne(x: number, y:number): boolean {
        if (!(this.Board[x][y] == TileState.Empty)) {
            return false;
        }
        if (this.countMaxInARow(x, y) >= 4) {
            return false;
        }
        return true;
    }

    public isValidMovePhaseTwo(x: number, y:number): boolean {
        if (!(this.Board[x][y] == TileState.Empty)) {
            return false;
        }
        let pieceX = this.Selected![0];
        let pieceY = this.Selected![1];

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
        if (x - 1 >= 0 && (x - 1 == pieceX) && (y == pieceY)) {
            if (!this.checkDown(pieceX, pieceY)) {
                return false;
            }
        }
        if (y + 1 <= 5 && (x == pieceX) && (y + 1 == pieceY)) {
            if (!this.checkLeft(pieceX, pieceY)) {
                return false;
            }
        }
        if (y - 1 >= 0 && (x == pieceX) && (y - 1 == pieceY)) {
            if (!this.checkRight(pieceX, pieceY)) {
                return false;
            }
        }


        return true;
    }


    public isValidPiecePhaseTwo(x: number, y: number): boolean {
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

    public RemovePiece(x: number, y:number) {
        if (this.Board[x][y] == TileState.X) {
            this.PlayerOnePieces -= 1;
        } else {
            this.PlayerTwoPieces -= 1;
        }
        this.Board[x][y] = TileState.Empty;

        UI.RemovePiece(x, y);
        this.RemovingAPiece = false;

        if (AI.checkForMoves(this).length == 0) {
            this.GamePhase = 3;
            UI.changeHeader(this.GamePhase)
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