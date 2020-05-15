import { IInvoiceLine } from './IInvoiceLine';
import { StringifyOptions } from "querystring";
import { IAppUser } from "./IAppUser";
import { ITransport } from "./ITransport";
import { IOrderStatus } from "./IOrderStatus";

export interface IInvoice{
    id: string;
    appUserId: string;
    appUser: IAppUser | null;

    estimated: Date | null;
    createdAt: Date | null;
    total : number | null

    orderStatusId: string;
    orderStatus: IOrderStatus | null;

    isPaid: boolean;

    transportId: string;
    transport: ITransport | null;

    invoiceLines: IInvoiceLine[] | null;
}

export interface IInvoiceCreate {
    id: string | undefined;

    estimated: Date | null;
    appUserId: string;

    isPaid: boolean;

    transportId: string | undefined;
    orderStatusId: string;
}
