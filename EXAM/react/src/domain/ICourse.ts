import { IHomeworkDTO } from './IHomeworkDTO';
import { IBaseDomain } from '../base/contracts/IBaseDomain';
import { IUser } from './IUser';
import { IStudentCourse } from './IStudentCourseDTO';
export interface ICourse extends IBaseDomain {
    name: string;
    code: string;
    ects: number;
    semester: string;
    year: number;
    description: string;
    user?: IUser;
    studentCourses?: IStudentCourse[];
    homework?: IHomeworkDTO[];
}