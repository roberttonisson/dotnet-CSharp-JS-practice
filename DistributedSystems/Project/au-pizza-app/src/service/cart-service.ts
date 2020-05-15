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

    async getActive(): Promise<IFetchResponse<ICart>> {
        try {
            const response = await this.httpClient
                .fetch(this.appState.baseUrl + "Carts" + "/active",{
                    cache: "no-store",
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                });
            // happy case
            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as ICart;
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
