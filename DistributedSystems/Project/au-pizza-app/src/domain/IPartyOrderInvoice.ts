import { IInvoice } from './IInvoice';
import { StringifyOptions } from "querystring";
import { IPartyOrder } from "./IPartyOrder";

export interface IPartyOrderInvoice {
    id: string;

    partyOrderId: string;
    partyOrder: IPartyOrder | null;

    invoiceId: string;
    invoice: IInvoice | null;
}

export interface IPartyOrderInvoiceCreate {
    id: string | undefined;
    partyOrderId: string;

    invoiceId: string;

}
