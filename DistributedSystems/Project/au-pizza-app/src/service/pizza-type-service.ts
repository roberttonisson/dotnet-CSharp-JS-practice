import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IPizzaType, IPizzaTypeCreate } from 'domain/IPizzaType';
import { BaseService } from './base-service';

@autoinject
export class PizzaTypeService extends BaseService<IPizzaType, IPizzaType>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("PizzaTypes", appState,httpClient);
    }

}
