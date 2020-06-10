import { ICourse } from './ICourse';
import { IHomeworkDTO } from "./IHomeworkDTO";

export interface IHomeworkIndexDTO {

    homeworkDtos: IHomeworkDTO[]
    courseDto: ICourse;
}