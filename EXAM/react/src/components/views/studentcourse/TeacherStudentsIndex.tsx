import React, { useState, useEffect, useContext } from "react";
import { IStudentCourse } from "../../../domain/IStudentCourseDTO";
import { BaseService } from "../../../base/BaseService";
import { useParams, Link } from "react-router-dom";
import GradeCourse from "./partials/GradeCoursePartial";
import { AppContext } from "../../../context/AppContext";

interface IState {
    studentCourses: IStudentCourse[];
    studentCoursesCopy: IStudentCourse[];
    search: string;
}

const TeacherStudentsIndex = () => {
    const [state, setItem] = useState({ studentCourses: [], studentCoursesCopy: [], search: '' } as IState);
    let { courseid } = useParams();
    const appContext = useContext(AppContext);
    const data = async () => {
        await BaseService
            .getEntities<IStudentCourse>('studentcourse/getstudentcourses/' + courseid, appContext.jwt!)
            .then(data => {
                setItem({ ...state, studentCourses: data, studentCoursesCopy: data });
            });
    }

    useEffect(() => {
        data()
    }, []);

    useEffect(() => {
        data()
    }, [state.studentCourses]);

    async function processRequest(item: IStudentCourse, status: boolean) {
        item.accepted = status;
        await BaseService.updateEntity<IStudentCourse>(item, 'studentcourse/edit', appContext.jwt!);
        data();
    }

    function enrollStatus(item: IStudentCourse) {

        if (item.accepted != null || item.accepted != undefined) {
            return (<>{item.accepted ? 'Yes' : 'No'}</>);
        }

        return (<>
            <button className="btn btn-success" onClick={() => processRequest(item, true)} >Accept</button>
            <button className="btn btn-danger" onClick={() => processRequest(item, false)}>Deny</button>
        </>);
    }

    function ifGrade(item: IStudentCourse) {
        if (item.accepted) {
            return <GradeCourse item={item} />
        }

        return (<></>);
    }

    function averageGrade() {
        let sum = 0;
        let studentcourses = state.studentCoursesCopy.filter(s => s.grade != null);
        studentcourses.forEach(element => {
            sum += parseFloat(element.grade!)
        });

        return sum / studentcourses.length;
    }

    function update(target: EventTarget & HTMLInputElement) {
        setItem({ ...state, [target.name]: target.value })
    }

    async function search() {
        setItem({ ...state, studentCoursesCopy: state.studentCourses.filter(s => (s.appUser!.firstName + s.appUser!.lastName).toLowerCase().includes(state.search.toLowerCase())) })
    }

    useEffect(() => {

    }, [state.studentCoursesCopy.length])

    return (<div className="container">
        <h1>Grade for</h1>

        <input type="text" value={state.search} name="search" onChange={(e) => update(e.target)} />
        <button className="btn btn-primary" onClick={() => search()} >Search</button>

        <table className="table">
            <thead>
                <tr>
                    <th>
                        Student
        </th>
                    <th>
                        Course
        </th>
                    <th>
                        Accepted
        </th>
                    <th>
                        Grade
        </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {state.studentCoursesCopy.map((ss, index) => (

                    <tr key={ss.id}>
                        <td>
                            {ss.appUser?.firstName + " " + ss.appUser?.lastName}
                        </td>
                        <td>
                            {ss.course?.name}
                        </td>
                        <td>
                            {enrollStatus(ss)}
                        </td>
                        <td>
                            {ifGrade(ss)}
                        </td>
                    </tr>
                ))}
                <tr>
                    <td colSpan={3} style={{ textAlign: "right" }}>
                        <b>Average: </b>
                    </td>
                    <td>
                        <b> {averageGrade()} out of 5</b>
                    </td>
                </tr>
                <tr>
                    <td colSpan={3} style={{ textAlign: "right" }}>
                        <b>Not graded students count: </b>
                    </td>
                    <td>
                        <b> {state.studentCoursesCopy.filter(ss => ss.grade == null).length} ({state.studentCoursesCopy.filter(ss => ss.grade == null).length / state.studentCoursesCopy.length * 100}%)</b>
                    </td>
                </tr>
                <tr>
                    <td colSpan={3} style={{ textAlign: "right" }}>
                        <b>Passed students: </b>
                    </td>
                    <td>
                        <b> {state.studentCoursesCopy.filter(ss => ss.grade != null && parseInt(ss.grade) > 0).length} ({state.studentCoursesCopy.filter(ss => ss.grade != null && parseInt(ss.grade) > 0).length / state.studentCoursesCopy.length * 100}%)</b>
                    </td>
                </tr>
                <tr>
                    <td colSpan={3} style={{ textAlign: "right" }}>
                        <b>Failed students: </b>
                    </td>
                    <td>
                        <b>{state.studentCoursesCopy.filter(ss => ss.grade != null && parseInt(ss.grade) == 0).length} ({state.studentCoursesCopy.filter(ss => ss.grade != null && parseInt(ss.grade) == 0).length / state.studentCoursesCopy.length * 100}%)</b>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>);
}

export default TeacherStudentsIndex;