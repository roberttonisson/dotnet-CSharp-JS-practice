import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IOrderStatus, IOrderStatusCreate } from 'domain/IOrderStatus';
import { BaseService } from './base-service';

@autoinject
export class OrderStatusService extends BaseService<IOrderStatus,IOrderStatus>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("OrderStatuss", appState,httpClient);
    }

}
