import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { ICrust, ICrustCreate } from 'domain/ICrust';
import { BaseService } from './base-service';

@autoinject
export class CrustService extends BaseService<ICrust,ICrust>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("Crusts", appState, httpClient);
    }

}
