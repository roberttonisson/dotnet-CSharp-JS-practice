import { StringifyOptions } from "querystring";
import { IAppUser } from "./IAppUser";

export interface IPartyOrder{
    id: string;

    appUserId: string;
    appUser: IAppUser | null;

    start: Date;
    end: Date;

    total: number | null;
    address: string;
    inviteKey: string;
}

export interface IPartyOrderCreate {
    id: string | undefined;
    appUserId: string;

    start: Date;
    end: Date;

    total: number | null;
    address: string;
    inviteKey: string;
}
