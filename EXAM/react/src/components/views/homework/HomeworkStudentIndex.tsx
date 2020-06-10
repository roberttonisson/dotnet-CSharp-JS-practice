import React, { useState, useEffect, useContext } from "react";
import { useParams, Link } from "react-router-dom";
import { IHomeworkDTO } from "../../../domain/IHomeworkDTO";
import { BaseService } from "../../../base/BaseService";
import jwt_decode from "jwt-decode";
import { AppContext } from "../../../context/AppContext";

const HomeworkStudentIndex = () => {
    let { id } = useParams();
    const appContext = useContext(AppContext);
    const [items, setItems] = useState([] as IHomeworkDTO[]);
    const data = async () => await BaseService
        .getEntities<IHomeworkDTO>('homework/GetStudentHomework/' + id, appContext.jwt!)
        .then(data => setItems(data));

    useEffect(() => {
        data();
    });

    return (
        <div style={{paddingLeft: "100px", paddingRight: "100px"}}>
            <h1>homework</h1>

            <table className="table">
                <thead>
                    <tr>
                        <th>
                            Title
                        </th>
                        <th>
                            Description
                         </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {items.map(hw => (
                        <tr key={hw.id}>
                            <td>
                                {hw.title}
                            </td>
                            <td>
                                {hw.description}
                            </td>
                            <td>
                                <Link to={'/homeworkstudentdetails/' + hw.id}>Details</Link>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <div>
                <Link to={'/studentcourses/' + jwt_decode<any>(appContext.jwt!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']}>Back</Link>
            </div>
        </div>
    );
}

export default HomeworkStudentIndex;