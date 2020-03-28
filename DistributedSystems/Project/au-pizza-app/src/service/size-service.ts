import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ISize } from 'domain/ISize';

@autoinject
export class SizeService {
    constructor(private httpClient: HttpClient) {

    }

    private readonly _baseUrl = 'https://localhost:5001/api/Sizes';

    getSizes(): Promise<ISize[]> {
        return this.httpClient
            .fetch(this._baseUrl, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ISize[]) => data)
            .catch(reason => {
                console.error(reason);
                return [];
            });

    }

    getSize(id: string): Promise<ISize | null> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ISize) => data)
            .catch(reason => {
                console.error(reason);
                return null;
            });
    }

    createSize(size: ISize): Promise<string> {
        console.log("-----------------" + JSON.stringify(size));
        return this.httpClient.post(this._baseUrl, JSON.stringify(size), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('createSize response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    updateSize(size: ISize): Promise<string> {
        return this.httpClient.put(this._baseUrl + '/' + size.id, JSON.stringify(size), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('updateSize response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    deleteSize(size: ISize): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + size.id, JSON.stringify(size), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('deleteSize response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

}
