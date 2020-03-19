import { TileState } from "./TileState";


export function changeHeader(gamePhase: number){
    let htmlTile = document.querySelector('.game-phase')!;
    if (gamePhase === 1) {
        htmlTile.innerHTML = "Phase 1 : put down your pieces.";
    }else if (gamePhase === 2) {
        htmlTile.innerHTML = "Phase 2: move your pieces!";
    } else if (gamePhase === 3) {
        htmlTile.innerHTML = "Game Over!";
    }
}

export function RemovePiece(x:number, y:number){
    let htmlTile = (document.querySelector(`[data-coord='${x.toString() + y.toString()}']`)!) as HTMLTableCellElement;
    if (htmlTile instanceof HTMLTableCellElement) {
        htmlTile.innerHTML = "";
        htmlTile.style.backgroundColor = "#a1887f";
    }
}

export function AddPiece(x:number, y:number, type: TileState){
    let element = document.createElement("i");
    element.style.fontSize = "40px";
    element.classList.add("material-icons");
    let handler = (document.querySelector(`[data-coord='${x.toString() + y.toString()}']`)!) as HTMLTableCellElement;
    if (type == TileState.X) {
        let node = document.createTextNode("brightness_high");
        element.appendChild(node);
        handler.appendChild(element);
    } else {
        let node = document.createTextNode("brightness_low");
        element.appendChild(node);
        handler.appendChild(element);
    }
    handler.style.backgroundColor = "#a1887f";
}

export function changeBackground(x: number, y: number, out: boolean){
    let color1 = "#a1887f";
    let color2 = "#a1887f";
    if (out) {
        color2 = "#8d6e63"
    } else {
        color1 = "#8d6e63"
    }
    let handler = (document.querySelector(`[data-coord='${x.toString() + y.toString()}']`)!) as HTMLTableCellElement;
    handler.style.backgroundColor = color1;
}

export function SelectedPieceColor(x:number, y:number, clicked: boolean){
    let htmlTile = (document.querySelector(`[data-coord='${x.toString() + y.toString()}']`)!) as HTMLTableCellElement;
    if (htmlTile instanceof HTMLTableCellElement) {
        if (clicked) {
            htmlTile.style.backgroundColor = "#ff8000";
        }else{
            htmlTile.style.backgroundColor = "#a1887f";
        }      
    }
}