import { IPizzaInCart } from './IPizzaInCart';
import { IAppUser } from './IAppUser';
import { StringifyOptions } from "querystring";
import { IDrinkInCart } from './IDrinkInCart';

export interface ICart {
    id: string;
    total : number | null

    appUserId: string;
    appUser: IAppUser | null;

    pizzaInCarts: IPizzaInCart[] | null;
    drinkInCarts: IDrinkInCart[] | null;

    }

export interface ICartCreate {

    id: string;

    appUserId: string;

}
