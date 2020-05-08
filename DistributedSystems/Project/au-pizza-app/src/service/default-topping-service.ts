import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IDefaultTopping, IDefaultToppingCreate } from 'domain/IDefaultTopping';
import { BaseService } from './base-service';

@autoinject
export class DefaultToppingService extends BaseService<IDefaultToppingCreate, IDefaultTopping>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("DefaultToppings", appState,httpClient);
    }

}
