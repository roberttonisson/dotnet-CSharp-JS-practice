import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IPizzaInCart, IPizzaInCartCreate } from 'domain/IPizzaInCart';
import { BaseService } from './base-service';

@autoinject
export class PizzaInCartService extends BaseService<IPizzaInCartCreate, IPizzaInCart>{

    constructor(protected  appState: AppState, protected  httpClient: HttpClient){
        super("PizzaInCarts", appState,httpClient);
    }

    async deleteCascade(id: string, pic: IPizzaInCart): Promise<IFetchResponse<boolean>> {
        try {
            const response = await this.httpClient
                .delete('PizzaInCarts/delete/' + id, JSON.stringify(pic), {
                    cache: 'no-store',
                    headers: {
                        authorization: "Bearer " + this.appState.jwt
                    }
                })

            if (response.status >= 200 && response.status < 300) {
                const data = (await response.json()) as boolean;
                return {
                    statusCode: response.status,
                    data: data
                }
            }

            return {
                statusCode: response.status,
                errorMessage: response.statusText
            }
        }
        catch (reason) {
            return {
                statusCode: 0,
                errorMessage: JSON.stringify(reason)
            }
        }
    }

}
