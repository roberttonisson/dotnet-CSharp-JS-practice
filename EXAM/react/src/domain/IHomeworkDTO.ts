import { IBaseDomain } from '../base/contracts/IBaseDomain';
export interface IHomeworkDTO extends IBaseDomain {
    title: string;
    description: string;
    deadline?: Date;
    courseid?: string;
    studentHomework?: IHomeworkDTO[];
}