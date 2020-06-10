import Axios from 'axios';
import { ILoginDTO } from '../domain/ILoginDTO';
import { IRegisterDTO } from '../domain/IRegisterDTO';

interface ILoginResponse {
    token: string;
    status: string;
}
export abstract class AccountApi {
    private static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    )

    static async getJwt(loginDTO: ILoginDTO): Promise<string | null> {
        const url = "account/login";
        try {
            const response = await this.axios.post<ILoginResponse>(url, loginDTO);
            if (response.status === 200) {
                return response.data.token;
            }
            return null;
        } catch (error) {
            return null;
        }
    }

    static async register(registerDTO: IRegisterDTO): Promise<number> {
        const url = "account/register";
        const response = await this.axios.post(url, registerDTO);
        return response.status;
    }

    static async getLoggedInUser(userName: string | null): Promise<IRegisterDTO | null> {
        try {
            const response = await this.axios.get('account/user/' + userName);

            // happy case
            if (response.status >= 200 && response.status < 300) {
                return response.data;
            }

            // something went wrong
            return null;
        } catch (reason) {
            return reason.statusText;
        }
    }

    static async updateUser(user: IRegisterDTO | null): Promise<number> {
        try {
            const response = await this.axios.put('account/update', user)

            // happy case
            if (response.status >= 200 && response.status < 300) {
                return response.status;
            }

            // something went wrong
            return response.status;
        } catch (reason) {
            return reason.statusText;
        }
    }
}
