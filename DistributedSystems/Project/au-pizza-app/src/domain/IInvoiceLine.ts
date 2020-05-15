import { IInvoice } from './IInvoice';
import { IDrinkInCart } from './IDrinkInCart';
import { StringifyOptions } from "querystring";
import { IPizzaInCart } from './IPizzaInCart';

export interface IInvoiceLine {
    id: string;
    total : number | null;
    quantity : number | null;
    
    pizzaInCartId: string | null;
    pizzaInCart: IPizzaInCart | null;

    drinkInCartId: string | null;
    drinkInCart: IDrinkInCart | null;

    invoiceId: string;
    invoice:  IInvoice | null;

}

export interface IInvoiceLineCreate {
    id: string | undefined;
    quantity : number | null;
    
    pizzaInCartId: string | null;

    drinkInCartId: string | null;

    invoiceId: string;
}
