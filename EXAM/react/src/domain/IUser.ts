import { IBaseDomain } from './../base/contracts/IBaseDomain';
export interface IUser extends IBaseDomain {
    firstName: string;
    lastName: string;
    email: string;
}