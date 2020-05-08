import { StringifyOptions } from "querystring";

export interface IAppUser extends IAppUserCreate {
    id: string;

    }

export interface IAppUserCreate {
    email : string;

    userName: string;
    firstName: string;
    lastName: string;
    address: string;

}
