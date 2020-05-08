import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IPizzaInCart, IPizzaInCartCreate } from 'domain/IPizzaInCart';
import { BaseService } from './base-service';

@autoinject
export class PizzaInCartService extends BaseService<IPizzaInCartCreate, IPizzaInCart>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("PizzaInCarts", appState,httpClient);
    }

}
