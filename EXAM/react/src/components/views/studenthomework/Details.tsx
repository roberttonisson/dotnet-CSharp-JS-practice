import React, { useState, useEffect, useContext } from "react";
import { useParams } from "react-router-dom";
import { IStudentHomework } from "../../../domain/IStudentHomework";
import { BaseService } from "../../../base/BaseService";
import GradeHomework from "./partials/GradeHomeworkPartial";
import { AppContext } from "../../../context/AppContext";

const StudentHomeworkDetails = () => {

    let { id } = useParams();
    const [item, setItem] = useState({} as IStudentHomework);
    const appContext = useContext(AppContext);
    const data = async () => await BaseService
        .getEntity<IStudentHomework>(id, 'studenthomework/getstudenthomework', appContext.jwt!)
        .then(data => setItem(data!));

    useEffect(() => {
        data()
    }, []);


    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setItem({ ...item, [target.name]: target.value });
    }

    const handleSubmit = async () => {
        await BaseService
            .updateEntity<IStudentHomework>(item, 'homework/puthomework', appContext.jwt!)
        return alert('changes saved')
    }

    return (<>
        <div className="container">
            <h4>StudentHomework</h4>
            <hr />
            <dl className="row">
                <dt className="col-sm-2">
                    Student
        </dt>
                <dd className="col-sm-10">
                    {item.appUser?.firstName + " " + item.appUser?.lastName}
        </dd>
                <dt className="col-sm-2">
                    Homework
        </dt>
                <dd className="col-sm-10">
                    {item.homework?.title}
        </dd>
                <dt className="col-sm-2">
                    Grade
        </dt>
                <dd className="col-sm-10">
                <GradeHomework item={item} />
        </dd>
                <dt className="col-sm-2">
                    Student's answer
        </dt>
                <dd className="col-sm-10">
                    {item.studentAnswer}
        </dd>
            </dl>
        </div>
        {/* <div>
            <a asp-action="TeacherIndex" asp-route-id="@Model.HomeworkId">Back to List</a>
        </div> */}
    </>);
}

export default StudentHomeworkDetails;