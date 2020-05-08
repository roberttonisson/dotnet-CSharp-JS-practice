import { StringifyOptions } from "querystring";

export interface ITopping extends IToppingCreate{
    id: string;
}

export interface IToppingCreate {
    name: string;
    price: number;
}
