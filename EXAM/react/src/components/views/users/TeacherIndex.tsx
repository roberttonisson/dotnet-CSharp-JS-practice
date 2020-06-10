import React, { useState, useEffect, useContext } from "react";
import { IUser } from "../../../domain/IUser";
import { BaseService } from "../../../base/BaseService";
import { AppContext } from "../../../context/AppContext";

interface IState {
    items: IUser[];
    search: string;
    copyItems: IUser[];
}

const TeacherIndex = () => {
    const [teachers, setTeachers] = useState({items: [], search: "", copyItems: []} as IState);
    const appContext = useContext(AppContext);
    const data = async () => await BaseService
        .getEntities<IUser>('users', appContext.jwt!)
        .then(data => setTeachers({...teachers, items: data, copyItems: data}));

    useEffect(() => {
        data();
    }, []);

    useEffect(() => {
    }, [teachers.copyItems.length])


    function update(target: EventTarget & HTMLInputElement) {
        setTeachers({...teachers, [target.name]: target.value})
    }

     async function search() {
        setTeachers({...teachers, copyItems: teachers.items.filter(s => (s.firstName + s.lastName + s.email).toLowerCase().includes(teachers.search.toLowerCase()))})
    }


    return (
        <div className="container">
            <h1>Teachers</h1>

            <input className="form-control w-25" type="text" value={teachers.search} name="search" onChange={(e) => update(e.target)} />
            <button className="btn btn-primary" onClick={() => search()} >Search</button>
            <br/>


            <table style={{ paddingLeft: "100px" }} className="table">
                <thead>
                    <tr>
                        <th>
                            Email
        </th>
                        <th>
                            First name
        </th>
                        <th>
                            Last name
        </th>
                    </tr>
                </thead>
                <tbody>
                    {teachers.copyItems.map(t => (

                        <tr key={t.id}>
                            <td>
                                {t.email}
                            </td>
                            <td>
                                {t.firstName}
                            </td>
                            <td>
                                {t.lastName}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TeacherIndex;