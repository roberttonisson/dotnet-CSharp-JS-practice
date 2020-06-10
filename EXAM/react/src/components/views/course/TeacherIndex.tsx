import React, { useState, useContext, useEffect } from "react";
import { ICourse } from "../../../domain/ICourse";
import { AppContext } from "../../../context/AppContext";
import { BaseService } from "../../../base/BaseService";
import jwt_decode from "jwt-decode";
import { Link } from "react-router-dom";

const TeacherCourseIndex = () => {
    const [courses, setCourses] = useState([] as ICourse[]);
    const appContext = useContext(AppContext);
    const data = async () => await BaseService
        .getEntities<ICourse>('courses/getteachercourses/' + jwt_decode<any>(appContext.jwt!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'], appContext.jwt!)
        .then(data => setCourses(data));

    useEffect(() => {
        data();
    }, [courses.length]);

    return (
        <div className="container">
            <h1>Courses that you are teaching</h1>

            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Name
        </th>
                        <th>
                            Code
        </th>
                        <th>
                            ECTS
        </th>
                        <th>
                            Semester
        </th>
                        <th>
                            Year
        </th>
                        <th>
                            Description
        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {courses.map(s => (
                        <tr key={s.id}>
                            <td>
                                {s.name}
                            </td>
                            <td>
                                {s.code}
                            </td>
                            <td>
                                {s.ects}
                            </td>
                            <td>
                                {s.semester}
                            </td>
                            <td>
                                {s.year}
                            </td>
                            <td>
                                {s.description}
                            </td>
                            <td>
                                <Link to={'/homeworkteacherindex/' + s.id}>Homework</Link> |
                                <Link to={'/teacherstudentsindex/' + s.id}>Students</Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TeacherCourseIndex;