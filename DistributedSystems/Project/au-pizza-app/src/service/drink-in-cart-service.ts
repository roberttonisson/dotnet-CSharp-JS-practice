import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IDrinkInCart, IDrinkInCartCreate } from 'domain/IDrinkInCart';
import { BaseService } from './base-service';

@autoinject
export class DrinkInCartService extends BaseService<IDrinkInCartCreate,IDrinkInCart>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("DrinkInCarts", appState,httpClient);
    }

}
