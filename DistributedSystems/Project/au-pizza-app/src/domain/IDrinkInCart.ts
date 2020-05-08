import { ICart } from './ICart';
import { IDrink } from './IDrink';
import { StringifyOptions } from "querystring";

export interface IDrinkInCart{
    id: string;
    quantity: number;
    price: number | null;
    
    drinkId: string;
    drink: IDrink | null;

    cartId: string;
    cart: ICart | null;
}

export interface IDrinkInCartCreate {
    id: string | undefined;
    quantity: number;
    
    drinkId: string;

    cartId: string;
}
