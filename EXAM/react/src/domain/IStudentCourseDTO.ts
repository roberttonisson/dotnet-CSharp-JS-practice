import { ICourse } from './ICourse';
import { IBaseDomain } from '../base/contracts/IBaseDomain';
import { IUser } from './IUser';
export interface IStudentCourse extends IBaseDomain {
    appUser?: IUser;
    appUserId?: string;
    course?: ICourse;
    accepted: boolean | null;
    grade?: string;
}