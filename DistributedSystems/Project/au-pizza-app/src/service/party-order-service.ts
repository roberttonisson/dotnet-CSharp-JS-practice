import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IPartyOrder, IPartyOrderCreate } from 'domain/IPartyOrder';
import { BaseService } from './base-service';

@autoinject
export class PartyOrderService extends BaseService<IPartyOrderCreate, IPartyOrder>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("PartyOrders", appState,httpClient);
    }

}
