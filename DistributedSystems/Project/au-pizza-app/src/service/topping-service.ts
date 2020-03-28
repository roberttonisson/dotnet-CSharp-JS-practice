import { autoinject } from 'aurelia-framework';
import { HttpClient } from 'aurelia-fetch-client';
import { ITopping } from 'domain/ITopping';

@autoinject
export class ToppingService {
    constructor(private httpClient: HttpClient) {

    }

    private readonly _baseUrl = 'https://localhost:5001/api/Toppings';

    getToppings(): Promise<ITopping[]> {
        return this.httpClient
            .fetch(this._baseUrl, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ITopping[]) => data)
            .catch(reason => {
                console.error(reason);
                return [];
            });

    }

    getTopping(id: string): Promise<ITopping | null> {
        return this.httpClient
            .fetch(this._baseUrl + '/' + id, { cache: "no-store" })
            .then(response => response.json())
            .then((data: ITopping) => data)
            .catch(reason => {
                console.error(reason);
                return null;
            });
    }

    createTopping(topping: ITopping): Promise<string> {
        console.log("-----------------" + JSON.stringify(topping));
        return this.httpClient.post(this._baseUrl, JSON.stringify(topping), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('createTopping response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    updateTopping(topping: ITopping): Promise<string> {
        return this.httpClient.put(this._baseUrl + '/' + topping.id, JSON.stringify(topping), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('updateTopping response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

    deleteTopping(topping: ITopping): Promise<string> {
        return this.httpClient.delete(this._baseUrl + '/' + topping.id, JSON.stringify(topping), {
            cache: 'no-store'
        }).then(
            response => {
                console.log('deleteTopping response', response);
                return response.statusText;
            }
        ).catch(reason => {
            console.error(reason);
            return JSON.stringify(reason);
        });
    }

}
