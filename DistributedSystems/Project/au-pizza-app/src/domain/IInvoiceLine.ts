import { IInvoice } from './IInvoice';
import { IDrinkInCart } from './IDrinkInCart';
import { StringifyOptions } from "querystring";
import { IPizzaInCart } from './IPizzaInCart';

export interface IInvoiceLine {
    id: string;
    total : number | null;
    quantity : number | null;
    
    pizzaInCartId: string;
    pizzaInCart: IPizzaInCart | null;

    drinkInCartId: string;
    drinkInCart: IDrinkInCart | null;

    invoiceId: string;
    invoice:  IInvoice | null;
}

export interface IInvoiceLineCreate {
    id: string | undefined;
    
    pizzaInCartId: string;

    drinkInCartId: string;

    invoiceId: string;
}
