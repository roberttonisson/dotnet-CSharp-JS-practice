import { StringifyOptions } from "querystring";
import { IAppUser } from "./IAppUser";
import { ITransport } from "./ITransport";

export interface IInvoice{
    id: string;
    appUserId: string;
    appUser: IAppUser | null;

    total : number | null

    isPaid: boolean;

    transportId: string;
    transport: ITransport | null;
}

export interface IInvoiceCreate {
    id: string | undefined;
    appUserId: string;

    isPaid: boolean;

    transportId: string;
}
