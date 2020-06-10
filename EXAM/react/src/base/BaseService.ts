import { IBaseDomain } from './contracts/IBaseDomain';
import Axios from 'axios';
import { useContext } from 'react';
import { AppContext } from '../context/AppContext';

export abstract class BaseService {

    getJwt() {
        let appContext = useContext(AppContext);
        return appContext.jwt;
    }

    static axios = Axios.create(
        {
            baseURL: "https://localhost:5001/api/",
            headers: {
                common: {
                    'Content-Type': 'application/json'
                }
            }
        }
    );

    static async getEntities<T>(view: string, jwt?: string): Promise<T[]> {
        try {
            const response = await this.axios
                .get(view, {
                    headers: {
                        Authorization: "Bearer " + jwt
                    }
                }
                );
            // happy case
            if (response.status >= 200 && response.status < 300) {
                return response.data;
            }

            // something went wrong
            return [];

        } catch (reason) {
            return [];
        }
    }

    static async getEntity<T>(id: string, view: string, jwt?: string): Promise<T | null> {
        try {
            const response = await this.axios
                .get(view + '/' + id, {
                    headers: {
                        Authorization: "Bearer " + jwt
                    }
                }
                );

            if (response.status >= 200 && response.status < 300) {
                return response.data;
            }

            return null;

        } catch (reason) {
            return null;
        }
    }

    static async getSingle<T>(view: string, jwt?: string): Promise<T | null> {
        try {
            const response = await this.axios
                .get(view , {
                    headers: {
                        Authorization: "Bearer " + jwt
                    }
                }
                );

            if (response.status >= 200 && response.status < 300) {
                return response.data;
            }

            return null;

        } catch (reason) {
            return null;
        }
    }

    static async createEntity<T>(entity: T, view: string, jwt?: string): Promise<number> {
        try {
            const response = await this.axios
                .post(view, entity, {
                    headers: {
                        Authorization: "Bearer " + jwt
                    }
                })

            if (response.status >= 200 && response.status < 300) {

                return response.status;
            }

            return response.status;
        }
        catch (reason) {
            return reason.status;
        }
    }

    static async updateEntity<T extends IBaseDomain>(entity: T, view: string, jwt?: string): Promise<number> {
        try {
            const response = await this.axios
                .put(view + '/' + entity.id, entity, {
                    headers: {
                        Authorization: "Bearer " + jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                return response.status;
            }
            return response.status;
        }
        catch (reason) {
            return reason.status;
        }
    }

    static async deleteEntity(id: string, view: string, jwt?: string): Promise<number> {

        try {
            const response = await this.axios
                .delete(view + '/' + id, {
                    headers: {
                        Authorization: "Bearer " + jwt
                    }
                });

            if (response.status >= 200 && response.status < 300) {
                return response.status;
            }
            return response.status;
        }
        catch (reason) {
            return reason.status;
        }
    }

}
