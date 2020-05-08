import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IInvoiceLine, IInvoiceLineCreate } from 'domain/IInvoiceLine';
import { BaseService } from './base-service';

@autoinject
export class InvoiceLineService extends BaseService<IInvoiceLineCreate, IInvoiceLine>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("InvoiceLines", appState,httpClient);
    }

}
