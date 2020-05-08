import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IAdditionalTopping, IAdditionalToppingCreate } from 'domain/IAdditionalTopping';
import { BaseService } from './base-service';

@autoinject
export class AdditionalToppingService extends BaseService<IAdditionalToppingCreate,IAdditionalTopping>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("AdditionalToppings", appState,httpClient);
    }

}
