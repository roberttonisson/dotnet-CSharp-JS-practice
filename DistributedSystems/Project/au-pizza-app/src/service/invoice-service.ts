import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { AppState } from 'state/app-state';
import { IFetchResponse } from 'types/IFetchResponse';
import { IInvoice, IInvoiceCreate } from 'domain/IInvoice';
import { BaseService } from './base-service';

@autoinject
export class InvoiceService extends BaseService<IInvoiceCreate, IInvoice>{

    constructor(protected appState: AppState, protected httpClient: HttpClient) {
        super("Invoices", appState, httpClient);
    }

    async reOrder(invoice: IInvoice): Promise<IFetchResponse<boolean>> {
        try {
            const response = await this.httpClient
                .post('Invoices/reOrder', JSON.stringify(invoice), {
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
