import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IDrink, IDrinkCreate } from 'domain/IDrink';
import { BaseService } from './base-service';

@autoinject
export class DrinkService extends BaseService<IDrink, IDrink>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("Drinks", appState,httpClient);
    }

}
