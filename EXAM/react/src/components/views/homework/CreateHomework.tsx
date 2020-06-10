import React, { useState, useEffect, FormEvent, useContext } from "react";
import { useParams, useHistory, Link } from "react-router-dom";
import { IHomeworkDTO } from "../../../domain/IHomeworkDTO";
import { BaseService } from "../../../base/BaseService";
import { AppContext } from "../../../context/AppContext";

const CreateHomework = () => {

    let { id } = useParams();
    const appContext = useContext(AppContext);
    const history = useHistory();
    const [props, setState] = useState({id: 'x', title: '', description: '', courseid: id} as IHomeworkDTO);

    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setState({ ...props, [target.name]: target.value });
    }

    const handleSubmit = async () => {

       await BaseService
            .createEntity(props, 'homework/posthomework', appContext.jwt!)
            .then(data =>  history.push('/hwteacherindex/' + id));

    }


    return (<div className="container">
        <h1>Create</h1>

        <h4>Homework</h4>
        <hr />
        <div className="row">
            <div className="col-md-4">
                <form >
                    <div className="form-group">
                        <label className="control-label">Title</label>
                        <input value={props.title} name="title" onChange={(e) => handleChange(e.target)} className="form-control" />
                    </div>
                    <div className="form-group">
                        <label asp-for="Description" className="control-label">Description</label>
                        <input value={props.description} name="description" onChange={(e) => handleChange(e.target)} asp-for="Description" className="form-control" />
                    </div>
                    <div className="form-group">
                        <input onClick={() => handleSubmit()} value="Create" className="btn btn-primary" />
                    </div>
                </form>
            </div>
        </div>

        <div>
            <Link to={'/hwteacherindex/' + id}>Back</Link>
        </div>
    </div>)
}

export default CreateHomework;