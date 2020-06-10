import React, { useState, useEffect, useContext } from "react";
import { useParams, Link } from "react-router-dom";
import { IHomeworkIndexDTO } from "../../../domain/IHomeworkIndexDTO";
import { BaseService } from "../../../base/BaseService";
import { IHomeworkDTO } from "../../../domain/IHomeworkDTO";
import { AppContext } from "../../../context/AppContext";

const HomeworkTeacherIndex = () => {

    let { id } = useParams();
    const appContext = useContext(AppContext);
    const [items, setItems] = useState([] as IHomeworkDTO[]);
    const data = async () => await BaseService
        .getEntities<IHomeworkDTO>('homework/GetTeacherHomework/' + id, appContext.jwt!)
        .then(data => setItems(data));

    useEffect(() => {
        data();
    });

    const deleteHw = async (id: string) => {
        await BaseService.deleteEntity(id, 'homework/deletehomework');
        await data();
    }


    return (
        <div className="container">
            <h1>Homework</h1>

            <p>
                <Link to={'/createhomework/' + id}>Create new homework</Link>
            </p>
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
                                <Link to={'/studenthomeworkteacher/' + hw.id}><button className="btn btn-primary">Submissions</button></Link>
                            <Link to={'/edithomework/' + hw.id}><button className="btn btn-primary">Edit</button></Link>
                                <button className="btn btn-danger" onClick={(e) => deleteHw(hw.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>)
}

export default HomeworkTeacherIndex;