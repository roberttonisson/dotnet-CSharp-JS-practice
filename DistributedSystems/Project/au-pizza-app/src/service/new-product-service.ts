import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { INewProduct, INewProductCreate } from 'domain/INewProduct';
import { BaseService } from './base-service';

@autoinject
export class NewProductService extends BaseService<INewProductCreate, INewProduct>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("NewProducts", appState,httpClient);
    }

}
