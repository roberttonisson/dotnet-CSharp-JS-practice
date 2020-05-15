import { StringifyOptions } from "querystring";

export interface ITransport  extends ITransportCreate{
    
}

export interface ITransportCreate {
    id: string | undefined;
    address: string;
    cost: number;
}
