import React, { useState, useContext, useEffect } from "react";
import { AppContext } from "../../../context/AppContext";
import { BaseService } from "../../../base/BaseService";
import jwt_decode from "jwt-decode";
import { IStudentCourse } from "../../../domain/IStudentCourseDTO";
import { Link } from "react-router-dom";

interface IState {
    courses: IStudentCourse[];
    coursesCopy: IStudentCourse[];
    year: number;
    semester: string;
}

const StudentCourseIndex = () => {

    const [state, setState] = useState({courses: [], coursesCopy: [], year: 0, semester: ''} as IState);
    const appContext = useContext(AppContext);
    const data = async () => await BaseService
        .getEntities<IStudentCourse>('courses/getstudentcourses/' + jwt_decode<any>(appContext.jwt!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'], appContext.jwt!)
        .then(data => setState({...state, courses: data, coursesCopy: data}));

    useEffect(() => {
        data();
    }, []);

    function averageGrade() {
        let sum = 0;
       let studentcourses =  state.coursesCopy.filter(s => s.grade != null);
       studentcourses.forEach(element => {
           sum += parseFloat(element.grade!)
       });

       return sum / studentcourses.length;
    }

    function handleChange(e: EventTarget & HTMLSelectElement | EventTarget & HTMLInputElement) {
        setState({...state, [e.name]: e.value})
    }

    function handleSubmit() {
        if (state.year == 0) {
            setState({...state, coursesCopy: state.courses.filter(s => s.course?.semester.includes(state.semester))})
        } else {
            setState({...state, coursesCopy: state.courses.filter(s => s.course?.semester.includes(state.semester) && s.course.year == state.year)})
        }
    }

    return (<div className="container">
        <h1>My courses</h1>
        
        Choose semester <br/>
        <select className="form-control w-25" name="semester" value={state.semester} onChange={(e) => handleChange(e.target)}>
            <option value="">Both</option>
            <option value="Spring">Spring</option>
            <option value="Autumn">Autumn</option>
        </select>
        <br/><br/>
        Choose year
        <br/>
        <input className="form-control w-25" name="year" value={state.year} type="number" min={0} max={5000} onChange={(e) => handleChange(e.target)} />
        <br/>
        <button className="btn btn-primary" onClick={() => handleSubmit()}>Search</button>

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
                        Grade
                    </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {state.coursesCopy.map(s => (

                    <tr key={s.id}>
                        <td>
                            {s.course!.name}
                        </td>
                        <td>
                            {s.course!.code}
                        </td>
                        <td>
                            {s.course!.ects}
                        </td>
                        <td>
                            {s.course!.semester}
                        </td>
                        <td>
                            {s.course!.year}
                        </td>
                        <td>
                            {s.grade}
                        </td>
                        <th>
                            <Link to={'/HomeworkStudentIndex/' + s.course!.id} >Homework</Link>
                        </th>
                    </tr>
                ))}
                <tr>
                    <td colSpan={5} style={{textAlign: "right"}}>
                        <b>Average grade: </b>
                    </td>
                    <td>
                        <b> {averageGrade()} out of 5</b>
                    </td>
                </tr>
            </tbody>
        </table>

    </div>);
}

export default StudentCourseIndex;