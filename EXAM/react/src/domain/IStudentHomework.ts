import { IHomeworkDTO } from './IHomeworkDTO';
import { IUser } from './IUser';
import { IBaseDomain } from './../base/contracts/IBaseDomain';
export interface IStudentHomework extends IBaseDomain {
    appUser?: IUser;
    appUserId?: string;
    homework?: IHomeworkDTO;
    grade?: string;
    gradedAt?: Date;
    studentAnswer?: string;
}