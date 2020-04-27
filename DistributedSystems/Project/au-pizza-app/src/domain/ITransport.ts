import { StringifyOptions } from "querystring";

export interface ITransport {
    id: string;
    address: string;
    cost: number;
}

export interface ITransportCreate {
    address: string;
    cost: number;
}
