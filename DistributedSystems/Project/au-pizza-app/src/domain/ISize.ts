import { StringifyOptions } from "querystring";

export interface ISize {
    id: string;
    name: string;
    sizeCm: number;
    price: number;
}

export interface ISizeCreate {
    name: string;
    sizeCm: number;
    price: number;
}
