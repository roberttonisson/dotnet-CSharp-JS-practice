import React, { useState, useEffect, useContext } from "react";
import { useParams, Link } from "react-router-dom";
import { IHomeworkDTO } from "../../../domain/IHomeworkDTO";
import { BaseService } from "../../../base/BaseService";
import { IStudentHomework } from "../../../domain/IStudentHomework";
import { AppContext } from "../../../context/AppContext";

const StudentHomeworkTeacherIndex = () => {
    let { id } = useParams();
    const appContext = useContext(AppContext);

    const [items, setItems] = useState([] as IStudentHomework[]);
    const data = async () => await BaseService
        .getEntities<IStudentHomework>('studenthomework/getteacherindex/' + id, appContext.jwt!)
        .then(data => setItems(data));

    useEffect(() => {
        data();
    }, []);

    function averageGrade() {
        let sum = 0;
        let studentcourses = items.filter(s => s.grade != null);
        studentcourses.forEach(element => {
            sum += parseFloat(element.grade!)
        });

        return sum / studentcourses.length;
    }

    return (<>
        <h1>Student Homework</h1>


        <table className="table">
            <thead>
                <tr>
                    <th>
                        Student
            </th>
                    <th>
                        Grade
            </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                {items.map(i => (
                    <tr key={i.id}>
                        <td>
                            {i.appUser?.firstName + " " + i.appUser?.lastName}
                        </td>
                        <th>
                            {i.grade}
                        </th>
                        <td>
                            <Link to={'/studenthomeworkdetails/' + i.id}>Details</Link>
                        </td>
                    </tr>
                ))}
                <tr>
                    <td colSpan={1} style={{ textAlign: "right" }}>
                        <b>Average: </b>
                    </td>
                    <td>
                        <b> {averageGrade()} out of 5</b>
                    </td>
                </tr>
                <tr>
                    <td colSpan={1} style={{ textAlign: "right" }}>
                        <b>Not graded students count: </b>
                    </td>
                    <td>
                        <b> {items.filter(ss => ss.grade == null).length} ({items.filter(ss => ss.grade == null).length / items.length * 100}%)</b>
                    </td>
                </tr>
                <tr>
                    <td colSpan={1} style={{ textAlign: "right" }}>
                        <b>Passed students: </b>
                    </td>
                    <td>
                        <b> {items.filter(ss => ss.grade != null && parseInt(ss.grade) > 0).length} ({items.filter(ss => ss.grade != null && parseInt(ss.grade) > 0).length / items.length * 100}%)</b>
                    </td>
                </tr>
                <tr>
                    <td colSpan={1} style={{ textAlign: "right" }}>
                        <b>Failed students: </b>
                    </td>
                    <td>
                        <b>{items.filter(ss => ss.grade != null && parseInt(ss.grade) == 0).length} ({items.filter(ss => ss.grade != null && parseInt(ss.grade) == 0).length / items.length * 100}%)</b>
                    </td>
                </tr>
            </tbody>
        </table>
    </>)
}

export default StudentHomeworkTeacherIndex;