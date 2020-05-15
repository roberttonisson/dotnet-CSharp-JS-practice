import { IPizzaInCart } from './IPizzaInCart';
import { IAppUser } from './IAppUser';
import { StringifyOptions } from "querystring";
import { IDrinkInCart } from './IDrinkInCart';
import { IOrderStatus } from './IOrderStatus';

export interface ICart {
    id: string;
    total : number | null;
    active: boolean;

    appUserId: string;
    appUser: IAppUser | null;


    pizzaInCarts: IPizzaInCart[] | null;
    drinkInCarts: IDrinkInCart[] | null;

    }

export interface ICartCreate {

    id: string | undefined;

    active: boolean;

    appUserId: string;
    


}
