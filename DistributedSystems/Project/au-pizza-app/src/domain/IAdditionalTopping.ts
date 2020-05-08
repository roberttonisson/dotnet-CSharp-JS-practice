import { ITopping } from 'domain/ITopping';
import { StringifyOptions } from "querystring";
import { IPizzaInCart } from './IPizzaInCart';

export interface IAdditionalTopping {
    id: string;

    toppingId: string;
    topping: ITopping | null;

    pizzaInCartId: string;
    pizzaInCart: IPizzaInCart | null;

    }

export interface IAdditionalToppingCreate {
    id: string | undefined;
    toppingId: string;
    pizzaInCartId: string;
}
