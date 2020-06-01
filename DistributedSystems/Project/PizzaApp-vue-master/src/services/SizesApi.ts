import { ISize, ISizeCreate } from '../domain/ISize';
import Axios from 'axios';

export abstract class SizesApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/v1.0/Sizes/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(jwt: string): Promise<ISize[]> {
        const url = "";
        try {
            const response = await this.axios.get<ISize[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async getSingle(id: string, jwt: string): Promise<ISize> {
        const url = "" + id;
        try {
            const response = await this.axios.get<ISize>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('getSingle response', response);
            if (response.status === 200) {
                return response.data;
            }
            return { id: "", price: 0, name: "", sizeCm: 0 };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return { id: "", price: 0, name: "", sizeCm: 0 };
        }
    }

    static async delete(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<ISize>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(size: ISizeCreate, jwt: string): Promise<void> {
        const url = "";
        console.log(JSON.stringify(size))
        console.log(JSON.stringify(size))
        console.log(JSON.stringify(size))
        try {
            const response = await this.axios.post<ISizeCreate>(url, size, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('create response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(size: ISize, jwt: string): Promise<void> {
        const url = "" + size.id;
        try {
            const response = await this.axios.put<ISize>(url, size, {
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
