import { ITransport, ITransportCreate } from './../domain/ITransport';
import Axios from 'axios';

export abstract class TransportsApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/Transports/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getAll(jwt: string): Promise<ITransport[]> {
        const url = "";
        try {
            const response = await this.axios.get<ITransport[]>(url, { headers: { Authorization: 'Bearer ' + jwt } });
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

    static async getSingle(id: string, jwt: string): Promise<ITransport> {
        const url = "" + id;
        try {
            const response = await this.axios.get<ITransport>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('getSingle response', response);
            if (response.status === 200) {
                return response.data;
            }
            return { id: "", cost: 0, address: "" };
        } catch (error) {
            console.log('error: ', (error as Error).message);
            return { id: "", cost: 0, address: "" };
        }
    }

    static async delete(id: string, jwt: string): Promise<void> {
        const url = "" + id;
        try {
            const response = await this.axios.delete<ITransport>(url, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('delete response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async create(transport: ITransportCreate, jwt: string): Promise<void> {
        const url = "";
        try {
            const response = await this.axios.post<ITransportCreate>(url, transport, { headers: { Authorization: 'Bearer ' + jwt } });
            console.log('create response', response);
            if (response.status === 200) {
                return;
            }
            return;
        } catch (error) {
            console.log('error: ', (error as Error).message);
        }
    }

    static async edit(transport: ITransport, jwt: string): Promise<void> {
        const url = "" + transport.id;
        try {
            const response = await this.axios.put<ITransport>(url, transport, {
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
