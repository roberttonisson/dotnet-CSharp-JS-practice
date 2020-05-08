import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IInvoice, IInvoiceCreate } from 'domain/IInvoice';
import { BaseService } from './base-service';

@autoinject
export class InvoiceService extends BaseService<IInvoiceCreate, IInvoice>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("Invoices", appState,httpClient);
    }

}
