import { StringifyOptions } from "querystring";

export interface ISize extends ISizeCreate{
    id: string;
}

export interface ISizeCreate {
    name: string;
    sizeCm: number;
    price: number;
}
