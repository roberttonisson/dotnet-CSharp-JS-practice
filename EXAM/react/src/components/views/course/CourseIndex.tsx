import React, { useState, useEffect, useContext } from "react";
import { BaseService } from "../../../base/BaseService";
import { ICourse } from "../../../domain/ICourse";
import { AppContext } from "../../../context/AppContext";
import jwt_decode from "jwt-decode";
import { IStudentCourse } from "../../../domain/IStudentCourseDTO";
import { IUser } from "../../../domain/IUser";

interface ICourseIndexState {
    courses: ICourse[];
    search: string;
    coursesCopy: ICourse[];
}

const CourseIndex = () => {
    const [state, setState] = useState({courses: [], search: "", coursesCopy: []} as ICourseIndexState);
    const appContext = useContext(AppContext);
    const data = async () => await BaseService
        .getEntities<ICourse>('courses/getcourses')
        .then(data => setState({...state, courses: data, coursesCopy: data}));

    useEffect(() => {
        data()
    }, []);

    useEffect(() => {

    }, [state.search.length])

    async function enroll(course: ICourse, email: string) {
        var user: IUser = {email: email, id: 'x', firstName: '', lastName: ''};

        let dto: IStudentCourse = {course: course, appUser: user, accepted: null, id: ''};

        await BaseService.createEntity<IStudentCourse>(dto, 'StudentCourse/PostStudentCourse', appContext.jwt!);
        data();
    }

    function update(target: EventTarget & HTMLInputElement) {
        setState({...state, [target.name]: target.value})
    }

     async function search() {
        setState({...state, coursesCopy: state.courses.filter(s => s.name.toLowerCase().includes(state.search.toLowerCase())
            || s.code.toLowerCase().includes(state.search.toLowerCase())
            || (s.user!.firstName + s.user!.lastName).toLowerCase().includes(state.search.toLowerCase()))})
    }

    useEffect(() => {

    }, [state.coursesCopy.length]);

    function enrollOption(item: ICourse) {

        if (appContext.jwt == null) {
            return (<td></td>);
        }
        else {
            var course = item.studentCourses?.find(s => s != undefined && s!.appUser != undefined && s.appUser.email == jwt_decode<any>(appContext.jwt!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']) ?? null;
            if (jwt_decode<any>(appContext.jwt!)['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == 'Teacher') {
                return (<td></td>)
            }
            else if (course == null && appContext.jwt != null) {
                return (<td>
                    <button onClick={() => enroll(item, jwt_decode<any>(appContext.jwt!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'])} > Enroll </button>
                </td>);
            }
            else if (course!.accepted == null) {
                return (<td>Pending</td>);
            }
            else if (course!.accepted == true) {
                return (<td>Accepted</td>);
            }
            else if (course!.accepted == false) {
                return (<td>Denied</td>);
            }
        }
        return (<td></td>);
    }

    return (
        <div className="container">
            <h1>Available courses</h1>

            <input className="form-control w-25" type="text" value={state.search} name="search" onChange={(e) => update(e.target)} />
            <button className="btn btn-primary" onClick={() => search()} >Search</button>

            <table  className="table">
                <thead>
                    <tr>
                        <th>
                            Name
        </th>
                        <th>
                            Code
        </th>
        <th>
                            Teacher
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
                        <th>
                            Students
        </th>
                        <th>
                            Enrollment
        </th>   
                    </tr>
                </thead>
                <tbody>
                    {state.coursesCopy.map(course => (

                        <tr key={course.id}>
                            <td>
                                {course.name}
                            </td>
                            <td>
                                {course.code}
                            </td>
                            <td>
                                {(course.user!.firstName + " " + course.user!.lastName)}
                            </td>
                            <td>
                                {course.ects}
                            </td>
                            <td>
                                {course.semester}
                            </td>
                            <td>
                                {course.year}
                            </td>
                            <td>
                                {course.description}
                            </td>
                            <td>
                                {course.studentCourses?.filter(ss => ss.accepted).length}
                            </td>
                            {enrollOption(course)}
                        </tr>
                    ))}

                </tbody>
            </table>

        </div>
    );
}

export default CourseIndex;