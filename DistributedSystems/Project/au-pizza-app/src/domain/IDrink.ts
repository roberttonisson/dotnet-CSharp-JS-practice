import { StringifyOptions } from "querystring";

export interface IDrink extends IDrinkCreate{
    id: string;
}

export interface IDrinkCreate {
    name: string;
    price: number;
    size: number
}
