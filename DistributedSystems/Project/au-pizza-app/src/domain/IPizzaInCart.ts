import { IAdditionalTopping } from './IAdditionalTopping';
import { ICart } from './ICart';
import { ISize } from 'domain/ISize';
import { ICrust } from './ICrust';
import { IPizzaType } from './IPizzaType';
import { StringifyOptions } from "querystring";

export interface IPizzaInCart {
    id: string;

    price: number | null;
    quantity: number;

    pizzaTypeId: string;
    pizzaType: IPizzaType | null;

    crustId: string;
    crust: ICrust | null;

    sizeId: string;
    size: ISize | null;

    cartId: string;
    cart: ICart | null;

    additionalToppings: IAdditionalTopping[] | null;
}

export interface IPizzaInCartCreate {
    
    id: string | undefined;

    quantity: number;

    pizzaTypeId: string;

    crustId: string;

    sizeId: string;

    cartId: string;

}
