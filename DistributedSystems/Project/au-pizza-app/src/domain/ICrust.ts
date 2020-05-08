import { StringifyOptions } from "querystring";

export interface ICrust extends ICrustCreate{
    id: string;
}

export interface ICrustCreate {
    name: string;
    price: number;
}
