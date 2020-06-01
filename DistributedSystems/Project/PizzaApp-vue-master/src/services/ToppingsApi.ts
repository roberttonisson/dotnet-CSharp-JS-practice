import { ITopping, IToppingCreate } from '../domain/ITopping';
import Axios from 'axios';

export abstract class ToppingsApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Toppings/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(jwt: string): Promise<ITopping[]> {
        const url = "";
        try {
            const response = await this.axios.get<ITopping[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('getAll response', response);
            if (response.status === 200) {
                return response.data;
            }
            return [];
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return [];
        }
    }

    static async getSingle(id: string, jwt: string): Promise<ITopping> {
        const url = "" + id;
        try {
            const response = await this.axios.get<ITopping>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('getSingle response', response);
            if (response.status === 200) {
                return response.data;
            }
            return { id: "", price: 0, name: "" };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return { id: "", price: 0, name: "" };
        }
    }

    static async delete(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<ITopping>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(topping: IToppingCreate, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<IToppingCreate>(url, topping, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('create response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(topping: ITopping, jwt: string): Promise<void> {
        const url = "" + topping.id;
        try {
            const response = await this.axios.put<ITopping>(url, topping, {
                headers: {
                    authorization: "Bearer " + jwt
                }
            });
            console.log('edit response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }
}
