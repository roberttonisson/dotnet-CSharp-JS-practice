import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IPartyOrderInvoice, IPartyOrderInvoiceCreate } from 'domain/IPartyOrderInvoice';
import { BaseService } from './base-service';

@autoinject
export class PartyOrderInvoiceService extends BaseService<IPartyOrderInvoice, IPartyOrderInvoice>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("PartyOrderInvoices", appState,httpClient);
    }

}
