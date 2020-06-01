import { autoinject } from 'aurelia-framework';
import { BaseService } from './base-service';
import { HttpClient } from 'aurelia-fetch-client';
import { IFetchResponse } from 'types/IFetchResponse';
import { ICulture } from 'domain/ICulture';
import { AppState } from 'state/app-state';

@autoinject
export class CultureService extends BaseService<ICulture, ICulture> {
    
    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super('Cultures', appState, httpClient);
    }


    async getAll(): Promise<IFetchResponse<ICulture[]>> {
        try {
            const response = await this.httpClient
                .fetch('Cultures', {
                    cache: "no-store"
                });
            // happy case
            if (response.ok) {
                const data = (await response.json()) as ICulture[];
                console.log(data);
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            // something went wrong
            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }

        } catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

}
