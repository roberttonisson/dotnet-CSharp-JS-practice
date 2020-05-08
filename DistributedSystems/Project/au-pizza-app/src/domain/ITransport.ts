import { StringifyOptions } from "querystring";

export interface ITransport  extends ITransportCreate{
    id: string;
}

export interface ITransportCreate {
    address: string;
    cost: number;
}
