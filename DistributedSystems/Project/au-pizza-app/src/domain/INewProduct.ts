import { IPizzaType } from './IPizzaType';
import { StringifyOptions } from "querystring";

export interface INewProduct{
    id: string;

    pizzaTypeId: string;
    pizzaType: IPizzaType | null;

    isActive: boolean;

    description: string;
}

export interface INewProductCreate {
    id: string | undefined;

    pizzaTypeId: string;

    isActive: boolean;

    description: string;
}
