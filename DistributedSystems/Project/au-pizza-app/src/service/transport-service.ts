import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ITransport } from 'domain/ITransport';

@autoinject
export class TransportService {
    constructor(private httpClient: HttpClient) {

    }

    private readonly _baseUrl = 'https://localhost:5001/api/Transports';

    getTransports(): Promise<ITransport[]> {
        return this.httpClient
            .fetch(this._baseUrl, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ITransport[]) => data)
            .catch(reason => {
                console.error(reason);
                return [];
            });

    }

    getTransport(id: string): Promise<ITransport | null> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ITransport) => data)
            .catch(reason => {
                console.error(reason);
                return null;
            });
    }

    async createTransport(transport: ITransport): Promise<string> {
        try {
            const response = await this.httpClient.post(this._baseUrl, JSON.stringify(transport), {
                cache: 'no-store'
            });
            console.log('createTransport response', response);
            return response.statusText;
        }
        catch (reason) {
            console.error(reason);
            return JSON.stringify(reason);
        }
    }

    updateTransport(transport: ITransport): Promise<string> {
        return this.httpClient.put(this._baseUrl + '/' + transport.id, JSON.stringify(transport), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('updateTransport response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    deleteTransport(transport: ITransport): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + transport.id, JSON.stringify(transport), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('deleteTransport response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

}
