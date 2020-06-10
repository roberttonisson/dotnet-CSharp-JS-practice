import React, { useState, useEffect, useContext } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { IHomeworkDTO } from "../../../domain/IHomeworkDTO";
import { BaseService } from "../../../base/BaseService";
import { AppContext } from "../../../context/AppContext";

const EditHomework = () => {

    let { id } = useParams();
    const [item, setItem] = useState({} as IHomeworkDTO);
    const appContext = useContext(AppContext);
    const data = async () => await BaseService
        .getEntity<IHomeworkDTO>(id, 'homework/gethomework', appContext.jwt!)
        .then(data => setItem(data!));
    useEffect(() => {
        data()
    }, []);

    useEffect(() => {
    }, [item.description, item.title]);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setItem({ ...item, [target.name]: target.value });
    }

    const handleSubmit = async () => {
        await BaseService
            .updateEntity<IHomeworkDTO>(item, 'homework/puthomework', appContext.jwt!)
        return alert('changes saved')
    }

    return (<div className="container">
        <h1>Edit</h1>

        <h4>Homework</h4>
        <hr />
        <div className="row">
            <div className="col-md-4">
                <div className="form-group">
                    <label className="control-label">Title</label>
                    <input value={item.title} name="title" className="form-control" onChange={(e) => handleChange(e.target)} />
                </div>
                <div className="form-group">
                    <label className="control-label">Description</label>
                    <input value={item.description} name="description" onChange={(e) => handleChange(e.target)} className="form-control" />
                </div>
                <div className="form-group">
                    <button className="btn btn-primary" onClick={() => handleSubmit()} type="submit">Submit</button>
                </div>
            </div>
        </div>

        {/* <div>
            <Link to={'/hwteacherindex/' + item.courseid}>Back</Link>
        </div> */}
    </div>)
}

export default EditHomework;