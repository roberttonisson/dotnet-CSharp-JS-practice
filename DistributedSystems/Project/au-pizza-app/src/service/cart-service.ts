import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { ICart, ICartCreate } from 'domain/ICart';
import { BaseService } from './base-service';

@autoinject
export class CartService extends BaseService<ICartCreate, ICart>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("Carts", appState,httpClient);
    }

}
