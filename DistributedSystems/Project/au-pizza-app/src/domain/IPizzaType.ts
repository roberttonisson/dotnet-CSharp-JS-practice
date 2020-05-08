import { StringifyOptions } from "querystring";

export interface IPizzaType extends IPizzaTypeCreate{
    id: string;
}

export interface IPizzaTypeCreate {
    name: string;
    price: number;
}
