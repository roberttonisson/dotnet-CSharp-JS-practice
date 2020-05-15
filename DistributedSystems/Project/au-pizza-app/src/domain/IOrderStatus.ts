import { StringifyOptions } from "querystring";

export interface IOrderStatus  extends IOrderStatusCreate{
    id: string | undefined;
}

export interface IOrderStatusCreate {
    id: string | undefined;
    status: string;
}
